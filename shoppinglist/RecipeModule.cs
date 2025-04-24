using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public class RecipeModule

    {
        private readonly IConsoleIO _io;
        private readonly IDataService _dataService;
        public RecipeModule(IConsoleIO io, IDataService dataService)
        {
            _io = io;
            _dataService = dataService;
        }

        public void Run()

// list<Recipe> recipeitems
        {
            List<FoodItem> items = _dataService.LoadFoodItemList();
            List<Recipe> rlist = _dataService.LoadRecipeList();
            string rcommand;
            do{
                _io.WriteLine("Select one: add (to add a recipe), remove (to remove a recipe), menu (To go back to main menu)");
                rcommand=_io.ReadLine()?.Trim().ToLower();
                

                if (rcommand=="add")
                {
                    _io.WriteLine("Enter a recipe name:");
                    string recipename = _io.ReadLine().Trim().ToLower();
                    Recipe newRecipe = new Recipe 
                    {
                       RecipeName = recipename, Ingredients = new List<string>()
                    };
                    string newingredient=" ";
                    do{
                        _io.WriteLine("Enter new ingredient or type 'done' to finish:");
                        newingredient = _io.ReadLine().Trim().ToLower();
                        if (newingredient != "done")
                        {
                        newRecipe.Ingredients.Add(newingredient);//add ingredient to ingredient list
                        _io.WriteLine("Ingredient added");
                        }
                    } while (newingredient.ToLower() !="done");

                    rlist.Add(newRecipe);//adding new recipe and ingredients to recipe list
                    _dataService.SaveRecipeList(rlist);
                    _io.WriteLine("new recipe added");

                //check to see if ingredient is in is in shoppinglist
                    foreach (var ingredient in newRecipe.Ingredients)
                    {
                        if(!items.Any(i => i.FoodName.Equals(ingredient, StringComparison.OrdinalIgnoreCase)))
                        {
                            _io.WriteLine($"Ingredient'{ingredient}' not found in shoppinglist");
                            _io.WriteLine("Enter grocery category for this item: ");
                            string category = _io.ReadLine().Trim().ToLower();
                            if (string.IsNullOrEmpty(category))
                            {
                                category = "misc";
                            }
                            items.Add(new FoodItem
                            {
                                GroceryCategory = category,
                                FoodName = ingredient,
                                Status = "needed"
                            });
                        }
                    }
                    _dataService.SaveFoodItemList(items); //save added ingredients to shopping list
                    _io.WriteLine("ingredients added to shopping list");
                    }
                else if (rcommand=="remove")
                {
                    _io.WriteLine("Enter a recipe to remove:");
                    string recipetoremove = _io.ReadLine().Trim().ToLower();
                    if (string.IsNullOrEmpty(recipetoremove))
                    {
                        _io.WriteLine("Recipe name cannot be empty.");
                        continue;
                    }
                    Recipe recipeToRemove=null;
                    foreach (var recipe in rlist)
                    {
                        if(recipe.RecipeName.ToLower() == recipetoremove)
                        {
                            recipeToRemove = recipe;
                            break;
                        }  
                    }   
                    if(recipeToRemove != null)
                    {
                        rlist.Remove(recipeToRemove);
                        _dataService.SaveRecipeList(rlist);
                        _io.WriteLine("Recipe removed!");
                    }           
                    else
                    {
                        _io.WriteLine("Recipe not Found, please try again");
                    }
                }
            } while(rcommand !="menu");
        }
    }
}
