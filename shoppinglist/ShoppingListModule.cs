using System;
using System.Collections.Generic;
using shoppinglist;
using shoppinglist.Models;
using System.IO;
using System.Linq;

namespace shoppinglist

{
    public static class ShoppingListModule

    {
        public static void Run()

        {
            List<FoodItem> items = DataService.LoadFoodItemList();
            string fcommand;

           do{
                Console.WriteLine("Select one: add = (Add Food Item), update = (update food item status), remove = (Remove Food item), menu (go back to main menu)");
                fcommand=Console.ReadLine();
                string item;
                string grocerycategory;
                if (fcommand=="add")
                    do {
                        Console.WriteLine("Enter a Grocery Category or 'done' to quit: ");
                        grocerycategory = Console.ReadLine().Trim().ToLower(); 
                        if (grocerycategory == "done") //compare item(user input to "done") to break loop.
                            break;
                        Console.WriteLine("Enter a food item name or 'done' to quit:");  
                        item = Console.ReadLine().Trim().ToLower();  
                        if (item == "done") //compare item(user input to "done") to break loop.
                            break;

                        Console.WriteLine("Enter a status: needed or have:");
                        string status = Console.ReadLine();

                        items.Add(new FoodItem {GroceryCategory = grocerycategory, FoodName=item, Status=status});
                        DataService.SaveFoodItemList(items);
                    }while (true);
                else if (fcommand == "update")
                    {
                        Console.WriteLine("Enter Food name to update:");
                        string itemtoupdate = Console.ReadLine().Trim().ToLower();

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
                            Console.WriteLine($"Current status is: {match.Status}");
                            Console.WriteLine("Enter new status (needed or have): ");
                            string newStatus = Console.ReadLine().Trim().ToLower();
                            if (newStatus == "needed" || newStatus=="have")
                            {
                                match.Status=newStatus;
                                DataService.SaveFoodItemList(items);
                                Console.WriteLine("Food item status updated.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid status.Please enter 'needed' or 'have'.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Food item not found.");
                        }
                    }
                else if (fcommand == "remove")
                    {
                        Console.Write("Enter a food item name to remove:");
                        string itemtoremove = Console.ReadLine().Trim().ToLower();//make lowercase
                        if (string.IsNullOrEmpty(itemtoremove))
                        {
                            Console.WriteLine("Food item name cannot be empty.");
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
                            DataService.SaveFoodItemList(items);
                            Console.WriteLine("food item removed!");
                        }
                        else
                        {
                            Console.WriteLine("Food item not found");
                        }
                    }              
                }while(fcommand!="menu"); 
            }    
        }    
    }
// list<fooditems> items