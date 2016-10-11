using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    /**
        An entity to represent a meal. Each meal can have a number of features.
        In this project, the feature list stays empty if it is a general meal.
    */
    public class Meal
    {
        public string Name { get; set; }
        public List<string> Features { get; set; }

        // Constructor for general Meal
        public Meal(string name)
        {
            this.Name = name;
            this.Features = new List<string>(); // Initiate empty feature list for general meal
        }

        // Constructor for featured meals
        public Meal(string name, List<string> features)
        {
            this.Name = name;
            this.Features = features;
        }
    }
}
