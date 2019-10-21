using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilebACKEND.Models
{
    //  [ComplexType]
    public class PFC
    {
        public int Id { get; set; }
        public long Proteins { get; set; }
        public long Fats { get; set; }
        public long Carbohydrates { get; set; }

        [JsonIgnore]
        public Food Owner { get; set; }
    }
}