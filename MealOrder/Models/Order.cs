using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrder
{
    public class Order
    {
        public Restaurant FromRestaurant { get; set; }
        public List<Meal> MealsOrdered { get; set; }
    }
}
