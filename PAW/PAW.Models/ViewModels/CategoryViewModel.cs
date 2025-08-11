using PAW.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW.Models.ViewModels
{
    public class CategoryViewModel
    {
        [JsonPropertyName("tempID")]
        public int TempID { get; set; }

        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("categoryName")]
        [Display(Name = "Name")]
        public string? CategoryName { get; set; }

        [JsonPropertyName("description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [JsonPropertyName("lastModified")]
        [Display(Name = "Last Modified")]
        public DateTime? LastModified { get; set; }

        [JsonPropertyName("modifiedBy")]
        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }
    }
}
