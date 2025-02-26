using HotelAPI.Models;
using HotelAPI.Requests;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Services.Interfaces
{
    public interface IRoomsService
    {
        public Task<Room?> Get(int id);

        public Task<List<RoomResponse>> GetAll();
        
        public Task<IActionResult> ChangePassword(PasswordRequest request);
    }
}
