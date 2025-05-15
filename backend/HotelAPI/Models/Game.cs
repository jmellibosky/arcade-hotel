using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }

    public int? MaxPlayers { get; set; }

    [JsonIgnore]
    public virtual ICollection<Arcade> Arcades { get; set; } = new List<Arcade>();

    [JsonIgnore]
    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
}
