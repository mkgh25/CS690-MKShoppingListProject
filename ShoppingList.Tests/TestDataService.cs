using shoppinglist.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingList.Tests
{
    public class TestDataService
    {
        public List<FoodItem> t_items;

        public TestDataService()
        {
            t_items = new List<FoodItem>
            {
                new FoodItem{ FoodName = "oranges", GroceryCategory = "fruit", Status = "needed"},
                new FoodItem{ FoodName = "tortillas", GroceryCategory = "bakery", Status = "have"}
            };
        }

        public void SaveFoodItemList(List<FoodItem> items)
        {
            t_items = items;
        }
    }
}