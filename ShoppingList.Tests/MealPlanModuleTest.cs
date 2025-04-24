using Xunit;
using shoppinglist;
using shoppinglist.Models;
using System.Collections.Generic;

public class MealPlanModuleTest
{
    [Fact]
    public void AddMeal()
    {
        var io = new TestConsoleIO (new[]
        {
            "add",
            "2025-04-30",
            "breakfast",
            "french toast",
            "menu"
        });

        var data  = new InMemoryDataService
        {
            Recipes = new List<Recipe>
            {
                new Recipe {RecipeName = "french toast", Ingredients = new List<string> {"bread","eggs"}}
            } 
        };
        var module = new MealPlanModule(io, data);
        module.Run();

        Assert.Single(data.Meals);
        Assert.Equal("french toast", data.Meals[0].RecipeName.RecipeName);
        Assert.Equal("breakfast", data.Meals[0].MealTime);
        Assert.Equal(new System.DateTime(2025,04,30), data.Meals[0].Date);
    }

    [Fact]

    public void RemoveMeal()
    {
        var io = new TestConsoleIO (new[]
        {
            "remove",
            "2025-04-29",
            "menu"
        });
        var data  = new InMemoryDataService
        {
            Meals = new List<MealPlan>
            {
                new MealPlan {Date = new System.DateTime(2025,04,29), MealTime = "breakfast", MealNumber = 1, RecipeName = new Recipe { RecipeName = "cereal"}}
            } 
        };
        var module = new MealPlanModule( io, data);
        module.Run();

        Assert.Empty(data.Meals);

    }
}