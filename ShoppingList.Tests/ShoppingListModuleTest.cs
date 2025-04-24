using Xunit;
using shoppinglist;
using shoppinglist.Models;
using System.Collections.Generic;

public class ShoppingListModuleTest
{
    [Fact]
    public void AddFoodItem()
    {
        var io = new TestConsoleIO(new[]
        {
            "add", 
            "fruits",
            "oranges",
            "needed",
            "done",
            "menu"
        });

        var data  = new InMemoryDataService();
        var module = new ShoppingListModule(io, data);
        module.Run();

        Assert.Single(data.FoodItems);
        Assert.Equal("oranges", data.FoodItems[0].FoodName);
        Assert.Equal("fruits", data.FoodItems[0].GroceryCategory);
        Assert.Equal("needed", data.FoodItems[0].Status);
    }
    
        [Fact]
    public void UpdateFoodItem()
    {
        var io = new TestConsoleIO(new[]
        {
            "update", 
            "oranges",
            "have",
            "menu"
        });

        var data  = new InMemoryDataService
        {
            FoodItems = new List<FoodItem>
            {
                new FoodItem {FoodName = "oranges", GroceryCategory = "fruit", Status = "needed"}
            } 
        };
        var module = new ShoppingListModule(io, data);
        module.Run();

        var updatedItem = data.FoodItems.Find(i => i.FoodName == "oranges");
        Assert.NotNull(updatedItem);
        Assert.Equal("have", updatedItem.Status);  
    }
        [Fact]
    public void RemoveFoodItem()
    {
        var io = new TestConsoleIO(new[]
        {
            "remove", 
            "tortillas",
            "menu"
        });

        var data  = new InMemoryDataService
        {
            FoodItems = new List<FoodItem>
            {
                new FoodItem {FoodName = "tortillas", GroceryCategory = "bakery", Status = "needed"},
                new FoodItem {FoodName = "milk", GroceryCategory = "dairy", Status = "have"}
            } 
        };
        var module = new ShoppingListModule(io, data);
        module.Run();

        Assert.DoesNotContain(data.FoodItems, i => i.FoodName == "tortillas");
        Assert.Single(data.FoodItems);       
    }
    
}