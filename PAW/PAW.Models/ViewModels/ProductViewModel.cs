using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW.Models.ViewModels
{
    public class ProductViewModel
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [Required, JsonPropertyName("productName")]
        [Display(Name = "Name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("inventoryId")]
        public int? InventoryId { get; set; }
        [JsonPropertyName("supplierId")]
        public int? SupplierId { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [Range(0, 5), JsonPropertyName("rating")]
        public decimal? Rating { get; set; }
        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
        [JsonPropertyName("lastModified")]
        public DateTime? LastModified { get; set; }
        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }
        [JsonPropertyName("unitsinstock")]
        public int? UnitsInStock { get; set; }

    }
}
