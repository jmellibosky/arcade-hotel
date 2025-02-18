using HotelAPI.Controllers;
using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Test
{
    public class DrinksControllerTest
    {
        private readonly DrinksController _controller;

        public DrinksControllerTest()
        {
            _controller =
                new DrinksController(
                    new ArcadeHotelContext(
                        new DbContextOptionsBuilder<ArcadeHotelContext>()
                            .UseSqlServer("internet85").Options));
        }

        [Fact]
        public async Task GetAll()
        {
            var controllerResult = await _controller.GetDrinks();

            var result = Assert.IsType<OkObjectResult>(controllerResult.Result);

            var value = Assert.IsType<List<DrinkResponse>>(result.Value);

            Assert.NotEmpty(value);
        }
    }
}
