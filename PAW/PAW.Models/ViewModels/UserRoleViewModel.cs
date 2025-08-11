using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PAW.Models.ViewModels
{
    public class UserRoleViewModel
    {
        [JsonPropertyName("tempID")]
        public int TempID { get; set; }

        [JsonPropertyName("id")]
        public decimal? Id { get; set; }

        [JsonPropertyName("roldId")]
        public decimal? RoldId { get; set; }

        [JsonPropertyName("userId")]
        public decimal? UserId { get; set; }
    }
}
