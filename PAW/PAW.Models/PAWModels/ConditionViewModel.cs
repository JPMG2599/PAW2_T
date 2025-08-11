using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAW.Models.PAWModels
{
    public class ConditionViewModel
    {
        public string Criteria { get; set; }  
        public string Property { get; set; } 
        public string? Value { get; set; }     
        public decimal Start { get; set; }    
        public decimal End { get; set; }
    }
}
