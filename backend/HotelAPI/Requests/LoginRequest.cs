using Microsoft.Identity.Client;

namespace HotelAPI.Requests
{
    public class LoginRequest
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }
}
