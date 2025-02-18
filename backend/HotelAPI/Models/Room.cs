using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelAPI.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int Password { get; set; }

    public int? LastMovementId { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual Movement? LastMovement { get; set; }
}
