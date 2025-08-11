using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace PAW.Models;
public partial class Category
{
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("categoryName")]
    public string? CategoryName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTime? LastModified { get; set; }

    [JsonPropertyName("modifiedBy")]
    public string? ModifiedBy { get; set; }

    [JsonPropertyName("product")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
