using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{

    public class MealPlanModule

    {
        
        private readonly IConsoleIO _io;
        private readonly IDataService _dataService;
        public MealPlanModule(IConsoleIO io, IDataService dataService)
        {
            _io = io;
            _dataService = dataService;
        }
        public void Run()  

        {
            List<MealPlan> mealList = _dataService.LoadMealPlanList();
            List<Recipe> rlist = _dataService.LoadRecipeList();

            string mcommand;
            do{
                    _io.WriteLine("Select one: add (add a mealplan), remove (remove a mealplan), menu (To go back to main menu)");
                    mcommand = _io.ReadLine().Trim().ToLower();

                    if (mcommand=="add"){
                        _io.WriteLine("Enter Mealplan start date: yyyy-MM-dd");
                        DateTime date = DateTime.Parse(_io.ReadLine());
                        _io.WriteLine("Enter mealtime selection: Breakfast, Lunch, or Dinner");
                        string mealTime = _io.ReadLine().Trim().ToLower();
                        _io.WriteLine("Enter recipe name for this meal:");
                        String recipeName = _io.ReadLine().Trim().ToLower();
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
                        _dataService.SaveMealPlanList(mealList);
                        _io.WriteLine("Meal plan saved");                        
                        }
                        else
                        {
                            _io.WriteLine($"No recipe match found, please go to recipes and add to recipe list");
                        }
                    } 
                    else if (mcommand == "remove")
                        {
                        _io.WriteLine("Enter the date of meal plan you want to remove: yyyy-mm-dd");
                        string dateinput = _io.ReadLine();
                        DateTime removedate;
                        if (DateTime.TryParse(dateinput, out removedate))
                        {
                        mealList.RemoveAll(m => m.Date.Date == removedate.Date);
                        _dataService.SaveMealPlanList(mealList);
                        _io.WriteLine("Meal plan(s) removed for " + removedate.ToString("yyyy-MM-dd"));
                        }
                        else {
                        _io.WriteLine("no mealplans found");
                        }
                    };
                }while(mcommand != "menu");
            } 
        }
    }

