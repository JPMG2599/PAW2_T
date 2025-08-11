using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PAW.Models;

public partial class UserRole
{
    [JsonPropertyName("id")]
    public decimal? Id { get; set; }

    [JsonPropertyName("roldId")]
    public decimal? RoldId { get; set; }

    [JsonPropertyName("userId")]
    public decimal? UserId { get; set; }
}
