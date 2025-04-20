using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public static class ReportModule

    {
        public static void Run()

            {
            Console.WriteLine("Shopping List Report => Needed Items by Grocery Category");
            string [] shoplistreport = File.ReadAllLines(DataService.foodfilepath); 
            foreach (string line in shoplistreport)
            {
                string [] foodparts = line.Split(",");
                if(foodparts.Length == 3)
                {
                    string GroceryCategory = foodparts[0].Trim();
                    string foodName = foodparts[1];
                    string status = foodparts[2];
                    if (status == "needed")
                    {
                        Console.WriteLine($"- {GroceryCategory}: {foodName}");
                    }
                }
                else
                {
                Console.WriteLine("Error");  
                }     
            }
            Console.WriteLine("View the mealplan for date range? yes/no: ");
            string reportdate = Console.ReadLine().Trim().ToLower();
            if (reportdate=="yes")
            {
                Console.WriteLine("Enter startdate (yyyy-MM-dd):");
                string startDateinput = Console.ReadLine().Trim();
                Console.WriteLine("Enter enddate (yyyy-MM-dd):");
                string endDateinput = Console.ReadLine().Trim();

                if(DateTime.TryParse(startDateinput, out DateTime startDate) &&
                    DateTime.TryParse(endDateinput, out DateTime endDate))
                {   
                //DateTime startDate = DateTime.Today;
                    var meals = DataService.LoadMealPlanList();
                    var selectedmeals = new List<MealPlan>();
                    foreach(var meal in meals)
                    {
                        if (meal.Date.Date >= startDate && meal.Date.Date <= endDate.Date)
                        {
                        selectedmeals.Add(meal);
                        }
                    }
                    selectedmeals.Sort((meal1,meal2)=>
                    {
                    int dateComparison= meal1.Date.CompareTo(meal2.Date);
                    if(dateComparison == 0)
                        {
                        return meal1.MealTime.CompareTo(meal2.MealTime);
                        }
                        return dateComparison;
                    });
                    if (selectedmeals.Count >0)
                    {
                        Console.WriteLine($"meal plan from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                        foreach (var meal in selectedmeals)
                        {
                            Console.WriteLine($"- {meal.Date:yyyy-MM-dd} ({meal.MealTime}): {meal.RecipeName.RecipeName} ");
                        }
                        } 
                    else
                    {
                        Console.WriteLine("No mealplan found for selected range.");
                    }
                }
                else
                {
                     Console.WriteLine("Please check start date or end date entries for valid format.");
                }
            } 
        }  
    }
}  
            