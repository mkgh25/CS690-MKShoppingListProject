using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public class ShoppingListModule

    {
        private readonly IConsoleIO _io;
        private readonly IDataService _dataService;
        public ShoppingListModule(IConsoleIO io, IDataService dataService) //using IConsoleIO for inputs.
        {
            _io = io;
            _dataService = dataService;
        }
        public void Run()

        {
            List<FoodItem> items = _dataService.LoadFoodItemList();
            string fcommand;

           do{
                _io.WriteLine("Select one: add = (Add Food Item), update = (update food item status), remove = (Remove Food item), menu (go back to main menu)");
                fcommand=_io.ReadLine();
                string item;
                string grocerycategory;
                if (fcommand=="add")
                    do {
                        _io.WriteLine("Enter a Grocery Category or 'done' to quit: ");
                        grocerycategory = _io.ReadLine().Trim().ToLower(); 
                        if (grocerycategory == "done") //compare item(user input to "done") to break loop.
                            break;
                        _io.WriteLine("Enter a food item name or 'done' to quit:");  
                        item = _io.ReadLine().Trim().ToLower();  
                        if (item == "done") //compare item(user input to "done") to break loop.
                            break;

                        _io.WriteLine("Enter a status: needed or have:");
                        string status = _io.ReadLine();

                        items.Add(new FoodItem {GroceryCategory = grocerycategory, FoodName=item, Status=status});
                        _dataService.SaveFoodItemList(items);
                    }while (true);
                else if (fcommand == "update")
                    {
                        _io.WriteLine("Enter Food name to update:");
                        string itemtoupdate = _io.ReadLine().Trim().ToLower();

                        FoodItem match = null;
                        foreach(var i in items)
                        {
                            if (i.FoodName.ToLower() == itemtoupdate)
                            {
                                match = i;
                                break;
                            }
                        }
                        if (match != null)
                        {
                            _io.WriteLine($"Current status is: {match.Status}");
                            _io.WriteLine("Enter new status (needed or have): ");
                            string newStatus = _io.ReadLine().Trim().ToLower();
                            if (newStatus == "needed" || newStatus=="have")
                            {
                                match.Status=newStatus;
                                _dataService.SaveFoodItemList(items);
                                _io.WriteLine("Food item status updated.");
                            }
                            else
                            {
                                _io.WriteLine("Invalid status.Please enter 'needed' or 'have'.");
                            }
                        }
                        else
                        {
                            _io.WriteLine("Food item not found.");
                        }
                    }
                else if (fcommand == "remove")
                    {
                        _io.WriteLine("Enter a food item name to remove:");
                        string itemtoremove = _io.ReadLine().Trim().ToLower();//make lowercase
                        if (string.IsNullOrEmpty(itemtoremove))
                        {
                            _io.WriteLine("Food item name cannot be empty.");
                            continue;
                        }
                        FoodItem foodmatch = null;
                        foreach(var i in items)
                        {
                            if (i.FoodName.ToLower() == itemtoremove)
                            {
                                foodmatch=i;
                                break;
                            }
                        }
                        if  (foodmatch != null)
                        {
                            items.Remove(foodmatch);
                            _dataService.SaveFoodItemList(items);
                            _io.WriteLine("food item removed!");
                        }
                        else
                        {
                            _io.WriteLine("Food item not found");
                        }
                    }              
                } while(fcommand!="menu"); 
            }    
        }    
    }
// list<fooditems> items