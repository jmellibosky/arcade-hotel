namespace HotelAPI.Responses
{
    public class DrinkResponse
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public List<MachineResponse> Machines { get; set; }
    }
}
