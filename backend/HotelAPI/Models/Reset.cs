using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Reset
{
    public int ResetId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int MovementId { get; set; }

    [JsonIgnore]
    public virtual Movement Movement { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
