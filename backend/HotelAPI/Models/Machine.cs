using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Machine
{
    public int MachinesId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string ImageUrl { get; set; }

    [JsonIgnore]
    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
