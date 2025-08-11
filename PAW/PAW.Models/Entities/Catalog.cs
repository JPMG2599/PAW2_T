using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PAW.Models;
public partial class Catalog
{
    [JsonPropertyName("identifier")]
    public int Identifier { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("rating")]
    public decimal? Rating { get; set; }
}
