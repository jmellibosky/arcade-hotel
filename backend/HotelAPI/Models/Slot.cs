using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Slot
{
    public int SlotId { get; set; }

    public int Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int MachineId { get; set; }

    public bool HasStock { get; set; }

    public int? DrinkId { get; set; }

    [JsonIgnore]
    public virtual Drink? Drink { get; set; }

    [JsonIgnore]
    public virtual Machine Machine { get; set; } = null!;
}
