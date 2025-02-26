using HotelAPI.Models;
using HotelAPI.Responses;
using HotelAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services.Implementations
{
    public class RoomsService : IRoomsService
    {
        private readonly ArcadeHotelContext _context;

        public RoomsService(ArcadeHotelContext context)
        {
            _context = context;
        }

        public async Task<Room?> Get(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomId == id);
        }

        public async Task<List<RoomResponse>> GetAll()
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

            return Response;
        }
    }
}
