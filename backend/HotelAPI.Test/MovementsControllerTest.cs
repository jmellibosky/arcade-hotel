using HotelAPI.Controllers;
using HotelAPI.Enums;
using HotelAPI.Models;
using HotelAPI.Requests;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Test
{
    public class MovementsControllerTest
    {
        private readonly MovementsController _controller;

        public MovementsControllerTest()
        {
            _controller = new MovementsController(
                    new ArcadeHotelContext(
                        new DbContextOptionsBuilder<ArcadeHotelContext>()
                            .UseSqlServer("internet85").Options));
        }

        #region Get Tests
        [Fact]
        public async Task GetByRoom_Ok()
        {
            var controllerResult = await _controller.GetMovementsByRoom(303);

            var result = Assert.IsType<OkObjectResult>(controllerResult.Result);

            var value = Assert.IsType<List<HistoryResponse>>(result.Value);

            Assert.NotEmpty(value);
        }

        [Fact]
        public async Task GetByRoom_NotFound_Room()
        {
            var controllerResult = await _controller.GetMovementsByRoom(9999999);

            var result = Assert.IsType<NotFoundObjectResult>(controllerResult.Result);
        }
        #endregion

        #region Reset Tests
        [Fact]
        public async Task Reset_Ok()
        {
            MovementRequest request = new MovementRequest()
            {
                UserId = 1,
                Room = "303"
            };

            var result = await _controller.Reset(request);

            var resultValue = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultObject = Assert.IsType<Movement>(resultValue.Value);

            Assert.Equal((int)MovementTypes.Reset, resultObject.MovementTypeId);
            Assert.Equal(0, resultObject.Balance);
        }

        [Fact]
        public async Task Reset_NotFound_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                UserId = 1,
                Room = "999"
            };

            var result = await _controller.Reset(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Reset_NotFound_User()
        {
            MovementRequest request = new MovementRequest()
            {
                UserId = 9999999,
                Room = "303"
            };

            var result = await _controller.Reset(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Reset_BadRequest_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                UserId = 1
            };

            var result = await _controller.Reset(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Reset_BadRequest_User()
        {
            MovementRequest request = new MovementRequest()
            {
                Room = "303"
            };

            var result = await _controller.Reset(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion

        #region Deposit Tests
        [Fact]
        public async Task Deposit_Ok()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303"
            };

            var result = await _controller.Deposit(request);

            var resultValue = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultObject = Assert.IsType<Movement>(resultValue.Value);

            Assert.Equal(10, resultObject.Amount);
            Assert.Equal((int)MovementTypes.Deposit, resultObject.MovementTypeId);
        }

        [Fact]
        public async Task Deposit_NotFound_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "999"
            };

            var result = await _controller.Deposit(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Deposit_BadRequest_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10
            };

            var result = await _controller.Deposit(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public async Task Deposit_BadRequest_Amount(int _amount)
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = _amount,
                Room = "303"
            };

            var result = await _controller.Deposit(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion

        #region Extraction Tests
        [Fact]
        public async Task Extraction_Ok()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303"
            };

            var result = await _controller.Extraction(request);

            var resultValue = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultObject = Assert.IsType<Movement>(resultValue.Value);

            Assert.Equal(10, resultObject.Amount);
            Assert.Equal((int)MovementTypes.Extraction, resultObject.MovementTypeId);
        }

        [Fact]
        public async Task Extraction_NotFound_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "999"
            };

            var result = await _controller.Extraction(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Extraction_BadRequest_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10
            };

            var result = await _controller.Extraction(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public async Task Extraction_BadRequest_Amount(int _amount)
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = _amount,
                Room = "303"
            };

            var result = await _controller.Extraction(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion

        #region Transaction Tests
        [Fact]
        public async Task Transaction_Ok_Drink()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303",
                DrinkId = 1
            };

            var result = await _controller.Transaction(request);

            var resultValue = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultObject = Assert.IsType<Movement>(resultValue.Value);

            Assert.Equal(10, resultObject.Amount);
            Assert.Equal(1, resultObject.DrinkId);
            Assert.Equal((int)MovementTypes.Extraction, resultObject.MovementTypeId);
        }

        [Fact]
        public async Task Transaction_Ok_Game()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303",
                GameId = 1
            };

            var result = await _controller.Transaction(request);

            var resultValue = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultObject = Assert.IsType<Movement>(resultValue.Value);

            Assert.Equal(10, resultObject.Amount);
            Assert.Equal(1, resultObject.GameId);
        }

        [Fact]
        public async Task Transaction_NotFound_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "999",
                GameId = 1
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Transaction_NotFound_Drink()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303",
                DrinkId = 9999999
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Transaction_NotFound_Game()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303",
                GameId = 9999999
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Transaction_BadRequest_Room()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                GameId = 9999999
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Transaction_BadRequest_Item()
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = 10,
                Room = "303"
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public async Task Transaction_BadRequest_Amount(int _amount)
        {
            MovementRequest request = new MovementRequest()
            {
                Amount = _amount,
                Room = "303",
                GameId = 1
            };

            var result = await _controller.Transaction(request);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
    }
}
