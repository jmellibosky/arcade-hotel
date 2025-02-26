using HotelAPI.Models;
using HotelAPI.Responses;

namespace HotelAPI.Services.Interfaces
{
    public interface IDrinksService
    {
        public Task<Drink?> Get(int id);

        public Task<List<DrinkResponse>> GetAll();
    }
}
