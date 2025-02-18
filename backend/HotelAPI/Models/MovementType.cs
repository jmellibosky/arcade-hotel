using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class MovementType
{
    public int MovementTypeId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
}
