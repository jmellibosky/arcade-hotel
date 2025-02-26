using HotelAPI.Models;
using HotelAPI.Responses;
using HotelAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Services.Implementations
{
    public class DrinksService : IDrinksService
    {
        private readonly ArcadeHotelContext _context;

        public DrinksService(ArcadeHotelContext context)
        {
            _context = context;
        }

        public async Task<Drink?> Get(int id)
        {
            return await _context.Drinks.FirstOrDefaultAsync(d => d.DrinkId == id);
        }

        public async Task<List<DrinkResponse>> GetAll()
        {
            List<DrinkResponse> Response = await _context.Drinks
                .Where(d => d.DeletedAt == null)
                .Select(d => new DrinkResponse()
                {
                    DrinkId = d.DrinkId,
                    Name = d.Name,
                    Price = d.Price,
                    Img = d.ImageUrl,
                    Machines = new List<MachineResponse>()
                })
                .ToListAsync();

                foreach (DrinkResponse DrinkItem in Response)
                {
                    List<Slot> Slots = await _context.Slots
                        .Where(s => s.DrinkId == DrinkItem.DrinkId && s.DeletedAt == null)
                        .Include(s => s.Machine)
                        .ToListAsync();

                    foreach (Slot SlotItem in Slots)
                    {
                        DrinkItem.Machines.Add(new MachineResponse()
                        {
                            Id = SlotItem.Machine.MachinesId,
                            Name = SlotItem.Machine.Name,
                            Description = SlotItem.Machine.Description,
                            Img = SlotItem.Machine.ImageUrl,
                            Stock = SlotItem.HasStock
                        });
                    }
                }

            return Response;
        }
    }
}
