using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    public class MealOrderGenerator
    {
        private static bool allMealFulfilled;
        private static Dictionary<string, int> featureFulfilled;
        private static List<Order> generatedOrders;

        public static List<Order> GenerateOrders(List<Restaurant> restaurants, Dictionary<string, int> mealRequirements)
        {
            featureFulfilled = new Dictionary<string, int>();
            generatedOrders = new List<Order>();
            if (restaurants.Count() > 0)
            {
                //int generalMealFulfilled = 0;
                restaurants = restaurants.OrderByDescending(x => x.Rating).ThenBy(x => x.Meals.Count()).ToList();
                foreach (Restaurant r in restaurants)
                {
                    allMealFulfilled = true;
                    Order orderFromSinglerestaurant = GenerateOrderFromSingleRestaurant(r, mealRequirements);
                    if (orderFromSinglerestaurant != null) generatedOrders.Add(orderFromSinglerestaurant);
                    if (allMealFulfilled) break;
                }
            }
            else Console.WriteLine("No restaurant provided");
            return generatedOrders;
        }

        private static Order GenerateOrderFromSingleRestaurant(Restaurant r, Dictionary<string, int> mealRequirements)
        {
            Order orderFromSinglerestaurant = null;
            List<Meal> allMealsInOrder = new List<Meal>();
            foreach (var singleFeatureRequirement in mealRequirements)
            {
                if (!featureFulfilled.ContainsKey(singleFeatureRequirement.Key)) featureFulfilled.Add(singleFeatureRequirement.Key, 0);
                //List<Meal> mealsAvailable = string.IsNullOrEmpty(singleFeatureRequirement.Key) ? r.getMealsWithFeature("") : r.getMealsWithFeature(singleFeatureRequirement.Key);
                List<Meal> mealsAvailable = r.getMealsWithFeature(singleFeatureRequirement.Key);
                int numMealsToFulfill = singleFeatureRequirement.Value - featureFulfilled[singleFeatureRequirement.Key];
                if (numMealsToFulfill > 0 && mealsAvailable.Count > 0)
                {
                    List<Meal> singleFeatureMeal = GenerateSingleFeatureMealOrder(mealsAvailable, singleFeatureRequirement, numMealsToFulfill);
                    allMealsInOrder.AddRange(singleFeatureMeal);
                    featureFulfilled[singleFeatureRequirement.Key] += singleFeatureMeal.Count;
                    // If featured meals to fulfill, keep going through restaurants
                    if ((singleFeatureRequirement.Value - featureFulfilled[singleFeatureRequirement.Key]) > 0) allMealFulfilled = false;
                }
            }
            if (allMealsInOrder.Count() > 0)
                orderFromSinglerestaurant = new Order
                {
                    FromRestaurant = r,
                    MealsOrdered = allMealsInOrder
                };
            return orderFromSinglerestaurant;
        }

        private static List<Meal> GenerateSingleFeatureMealOrder(List<Meal> mealsAvailable, KeyValuePair<string, int> singleFeatureRequirement, int numMealsToFulfill)
        {
            List<Meal> mealsToOrder = new List<Meal>();
            int numMealsAvailable = mealsAvailable.Count;
            int numMealToOrder = numMealsToFulfill >= numMealsAvailable ? numMealsAvailable : numMealsToFulfill;
            mealsToOrder = mealsAvailable.Take(numMealToOrder).ToList();
            return mealsToOrder;
        }
    }
}
