using Microsoft.Identity.Client;

namespace HotelAPI.Requests
{
    public class LogoutRequest
    {
        public string User { get; set; }
        public string Key { get; set; }
    }
}
