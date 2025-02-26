using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using HotelAPI.Services.Interfaces;
using HotelAPI.Services.Implementations;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinksService _drinks;

        public DrinksController(IDrinksService drinks)
        {
            _drinks = drinks;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkResponse>>> GetDrinks()
        {Console.WriteLine("run");
            try
            {
                var Response = await _drinks.GetAll();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
