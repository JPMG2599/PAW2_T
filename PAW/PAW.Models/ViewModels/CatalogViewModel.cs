using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW.Models.ViewModels
{
    public class CatalogViewModel
    {
        [JsonPropertyName("tempID")]
        public int TempID { get; set; }

        [JsonPropertyName("identifier")]
        public int Identifier { get; set; }

        [Required]
        [JsonPropertyName("name")]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [JsonPropertyName("sku")]
        [Display(Name = "SKU")]
        public string? Sku { get; set; }

        [JsonPropertyName("createdDate")]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [JsonPropertyName("createdBy")]
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("rating")]
        [Display(Name = "Rating")]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal? Rating { get; set; }
    }
}
