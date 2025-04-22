using Xunit;
using shoppinglist;
using shoppingList.Models;
using System.Linq;


namespace ShoppingList.Tests
{
    public class ShoppingListModuleTest

    { 
        public TestDataService tdataservice;
        public ShoppingListModule tshoppinglistmodule;

        public ShoppingListModuleTest()
        {
            tdataservice = new TestDataService();
            tshoppinglistmodule = new ShoppingListModule (tdataservice);
        }
        
        [Fact]
        public void addfooditem()
        {
            var Item = new FoodItem { FoodName = "broccoli", GroceryCategory = "vegetables", Status = "needed"};
            tshoppinglistmodule.addfooditem(Item);
            var items = tdataservice.LoadFoodItemList();
            Assert.Contains(items, i=> i.FoodName == "broccoli");
        }
        [Fact]
        public void updatefooditem()
        {var itemtoupdate = "oranges";
        var newStatus = "have";   
        tshoppinglistmodule.updatefooditem(itemtoupdate,newStatus);
        var items = tdataservice.LoadFoodItemList();
        var updatedItem = items.FirstOrDefault(i => i.FoodName == itemtoupdate);
        Assert.NotNull(updatedItem);
        Assert.Equal(newStatus, updatedItem.Status);
        }
        [Fact]

        public void removefooditem()
        {var itemtoremove = "tortillas";
        tshoppinglistmodule.removefooditem(itemtoremove);
        var items = tdataservice.LoadFoodItemList();
        Assert.DoesNotContain(items, i.FoodName == itemtoremove);
        }
    }
}