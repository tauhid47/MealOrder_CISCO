using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MealOrder;
using System.Collections.Generic;
using System.Linq;

namespace MealOrderTests
{
    [TestClass]
    public class MealOrderUnitTest
    {
        // Test whether getMealsWithFeature returns right results with the provided meal feature
        [TestMethod]
        public void ShouldReturnMealWithFeature()
        {
            List<Meal> meals = new List<Meal>();
            Meal BurgerMeal = new Meal("Burger");
            meals.Add(BurgerMeal);
            List<string> vegetarianFeatures = new List<string>() { "Vegetarian" };
            Meal VegeBurgerMeal = new Meal("Vegetarian Burger", vegetarianFeatures);          
            meals.Add(VegeBurgerMeal);
            Restaurant aRestaurant = new Restaurant("Fat Burger", 4, meals);
            int result = aRestaurant.getMealsWithFeature("Vegetarian").Count;
            Assert.AreEqual(1, result);
        }

        // Test whether right orders are generated. I this is the exact scenarion given to me as an example with the coding requirements
        [TestMethod]
        public void ShouldGenerateCompleteMealOrder()
        {
            // Requirements
            int numRequriedVegeMeals = 5;
            int numRequiredGlutenFreeMeal = 7;
            int numRequiredTotalMeals = 50;
            Dictionary<string, int> featureRequirements = new Dictionary<string, int>();
            featureRequirements.Add("Vegetarian, Nut-Free", numRequriedVegeMeals);
            featureRequirements.Add("Gluten-Free", numRequiredGlutenFreeMeal);
            featureRequirements.Add("", numRequiredTotalMeals - numRequriedVegeMeals - numRequiredGlutenFreeMeal);

            // Data set
            List<Restaurant> restaurants = GenerateTestRestaurantData();

            // Compare Results
            List<Order> generatedOrders = MealOrderGenerator.GenerateOrders(restaurants, featureRequirements);
            int restAVegeCount = GetTestFeatureCount("Restaurant A", generatedOrders, "Vegetarian, Nut-Free");
            int restAGeneralCount = GetTestFeatureCount("Restaurant A", generatedOrders, "");

            int restBVegeCount = GetTestFeatureCount("Restaurant B", generatedOrders, "Vegetarian, Nut-Free");
            int restBGlutenFreeCount = GetTestFeatureCount("Restaurant B", generatedOrders, "Gluten-Free");
            int restBGeneralCount = GetTestFeatureCount("Restaurant B", generatedOrders, "");

            Assert.AreEqual(4, restAVegeCount);
            Assert.AreEqual(36, restAGeneralCount);

            Assert.AreEqual(1, restBVegeCount);
            Assert.AreEqual(7, restBGlutenFreeCount);
            Assert.AreEqual(2, restBGeneralCount);
        }

        private int GetTestFeatureCount(string restaurantName, List<Order> generatedOrders, string feature)
        {
            int mealCount = 0;
            var restaurantAOrders = generatedOrders.Where(x => x.FromRestaurant.Name == restaurantName);
            if (restaurantAOrders.Count() > 0)
            {
                var generatedRestaurantAMeals = restaurantAOrders.First().MealsOrdered;
                if (feature != "")
                {
                    string[] featureArray = feature.Split(',');
                    mealCount = generatedRestaurantAMeals.Where(x=>featureArray.All(y => x.Features.Contains(y.Trim()))).Count();
                }
                else mealCount = generatedRestaurantAMeals.Where(x => x.Features.Count == 0).Count();
            }
            return mealCount;
        }

        private List<Restaurant> GenerateTestRestaurantData()
        {
            // Restaurant A
            string[] vegeFeature = { "Vegetarian", "Nut-Free" };
            string[] GlutenFreeFeature = { "Gluten-Free" };
            string[] GeneralFeature = { };
            List<Meal> restaurantAMeals = GenerateTestMealsData(4, vegeFeature);
            restaurantAMeals.AddRange(GenerateTestMealsData(36, GeneralFeature));
            Restaurant restaurantA = new Restaurant("Restaurant A", 5, restaurantAMeals);

            // Restaurant B
            List<Meal> restaurantBMeals = GenerateTestMealsData(20, vegeFeature);
            restaurantBMeals.AddRange(GenerateTestMealsData(20, GlutenFreeFeature));
            restaurantBMeals.AddRange(GenerateTestMealsData(60, GeneralFeature));
            Restaurant restaurantB = new Restaurant("Restaurant B", 3, restaurantBMeals);

            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants.Add(restaurantA);
            restaurants.Add(restaurantB);
            return restaurants;
        }

        private List<Meal> GenerateTestMealsData(int numMeal, string[] features)
        {
            List<Meal> mealsToReturn = new List<Meal>();
            for(int i = 0; i< numMeal; i++)
            {
                if (features.Length == 0)
                    mealsToReturn.Add(new Meal("Meal-" + i));
                else
                {
                    List<string> mealFeatures = new List<string>();
                    foreach(string aFeature in features)
                        mealFeatures.Add(aFeature);
                    mealsToReturn.Add(new Meal("Meal-" + i, mealFeatures));
                }
            }
            return mealsToReturn;
        }
    }
}
