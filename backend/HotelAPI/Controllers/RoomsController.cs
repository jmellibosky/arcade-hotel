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

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ArcadeHotelContext _context;

        public RoomsController(ArcadeHotelContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetRooms()
        {
            try
            {
                List<RoomResponse> Response = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .Select(r => new RoomResponse()
                    {
                        Room = r.Name,
                        Pass = r.Password.ToString(),
                        Balance = r.LastMovement != null ? r.LastMovement.Balance : 0d
                    })
                    .ToListAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //// GET: api/Rooms/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Room>> GetRoom(int id)
        //{
        //    var room = await _context.Rooms.FindAsync(id);

        //    if (room == null)
        //    {
        //        return NotFound();
        //    }

        //    return room;
        //}

        //// PUT: api/Rooms/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRoom(int id, Room room)
        //{
        //    if (id != room.RoomId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(room).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoomExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpPut]
        //[Route("pass")]
        //public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest request)
        //{
        //    try
        //    {
        //        if (request.Room.IsNullOrEmpty() || request.Pass.IsNullOrEmpty())
        //        {
        //            return BadRequest("Room or Pass were empty strings");
        //        }

        //        Room? RoomToReset = await _context.Rooms
        //            .FirstOrDefaultAsync(r => r.Name == request.Room);

        //        if (RoomToReset == null)
        //        {
        //            return BadRequest("Room was not found");
        //        }

        //        RoomToReset.Password = int.Parse(request.Pass);

        //        await _context.SaveChangesAsync();

        //        return Ok(RoomToReset.Name);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        //// POST: api/Rooms
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Room>> PostRoom(Room room)
        //{
        //    _context.Rooms.Add(room);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (RoomExists(room.RoomId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
        //}

        //// DELETE: api/Rooms/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRoom(int id)
        //{
        //    var room = await _context.Rooms.FindAsync(id);
        //    if (room == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Rooms.Remove(room);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RoomExists(int id)
        //{
        //    return _context.Rooms.Any(e => e.RoomId == id);
        //}
    }
}
