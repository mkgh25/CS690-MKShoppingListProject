using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public static class RecipeModule

    {
        public static void Run()
// list<Recipe> recipeitems
        {
            List<FoodItem> items = DataService.LoadFoodItemList();
            List<Recipe> rlist = DataService.LoadRecipeList();
            string rcommand;
            do{
                Console.WriteLine("Select one: add (to add a recipe), remove (to remove a recipe), menu (To go back to main menu)");
                rcommand=Console.ReadLine()?.Trim().ToLower();
                

                if (rcommand=="add")
                {
                    Console.Write("Enter a recipe name:");
                    string recipename = Console.ReadLine().Trim().ToLower();
                    Recipe newRecipe = new Recipe 
                    {
                       RecipeName = recipename, Ingredients = new List<string>()
                    };
                    string newingredient=" ";
                    do{
                        Console.WriteLine("Enter new ingredient or type 'done' to finish:");
                        newingredient = Console.ReadLine().Trim().ToLower();
                        if (newingredient != "done")
                        {
                        newRecipe.Ingredients.Add(newingredient);//add ingredient to ingredient list
                        Console.WriteLine("Ingredient added");
                        }
                    } while (newingredient.ToLower() !="done");

                    rlist.Add(newRecipe);//adding new recipe and ingredients to recipe list
                    DataService.SaveRecipeList(rlist);
                    Console.WriteLine("new recipe added");

                //check to see if ingredient is in is in shoppinglist
                    foreach (var ingredient in newRecipe.Ingredients)
                    {
                        if(!items.Any(i => i.FoodName.Equals(ingredient, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine($"Ingredient'{ingredient}' not found in shoppinglist");
                            Console.Write("Enter grocery category for this item: ");
                            string category = Console.ReadLine().Trim().ToLower();
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
                    DataService.SaveFoodItemList(items); //save added ingredients to shopping list
                    Console.WriteLine("ingredients added to shopping list");
                    }
                else if (rcommand=="remove")
                {
                    Console.Write("Enter a recipe to remove:");
                    string recipetoremove = Console.ReadLine().Trim().ToLower();
                    if (string.IsNullOrEmpty(recipetoremove))
                    {
                        Console.WriteLine("Recipe name cannot be empty.");
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
                        DataService.SaveRecipeList(rlist);
                        Console.WriteLine("Recipe removed!");
                    }           
                    else
                    {
                        Console.WriteLine("Recipe not Found, please try again");
                    }
                }
            } while(rcommand !="menu");
        }
    }
}
