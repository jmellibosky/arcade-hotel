using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.IdentityModel.Tokens;
using HotelAPI.Requests;
using HotelAPI.Services.Interfaces;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsService _rooms;

        public RoomsController(IRoomsService rooms)
        {
            _rooms = rooms;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetRooms()
        {
            try
            {
                var Response = await _rooms.GetAll();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPut]
        [Route("pass")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest request)
        {
            try
            {
                if (request.Room.IsNullOrEmpty() || request.Pass.IsNullOrEmpty())
                {
                    return BadRequest("Room or Pass were empty strings");
                }

                return await _rooms.ChangePassword(request);
            }    
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
