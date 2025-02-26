using HotelAPI.Models;
using HotelAPI.Responses;
using HotelAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services.Implementations
{
    public class GamesService : IGamesService
    {
        private readonly ArcadeHotelContext _context;

        public GamesService(ArcadeHotelContext context)
        {
            _context = context;
        }

        public async Task<Game?> Get(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.GameId == id);
        }

        public async Task<List<GameResponse>> GetAll()
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

            return Response;
        }
    }
}
