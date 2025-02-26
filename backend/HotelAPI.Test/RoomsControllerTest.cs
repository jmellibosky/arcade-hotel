using HotelAPI.Controllers;
using HotelAPI.Models;
using HotelAPI.Responses;
using HotelAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Test
{
    public class RoomsControllerTest
    {
        private readonly RoomsController _controller;

        public RoomsControllerTest()
        {
            ArcadeHotelContext context = new ArcadeHotelContext(new DbContextOptionsBuilder<ArcadeHotelContext>().UseSqlServer("internet85").Options);

            _controller = new RoomsController(new RoomsService(context));
        }

        [Fact]
        public async Task GetAll()
        {
            var controllerResult = await _controller.GetRooms();

            var result = Assert.IsType<OkObjectResult>(controllerResult.Result);

            var value = Assert.IsType<List<RoomResponse>>(result.Value);

            Assert.NotEmpty(value);
        }
    }
}
