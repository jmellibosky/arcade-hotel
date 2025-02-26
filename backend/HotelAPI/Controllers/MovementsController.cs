using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelAPI.Models;
using HotelAPI.Responses;
using Microsoft.IdentityModel.Tokens;
using HotelAPI.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using HotelAPI.Enums;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly ArcadeHotelContext _context;

        public MovementsController(ArcadeHotelContext context)
        {
            _context = context;
        }

        private static string GetItemName(Movement m)
        {
            if (m.MovementTypeId == 1)
            {
                // Reset
                return "1";
            }
            else if (m.MovementTypeId == 2)
            {
                // Deposit
                return "2";
            }
            else if (m.Game != null)
            {
                return m.Game.Name;
            }
            else if (m.Drink != null)
            {
                return m.Drink.Name;
            }
            else if (m.MovementTypeId == 3)
            {
                // Extraction
                return "3";
            }
            else
            {
                // Other
                return "-1";
            }
        }

        // GET: api/Movements/5?reset=false
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HistoryResponse>>> GetMovementsByRoom(int id, bool reset = false)
        {
            try
            {
                Room? Room = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .FirstOrDefaultAsync(r => r.Name == id.ToString());
                if (Room == null) return NotFound("Room not found.");

                List<HistoryResponse> Response = await _context.Movements
                    .OrderByDescending(m => m.MovementId)
                    .Include(m => m.Game)
                    .Include(m => m.Drink)
                    .Include(m => m.LastReset)
                    .Where(m => m.RoomId == Room.RoomId 
                        && m.MovementTypeId != (int)MovementTypes.Reset
                        && (reset || m.LastResetId == Room.LastMovement.LastResetId))
                    .Select(m => new HistoryResponse()
                    {
                        Item = GetItemName(m),
                        Price = m.Amount,
                        Time = m.CreatedAt
                    })
                    .ToListAsync();

                return Ok(Response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("reset")]
        public async Task<ActionResult<Movement>> Reset([FromBody] MovementRequest request)
        {
            if (string.IsNullOrEmpty(request.Room)) return BadRequest("Room is required.");
            if (request.UserId == null) return BadRequest("UserId is required.");
            if (!await _context.Users.AnyAsync(u => u.UserId == request.UserId)) return NotFound("User not found.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Room? RoomToReset = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .FirstOrDefaultAsync(r => r.Name == request.Room);

                if (RoomToReset == null) return NotFound("Room not found.");
                if (RoomToReset.LastMovement == null) return NotFound("Room has no last movement.");

                Movement ResetMovement = new Movement()
                {
                    RoomId = RoomToReset.RoomId,
                    Amount = RoomToReset.LastMovement == null ? 0 : -RoomToReset.LastMovement.Balance,
                    Balance = 0,
                    CreatedAt = DateTime.Now,
                    MovementTypeId = 1
                };

                await _context.Movements.AddAsync(ResetMovement);
                await _context.SaveChangesAsync();

                Reset CashReset = new Reset()
                {
                    CreatedAt = DateTime.Now,
                    UserId = request.UserId.GetValueOrDefault(),
                    MovementId = ResetMovement.MovementId
                };

                await _context.Resets.AddAsync(CashReset);
                await _context.SaveChangesAsync();

                ResetMovement.LastResetId = CashReset.ResetId;
                RoomToReset.LastMovementId = ResetMovement.MovementId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ResetMovement);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<ActionResult<Movement>> Deposit([FromBody] MovementRequest request)
        {
            if (string.IsNullOrEmpty(request.Room)) return BadRequest("Room is required.");
            if (request.Amount <= 0) return BadRequest("Amount must be over 0.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Room? RoomToDeposit = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .FirstOrDefaultAsync(r => r.Name == request.Room);

                if (RoomToDeposit == null) return NotFound("Room not found.");
                if (RoomToDeposit.LastMovement == null) return NotFound("Room has no last movement.");

                Movement DepositMovement = new Movement()
                {
                    RoomId = RoomToDeposit.RoomId,
                    Amount = request.Amount,
                    Balance = request.Amount + RoomToDeposit.LastMovement.Balance,
                    CreatedAt = DateTime.Now,
                    MovementTypeId = 2,
                    LastResetId = RoomToDeposit.LastMovement.LastResetId
                };

                await _context.Movements.AddAsync(DepositMovement);
                await _context.SaveChangesAsync();

                RoomToDeposit.LastMovementId = DepositMovement.MovementId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(DepositMovement);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("extraction")]
        public async Task<ActionResult<Movement>> Extraction([FromBody] MovementRequest request)
        {
            if (string.IsNullOrEmpty(request.Room)) return BadRequest("Room is required.");
            if (request.Amount <= 0) return BadRequest("Amount must be over 0.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Room? RoomToExtract = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .FirstOrDefaultAsync(r => r.Name == request.Room);

                if (RoomToExtract == null) return NotFound("Room not found.");
                if (RoomToExtract.LastMovement == null) return NotFound("Room has no last movement.");

                Movement ExtractMovement = new Movement()
                {
                    RoomId = RoomToExtract.RoomId,
                    Amount = request.Amount,
                    Balance = RoomToExtract.LastMovement.Balance - request.Amount,
                    CreatedAt = DateTime.Now,
                    MovementTypeId = 3,
                    LastResetId = RoomToExtract.LastMovement.LastResetId
                };

                await _context.Movements.AddAsync(ExtractMovement);
                await _context.SaveChangesAsync();

                RoomToExtract.LastMovementId = ExtractMovement.MovementId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(ExtractMovement);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("transaction")]
        public async Task<ActionResult<Movement>> Transaction([FromBody] MovementRequest request)
        {
            if (string.IsNullOrEmpty(request.Room)) return BadRequest("Room is required.");
            if (request.DrinkId == null && request.GameId == null) return BadRequest("Either Game or Drink must be provided.");
            if (request.DrinkId != null && !await _context.Drinks.AnyAsync(d => d.DrinkId == request.DrinkId)) return NotFound("Drink not found.");
            if (request.GameId != null && !await _context.Games.AnyAsync(g => g.GameId == request.GameId)) return NotFound("Game not found.");
            if (request.Amount <= 0) return BadRequest("Amount must be over 0");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Room? RoomToExtract = await _context.Rooms
                    .Include(r => r.LastMovement)
                    .FirstOrDefaultAsync(r => r.Name == request.Room);

                if (RoomToExtract == null) return NotFound("Room not found.");
                if (RoomToExtract.LastMovement == null) return NotFound("Room has no last movement.");

                Movement TransactionMovement = new Movement()
                {
                    RoomId = RoomToExtract.RoomId,
                    Amount = request.Amount,
                    Balance = RoomToExtract.LastMovement.Balance - request.Amount,
                    CreatedAt = DateTime.Now,
                    MovementTypeId = 3,
                    LastResetId = RoomToExtract.LastMovement.LastResetId,
                    DrinkId = request.DrinkId,
                    GameId = request.GameId
                };

                await _context.Movements.AddAsync(TransactionMovement);
                await _context.SaveChangesAsync();

                RoomToExtract.LastMovementId = TransactionMovement.MovementId;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(TransactionMovement);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }
}
