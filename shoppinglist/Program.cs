using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;// added for special functions.


namespace shoppinglist;

class Program
{

    static void Main(string[] args)
    { 
        //Create file and save
        if (!File.Exists(DataService.foodfilepath))
        {
            File.Create(DataService.foodfilepath).Close();
        }
        if (!File.Exists(DataService.recipefilepath))
        {
            File.Create(DataService.recipefilepath).Close();
        }
        if (!File.Exists(DataService.mealsfilepath))
        {
            File.Create(DataService.mealsfilepath).Close();
        }

        //Call lists from methods
        List<FoodItem> items = DataService.LoadFoodItemList();
        List<Recipe> rlist = DataService.LoadRecipeList();

        //create variables
        string usercommand;
        //string fcommand;
        //string rcommand;
        string mcommand;


        do
        {

            Console.WriteLine("Enter a selection: ");
            Console.WriteLine("shoppinglist = to manage shopping list");
            Console.WriteLine("recipes = to manage recipes");
            Console.WriteLine("mealplan = to manage mealplan");
            Console.WriteLine("report = to display shopping list");
            Console.WriteLine("exit = to exit program");
            usercommand = Console.ReadLine();
            //Console.WriteLine($"DEBUG: You typed '{usercommand}'");

            //Shopping list - module

            if (usercommand == "shoppinglist")
            {
                ShoppingListModule.Run();
            }
            else if (usercommand == "recipes")
            {
                RecipeModule.Run();
            }
            else if (usercommand == "mealplan")
            {
                //mealplan - module
                MealPlanModule.Run();

            }
            else if (usercommand == "report")
            {
                ReportModule.Run();
            }
        } while (usercommand != "exit");
    }
}
