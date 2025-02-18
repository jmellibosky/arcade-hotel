using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Arcade
{
    public int ArcadeId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? GameId { get; set; }

    [JsonIgnore]
    public virtual Game? Game { get; set; }
}
