using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Movement
{
    public int MovementId { get; set; }

    public DateTime CreatedAt { get; set; }

    public double Amount { get; set; }

    public double Balance { get; set; }

    public int MovementTypeId { get; set; }

    public int? DrinkId { get; set; }

    public int? GameId { get; set; }

    public int RoomId { get; set; }

    public int? LastResetId { get; set; }

    [JsonIgnore]
    public virtual Drink? Drink { get; set; }

    [JsonIgnore]
    public virtual Game? Game { get; set; }

    [JsonIgnore]
    public virtual Reset? LastReset { get; set; }

    [JsonIgnore]
    public virtual MovementType MovementType { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Reset> Resets { get; set; } = new List<Reset>();

    [JsonIgnore]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
