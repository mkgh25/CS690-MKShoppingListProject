using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public class ReportModule

    {
        private readonly IConsoleIO _io;
        private readonly IDataService _dataService;
        public ReportModule(IConsoleIO io, IDataService dataService)
        {
            _io = io;
            _dataService = dataService;
        }
        public void Run()

            {
            _io.WriteLine("Shopping List Report => Needed Items by Grocery Category");
            var items = _dataService.LoadFoodItemList();
            foreach (var item in items)
            {
                if (item.Status == "needed")
                {
                    _io.WriteLine($"- {item.GroceryCategory}: {item.FoodName}");
                }
            }
            _io.WriteLine("View the mealplan for date range? yes/no: ");
            string reportdate = _io.ReadLine().Trim().ToLower();
            if (reportdate=="yes")
            {
                _io.WriteLine("Enter startdate (yyyy-MM-dd):");
                string startDateinput = _io.ReadLine().Trim();
                _io.WriteLine("Enter enddate (yyyy-MM-dd):");
                string endDateinput = _io.ReadLine().Trim();

                if(DateTime.TryParse(startDateinput, out DateTime startDate) &&
                    DateTime.TryParse(endDateinput, out DateTime endDate))
                {   
                //DateTime startDate = DateTime.Today;
                    var meals = _dataService.LoadMealPlanList();
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
                        _io.WriteLine($"meal plan from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                        foreach (var meal in selectedmeals)
                        {
                            _io.WriteLine($"- {meal.Date:yyyy-MM-dd} ({meal.MealTime}): {meal.RecipeName.RecipeName} ");
                        }
                        } 
                    else
                    {
                        _io.WriteLine("No mealplan found for selected range.");
                    }
                }
                else
                {
                     _io.WriteLine("Please check start date or end date entries for valid format.");
                }
            } 
        }  
    }
}  
            