using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;

namespace HotelAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ResetsController : ControllerBase
    //{
    //    private readonly ArcadeHotelContext _context;

    //    public ResetsController(ArcadeHotelContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/Resets
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Reset>>> GetResets()
    //    {
    //        return await _context.Resets.ToListAsync();
    //    }

    //    // GET: api/Resets/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Reset>> GetReset(int id)
    //    {
    //        var reset = await _context.Resets.FindAsync(id);

    //        if (reset == null)
    //        {
    //            return NotFound();
    //        }

    //        return reset;
    //    }

    //    // PUT: api/Resets/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutReset(int id, Reset reset)
    //    {
    //        if (id != reset.ResetId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(reset).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ResetExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/Resets
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<Reset>> PostReset(Reset reset)
    //    {
    //        _context.Resets.Add(reset);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetReset", new { id = reset.ResetId }, reset);
    //    }

    //    // DELETE: api/Resets/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteReset(int id)
    //    {
    //        var reset = await _context.Resets.FindAsync(id);
    //        if (reset == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Resets.Remove(reset);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ResetExists(int id)
    //    {
    //        return _context.Resets.Any(e => e.ResetId == id);
    //    }
    //}
}
