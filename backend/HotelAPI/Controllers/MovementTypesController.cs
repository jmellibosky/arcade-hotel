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
    //public class MovementTypesController : ControllerBase
    //{
    //    private readonly ArcadeHotelContext _context;

    //    public MovementTypesController(ArcadeHotelContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/MovementTypes
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<MovementType>>> GetMovementTypes()
    //    {
    //        return await _context.MovementTypes.ToListAsync();
    //    }

    //    // GET: api/MovementTypes/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<MovementType>> GetMovementType(int id)
    //    {
    //        var movementType = await _context.MovementTypes.FindAsync(id);

    //        if (movementType == null)
    //        {
    //            return NotFound();
    //        }

    //        return movementType;
    //    }

    //    // PUT: api/MovementTypes/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutMovementType(int id, MovementType movementType)
    //    {
    //        if (id != movementType.MovementTypeId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(movementType).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!MovementTypeExists(id))
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

    //    // POST: api/MovementTypes
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<MovementType>> PostMovementType(MovementType movementType)
    //    {
    //        _context.MovementTypes.Add(movementType);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetMovementType", new { id = movementType.MovementTypeId }, movementType);
    //    }

    //    // DELETE: api/MovementTypes/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteMovementType(int id)
    //    {
    //        var movementType = await _context.MovementTypes.FindAsync(id);
    //        if (movementType == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.MovementTypes.Remove(movementType);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool MovementTypeExists(int id)
    //    {
    //        return _context.MovementTypes.Any(e => e.MovementTypeId == id);
    //    }
    //}
}
