using Xunit;
using shoppinglist;
using shoppinglist.Models;
using System.Collections.Generic;

public class RecipeModuleTest
{
    [Fact]
    public void AddRecipe()
    {
        var io = new TestConsoleIO(new[]
        {
            "add", 
            "spagetti",
            "noodles",
            "tomato sauce",
            "garlic",
            "done", 
            "misc",
            "misc",
            "misc",
            "menu"
        });

        var data  = new InMemoryDataService();
        var module = new RecipeModule(io, data);
        module.Run();

        Assert.Single(data.Recipes);
        Assert.Equal("spagetti", data.Recipes[0].RecipeName);
        Assert.Equal(3, data.Recipes[0].Ingredients.Count);
        Assert.Equal(3, data.FoodItems.Count);
    }


     [Fact]
    public void RemoveRecipe()
    {
        var io = new TestConsoleIO(new[]
        {
            "remove", 
            "tacos",
            "menu"
        });

        var data  = new InMemoryDataService
        {
            Recipes = new List<Recipe>
            {
                new Recipe {RecipeName = "tacos", Ingredients = new List<string> {"tortillas","beef"}},
                new Recipe {RecipeName = "lasagna", Ingredients = new List<string> {"noodles","mozzerella cheese"}}
            } 
        };
        var module = new RecipeModule(io, data);
        module.Run();

        Assert.Single(data.Recipes); 
        Assert.DoesNotContain(data.Recipes, i => i.RecipeName == "tacos");
              
    }
    
}