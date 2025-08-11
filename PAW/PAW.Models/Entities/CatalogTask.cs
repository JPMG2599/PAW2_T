using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PAW.Data.Models;

public partial class CatalogTask
{
    [JsonPropertyName("id")]
    public decimal Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("taskId")]
    public decimal TaskId { get; set; }

    [JsonPropertyName("status")]
    public decimal Status { get; set; }

    [JsonPropertyName("modifiedDate")]
    public DateTime ModifiedDate { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("modifiedBy")]
    public string ModifiedBy { get; set; } = null!;

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; } = null!;

    [JsonPropertyName("taskType")]
    public decimal? TaskType { get; set; }
}
