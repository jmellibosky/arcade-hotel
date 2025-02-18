using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly ArcadeHotelContext _context;

        public DrinksController(ArcadeHotelContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkResponse>>> GetDrinks()
        {
            try
            {
                List<DrinkResponse> Response = await _context.Drinks
                .Where(d => d.DeletedAt == null)
                .Select(d => new DrinkResponse()
                {
                    DrinkId = d.DrinkId,
                    Name = d.Name,
                    Price = d.Price,
                    Img = d.ImageUrl,
                    Machines = new List<MachineResponse>()
                })
                .ToListAsync();

                foreach (DrinkResponse DrinkItem in Response)
                {
                    List<Slot> Slots = await _context.Slots
                        .Where(s => s.DrinkId == DrinkItem.DrinkId && s.DeletedAt == null)
                        .Include(s => s.Machine)
                        .ToListAsync();

                    foreach (Slot SlotItem in Slots)
                    {
                        DrinkItem.Machines.Add(new MachineResponse()
                        {
                            Id = SlotItem.Machine.MachinesId,
                            Name = SlotItem.Machine.Name,
                            Description = SlotItem.Machine.Description,
                            Img = SlotItem.Machine.ImageUrl,
                            Stock = SlotItem.HasStock
                        });
                    }
                }

                return Ok(Response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //// GET: api/Drinks/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Drink>> GetDrink(int id)
        //{
        //    var drink = await _context.Drinks.FindAsync(id);

        //    if (drink == null)
        //    {
        //        return NotFound();
        //    }

        //    return drink;
        //}

        //// PUT: api/Drinks/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDrink(int id, Drink drink)
        //{
        //    if (id != drink.DrinkId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(drink).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DrinkExists(id))
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

        //// POST: api/Drinks
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        //{
        //    _context.Drinks.Add(drink);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDrink", new { id = drink.DrinkId }, drink);
        //}

        //// DELETE: api/Drinks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDrink(int id)
        //{
        //    var drink = await _context.Drinks.FindAsync(id);
        //    if (drink == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Drinks.Remove(drink);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        ////}

        //private bool DrinkExists(int id)
        //{
        //    return _context.Drinks.Any(e => e.DrinkId == id);
        //}
    }
}
