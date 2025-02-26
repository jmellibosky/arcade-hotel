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

            Room? userRoom = _context.Rooms.FirstOrDefault(r => r.Name == request.User && r.Password.ToString() == request.Pass);
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
                }
            }
            else
            {
                sessionKey = $"user:room_{userRoom.RoomId}";
            }
            
            await _redis.StringSetAsync(sessionKey, request.User, TimeSpan.FromDays(7));

            Response.Cookies.Append("SessionId", sessionKey, new CookieOptions
            {
                HttpOnly = true, // Para que la cookie no sea accesible desde JavaScript
                Secure = true, // Solo se enviará sobre HTTPS
                SameSite = SameSiteMode.Strict, // Evitar que la cookie sea enviada en solicitudes cruzadas
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { Message = "Session successfully authenticated." });
        }

        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            var sessionKey = Request.Cookies["SessionId"];
            if (sessionKey == null)
            {
                return BadRequest("No active session.");
            }

            await _redis.KeyDeleteAsync(sessionKey);
            return Ok(new { Message = "Session successfully closed." });
        }
    }
}
