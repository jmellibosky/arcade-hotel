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
    //public class ArcadesController : ControllerBase
    //{
    //    private readonly ArcadeHotelContext _context;

    //    public ArcadesController(ArcadeHotelContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/Arcades
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Arcade>>> GetArcades()
    //    {
    //        return await _context.Arcades.ToListAsync();
    //    }

    //    // GET: api/Arcades/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Arcade>> GetArcade(int id)
    //    {
    //        var arcade = await _context.Arcades.FindAsync(id);

    //        if (arcade == null)
    //        {
    //            return NotFound();
    //        }

    //        return arcade;
    //    }

    //    // PUT: api/Arcades/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutArcade(int id, Arcade arcade)
    //    {
    //        if (id != arcade.ArcadeId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(arcade).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ArcadeExists(id))
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

    //    // POST: api/Arcades
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<Arcade>> PostArcade(Arcade arcade)
    //    {
    //        _context.Arcades.Add(arcade);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetArcade", new { id = arcade.ArcadeId }, arcade);
    //    }

    //    // DELETE: api/Arcades/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteArcade(int id)
    //    {
    //        var arcade = await _context.Arcades.FindAsync(id);
    //        if (arcade == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Arcades.Remove(arcade);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ArcadeExists(int id)
    //    {
    //        return _context.Arcades.Any(e => e.ArcadeId == id);
    //    }
    //}
}
