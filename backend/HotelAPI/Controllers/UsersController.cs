using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
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

        public UsersController(ArcadeHotelContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine("Login attempt for user: " + request.User);

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
            return Ok(new { Message = "Session successfully closed." });
        }
    }
}
