using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW.Models.ViewModels
{
    public class InventoryViewModel
    {
        [JsonPropertyName("tempID")]
        public int TempID { get; set; }

        [JsonPropertyName("inventoryId")]
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }

        [JsonPropertyName("unitPrice")]
        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [JsonPropertyName("unitsInStock")]
        [Display(Name = "Unit In Stock")]
        public int? UnitsInStock { get; set; }

        [JsonPropertyName("lastUpdated")]
        [Display(Name = "Last Updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonPropertyName("dateAdded")]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [JsonPropertyName("modifyBy")]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }
    }
}
