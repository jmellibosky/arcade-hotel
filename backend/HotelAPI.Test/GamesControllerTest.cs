using HotelAPI.Controllers;
using HotelAPI.Models;
using HotelAPI.Responses;
using HotelAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Test
{
    public class GamesControllerTest
    {
        private readonly GamesController _controller;

        public GamesControllerTest()
        {
            ArcadeHotelContext context = new ArcadeHotelContext(new DbContextOptionsBuilder<ArcadeHotelContext>().UseSqlServer("internet85").Options);

            _controller = new GamesController(new GamesService(context));
        }

        [Fact]
        public async Task GetAll()
        {
            var controllerResult = await _controller.GetGames();

            var result = Assert.IsType<OkObjectResult>(controllerResult.Result);

            var value = Assert.IsType<List<GameResponse>>(result.Value);

            Assert.NotEmpty(value);
        }
    }
}
