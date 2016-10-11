using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    /**
        For this prject, I assumed that only one order is placed in a restaurant.
        This order includes a list of meals with specified features
    */
    public class Order
    {
        public Restaurant FromRestaurant { get; set; }
        public List<Meal> MealsOrdered { get; set; }
    }
}
