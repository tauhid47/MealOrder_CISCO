using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    public class Restaurant
    {
        public string Name { get; set; }
        public float Rating { get; set; }
        public List<Meal> Meals { get; set; }

        public Restaurant(string name, float rating, List<Meal> meals)
        {
            this.Name = name;
            this.Rating = rating;
            this.Meals = meals;
        }

        public List<Meal> getMealsWithFeature(String features)
        {
            List<Meal> mealsToReturn = new List<Meal>();
            if (features.Length == 0)
            {
                var mealsWithFeatures = this.Meals.Where(x => x.Features.Count() == 0);
                if (mealsWithFeatures.Count() > 0) mealsToReturn = mealsWithFeatures.ToList();
            }
            else
            {
                string[] featureArray = features.Split(',');
                var mealsWithFeatures = this.Meals.Where(x => x.Features.Count() > 0 && featureArray.All(y => x.Features.Contains(y.Trim())));
                if (mealsWithFeatures.Count() > 0) mealsToReturn = mealsWithFeatures.ToList();
            }
            return mealsToReturn;
        }
    }
}
