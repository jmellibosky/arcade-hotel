namespace HotelAPI.Requests
{
    public class PasswordRequest
    {
        public string Room { get; set; }
        // Room's name (n°)
        public string Pass { get; set; }
        // New password
        // Must be numeric and Length = 4
    }
}
