using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilebACKEND.ViewModel
{
    public class FoodDTO: EntityData
    {
        public string Name { get; set; }
        public long Kkal { get; set; }
        public long Weight { get; set; }
        public long Proteins { get; set; }
        public long Fats { get; set; }
        public long Carbohydrates { get; set; }
        public string Image { get; set; }
    }
}