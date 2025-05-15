namespace HotelAPI.Requests
{
    public class MovementRequest
    {
        public string Room { get; set; } = string.Empty;
        // Room's name (n°) 
        public int? UserId { get; set; } = null;           
        // Admin user who's resetting
        public int Amount { get; set; }
        // Amount of money involved in the operation
        // Reset is always zero
        public int? DrinkId { get; set; } = null;
        // Id of drink involved in transaction
        public int? GameId { get; set; } = null;
        // Id of game involved in transaction
        public int? PlayerNumber { get; set; } = 1;
        // Player Number to add coin to
    }
}
