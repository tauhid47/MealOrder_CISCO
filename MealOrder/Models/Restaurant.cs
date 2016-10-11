using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    /**
        A restaurant contains a name, rating and associated meals, as instructed in the problem.
    */
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

        /*
            Method to return meals with specified feature(s).
            If looking for meals with multiple feature constraints, pass them as comma-separated string.
            If looking for a general meal, pass an empty string
            This method will return only one type of meals.
        */
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
