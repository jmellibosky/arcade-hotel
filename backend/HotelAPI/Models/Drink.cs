using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Drink
{
    public int DrinkId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }

    [JsonIgnore]
    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();

    [JsonIgnore]
    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
