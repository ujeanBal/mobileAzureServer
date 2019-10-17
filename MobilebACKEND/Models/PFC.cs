using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobilebACKEND.Models
{
    //  [ComplexType]
    public class PFC
    {
        public int Id { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public int Carbohydrates { get; set; }

        [JsonIgnore]
       public Food Owner { get; set; }
    }
}