using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ArcadeHotelContext _context;

        public GamesController(ArcadeHotelContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            try
            {
                List<GameResponse> Response = await _context.Games
                    .Where(g => g.DeletedAt == null)
                    .Select(g => new GameResponse()
                    {
                        GameId = g.GameId,
                        Name = g.Name,
                        Price = g.Price,
                        Img = g.ImageUrl,
                        Machines = g.Arcades
                            .Select(a => new MachineResponse()
                            {
                                Id = a.ArcadeId,
                                Name = a.Name,
                                Description = "",
                                Img = "",
                                Stock = true
                            })
                            .ToList()
                    })
                    .ToListAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        //// GET: api/Games/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Game>> GetGame(int id)
        //{
        //    Game? game = await _context.Games.FindAsync(id);

        //    return game == null ? (ActionResult<Game>)NotFound() : (ActionResult<Game>)game;
        //}

        //// PUT: api/Games/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.GameId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
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

        //// POST: api/Games
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Game>> PostGame(Game game)
        //{
        //    _context.Games.Add(game);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGame", new { id = game.GameId }, game);
        //}

        //// DELETE: api/Games/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGame(int id)
        //{
        //    Game? game = await _context.Games.FindAsync(id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Games.Remove(game);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool GameExists(int id)
        //{
        //    return _context.Games.Any(e => e.GameId == id);
        //}
    }
}
