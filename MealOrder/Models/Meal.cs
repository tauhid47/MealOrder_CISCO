using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    public class Meal
    {
        public string Name { get; set; }
        public List<string> Features { get; set; }

        public Meal(string name)
        {
            this.Name = name;
            this.Features = new List<string>();
        }
        public Meal(string name, List<string> features)
        {
            this.Name = name;
            this.Features = features;
        }
    }
}
