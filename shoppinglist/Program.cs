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
        IConsoleIO io = new ConsoleIO();
        IDataService dataService = new FileDataService();
        //Create file and save
        var fileDataService = dataService as FileDataService;
        if (fileDataService != null)
    {
        if (!File.Exists(fileDataService.FoodFilePath)) File.Create(fileDataService.FoodFilePath).Close();
        if (!File.Exists(fileDataService.RecipeFilePath)) File.Create(fileDataService.RecipeFilePath).Close();
        if (!File.Exists(fileDataService.MealsFilePath)) File.Create(fileDataService.MealsFilePath).Close();
    }

        //create variables
        string usercommand;
        //string fcommand;
        //string rcommand;
        //string mcommand;


        do
        {

            io.WriteLine("Enter a selection: ");
            io.WriteLine("shoppinglist = to manage shopping list");
            io.WriteLine("recipes = to manage recipes");
            io.WriteLine("mealplan = to manage mealplan");
            io.WriteLine("report = to display shopping list");
            io.WriteLine("exit = to exit program");
            usercommand = io.ReadLine();
            //Console.WriteLine($"DEBUG: You typed '{usercommand}'");

            //Shopping list - module

            if (usercommand == "shoppinglist")
            {
                new ShoppingListModule(io, dataService).Run();
            }
            else if (usercommand == "recipes")
            {
               new RecipeModule(io, dataService).Run();
            }
            else if (usercommand == "mealplan")
            {
                //mealplan - module
                new MealPlanModule(io, dataService).Run();

            }
            else if (usercommand == "report")
            {
                new ReportModule(io, dataService).Run();
            }
        } while (usercommand != "exit");
    }
}
