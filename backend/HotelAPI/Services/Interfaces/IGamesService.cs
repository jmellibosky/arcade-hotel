using HotelAPI.Models;
using HotelAPI.Responses;

namespace HotelAPI.Services.Interfaces
{
    public interface IGamesService
    {
        public Task<Game?> Get(int id);

        public Task<List<GameResponse>> GetAll();
    }
}
