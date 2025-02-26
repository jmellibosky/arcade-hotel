using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
using StackExchange.Redis;
using HotelAPI.Requests;
using System.Text;
using System.Security.Cryptography;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ArcadeHotelContext _context;
        private readonly IDatabase _redis;

        public UsersController(ArcadeHotelContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis.GetDatabase();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Recuperar el usuario desde la base de datos
            string sessionKey;
            string name;
            string type;
            double balance = 0;

            Room? userRoom = _context.Rooms
                .Include(r => r.LastMovement)
                .FirstOrDefault(r => r.Name == request.User && r.Password.ToString() == request.Pass);

            if (userRoom == null)
            {
                User? userUser = _context.Users.FirstOrDefault(u => u.Name == request.User && u.Password == request.Pass);
                if (userUser == null)
                {
                    return Unauthorized("User not found.");
                }
                else
                {
                    sessionKey = $"user:user_{userUser.UserId}";
                    name = userUser.Name;
                    type = "admin";
                }
            }
            else
            {
                sessionKey = $"user:room_{userRoom.RoomId}";
                name = userRoom.Name;
                type = "room";
                balance = userRoom.LastMovement != null ? userRoom.LastMovement.Balance : 0;
            }

            sessionKey = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(sessionKey)));

            
            await _redis.StringSetAsync(sessionKey, request.User, TimeSpan.FromDays(7));

            Response.Cookies.Append("SessionId", sessionKey, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new {
                Message = "Session successfully authenticated.",
                Key = sessionKey,
                Name = name,
                Type = type,
                Balance = balance
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            RedisValue sessionKey = await _redis.StringGetAsync(request.Key);

            if (!sessionKey.HasValue)
            {
                return BadRequest("No active session.");
            }

            if (request.User != sessionKey) {
                return BadRequest("Key and user don't match up.");
            }
            
            await _redis.KeyDeleteAsync(request.Key);
            return Ok(new { Message = "Session successfully closed." });
        }
    }
}
