using HotelAPI.Models;
using HotelAPI.Responses;

namespace HotelAPI.Services.Interfaces
{
    public interface IRoomsService
    {
        public Task<Room?> Get(int id);

        public Task<List<RoomResponse>> GetAll();
    }
}
