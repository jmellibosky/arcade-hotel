namespace HotelAPI.Responses
{
    public class GameResponse
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public List<MachineResponse> Machines { get; set; }
        public int Players { get; set; }
    }
}
