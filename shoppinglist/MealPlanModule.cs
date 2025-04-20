using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public static class MealPlanModule

    {
        public static void Run()
        {
            List<MealPlan> mealList = DataService.LoadMealPlanList();
            List<Recipe> rlist = DataService.LoadRecipeList();

            string mcommand;
            do{
                    Console.WriteLine("Select one: add (add a mealplan), remove (remove a mealplan), menu (To go back to main menu)");
                    mcommand = Console.ReadLine().Trim().ToLower();

                    if (mcommand=="add"){
                        Console.WriteLine("Enter Mealplan start date: yyyy-MM-dd");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter mealtime selection: Breakfast, Lunch, or Dinner");
                        string mealTime = Console.ReadLine().Trim().ToLower();
                        Console.WriteLine("Enter recipe name for this meal:");
                        String recipeName = Console.ReadLine().Trim().ToLower();
                        int mealnumber = mealList.Count + 1;

                        Recipe selectRecipe = null;
                        foreach(var recipe in rlist)
                        {
                            if (recipe.RecipeName.Equals(recipeName, StringComparison.OrdinalIgnoreCase))
                            {
                                selectRecipe = recipe;//find recipe in list;
                                break;
                            }
                        }
                        if(selectRecipe != null)
                        {
                        mealList.Add(new MealPlan
                        {
                            Date = date,
                            MealTime = mealTime,
                            MealNumber = mealnumber,
                            RecipeName = new Recipe{RecipeName = selectRecipe.RecipeName}
                        });
                        DataService.SaveMealPlanList(mealList);
                        Console.WriteLine("Meal plan saved");                        
                        }
                        else
                        {
                            Console.WriteLine($"No recipe match found, please go to recipes and add to recipe list");
                        }
                    } 
                    else if (mcommand == "remove")
                        {
                        Console.WriteLine("Enter the date of meal plan you want to remove: yyyy-mm-dd");
                        string dateinput = Console.ReadLine();
                        DateTime removedate;
                        if (DateTime.TryParse(dateinput, out removedate))
                        {
                        mealList.RemoveAll(m => m.Date.Date == removedate.Date);
                        DataService.SaveMealPlanList(mealList);
                        Console.WriteLine("Meal plan(s) removed for " + removedate.ToString("yyyy-MM-dd"));
                        }
                        else {
                        Console.WriteLine("no mealplans found");
                        }
                    };
                }while(mcommand != "menu");
            } 
        }
    }

