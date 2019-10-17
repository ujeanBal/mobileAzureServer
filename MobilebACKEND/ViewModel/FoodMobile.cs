using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilebACKEND.ViewModel
{
    public class FoodMobile: EntityData
    {
        public string Name { get; set; }
        public string Kkal { get; set; }
        public string Weight { get; set; }
        public string Proteins { get; set; }
        public string Fats { get; set; }
        public string Carbohydrates { get; set; }
    }
}