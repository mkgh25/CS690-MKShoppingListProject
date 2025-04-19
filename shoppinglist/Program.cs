using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;// added for special functions.


namespace shoppinglist;

class Program
{//create classes of objects I want to use.
// arbitratry comment for testing 
    class FoodItem //create class (description of FoodItem food item is)
    {
        public string GroceryCategory {get;set;}
        public string FoodName {get;set;}
        public string Status { get;set;}
    }
    class RecipeClass//create class of recipe (description of what a recipe is)
    {
        public string RecipeName { get; set;}
        public List<string> Ingredients {get; set;}//creates a property that can be retrieved and set (value assigned)
    }
    class MealPlan//create class of mealsplan (description of what a mealplan is)
    {
        public string MealTime { get; set;}//which mealtime is this?
        public DateTime Date { get; set;}//date for meal plan created for
        public RecipeClass RecipeName { get; set;}//Recipe name from Recipe Class
        public int MealNumber {get; set;}//what meal number
    }
    //create the source of the data I want to import/use in program.
    static string foodfilepath="food-database.txt";
    static string recipefilepath="recipe-database.txt";
    static string mealsfilepath="meals-database.txt";

    //creates a placeholder list for the data to be retrieved for other functions to be used.
    static List<FoodItem> LoadFoodItemList()
    {
        List<FoodItem> items = new List<FoodItem>();
        string[] lines = File.ReadAllLines(foodfilepath);
        foreach (string line in lines)
        {
            string[] foodparts = line.Split(",");
            if(foodparts.Length == 3)
            {
                string grocerycategory = foodparts[0].Trim();
                string foodName = foodparts[1].Trim();
                string status = foodparts[2].Trim();
                items.Add(new FoodItem{ GroceryCategory = grocerycategory, FoodName = foodName, Status = status});
            }
            
        }
       return items; 
    } 
    static List<RecipeClass> LoadRecipeList()
    {
        List <RecipeClass> recipes = new List<RecipeClass>();
        string[] lines = File.ReadAllLines(recipefilepath);
        foreach (string line in lines)
        {
            var recipeparts =line.Split(":");
            if (recipeparts.Length == 2)
            {
                string name = recipeparts[0].Trim();
                List<string> ingredients = recipeparts[1].Split(",").Select(i=>i.Trim()).ToList();
                recipes.Add(new RecipeClass
                {
                    RecipeName = name,
                    Ingredients = ingredients
                });
            }
        }
        return recipes;
    }
    static List<MealPlan> LoadMealPlanList()
    {
        List <MealPlan> meals = new List<MealPlan>();
        string[] lines = File.ReadAllLines(mealsfilepath);
        foreach (string line in lines)
        {
            var mealparts =line.Split(",");
            if (mealparts.Length == 4)
            {
                string datestr = mealparts[0].Trim();
                string mealtime = mealparts[1].Trim();
                string strmealnum = mealparts[2].Trim();
                string recipeName = mealparts[3].Trim();
                //DateTime date = DateTime.Parse(datestr);//need to parse date and mealnumber;
                //int mealnumber = int.Parse(strmealnum);
                if (!DateTime.TryParse(datestr, out DateTime date))//parse date for storing and validate format.
                {
                    Console.WriteLine($"Invalid date.");
                    continue;
                }
                if(!int.TryParse(strmealnum, out int mealnumber))
                {
                    Console.WriteLine($"invalid mealnumber");
                    continue;
                }
                {meals.Add(new MealPlan
                    {
                        Date = date,
                        MealTime = mealtime,
                        MealNumber = mealnumber,
                        RecipeName = new RecipeClass {RecipeName = recipeName}
                    });
                }
            }
        }
        return meals;
    }
    //create method to save fooditems lists and recipe lists to .txt files.
    static void SaveFoodItemList(List<FoodItem> items)
    {
        List<string> lines = new List<string>();
        foreach (var item in items)
        {
            string line = item.GroceryCategory + "," + item.FoodName + "," + item.Status;
            lines.Add(line);
        }
        File.WriteAllLines(foodfilepath,lines);  
    }
    static void SaveRecipeList(List<RecipeClass> recipeitems)
    {
        List<string> lines = new List<string>();
        foreach (var recipe in recipeitems)
        {
            string line = recipe.RecipeName + ":" + string.Join(",",recipe.Ingredients);
            lines.Add(line);
        }
        File.WriteAllLines(recipefilepath,lines);
    }

    static void SaveMealPlanList(List<MealPlan> meals)
    {
        List<string> lines = new List<string>();
        foreach (var meal in meals)
        {
            string line = $"{meal.Date:yyyy-MM-dd},{meal.MealTime},{meal.MealNumber},{meal.RecipeName.RecipeName}";
            lines.Add(line);
        }
        File.WriteAllLines(mealsfilepath, lines);
    }


    static void Main(string[] args)
    {//Create file and save
        if (!File.Exists(foodfilepath))
        {
            File.Create(foodfilepath).Close();
        }
        if (!File.Exists(recipefilepath))
        {
            File.Create(recipefilepath).Close();
        }
        if (!File.Exists(mealsfilepath))
        {
            File.Create(mealsfilepath).Close();
        }

        //Call lists from methods
        List<FoodItem> items = LoadFoodItemList();
        List<RecipeClass> rlist = LoadRecipeList();

        //create variables
        string usercommand;
        string fcommand;
        string rcommand;
        string mcommand;


        do {

            Console.WriteLine("Enter a selection: ");
            Console.WriteLine("shoppinglist = to manage shopping list");
            Console.WriteLine("recipes = to manage recipes");
            Console.WriteLine("mealplan = to manage mealplan");
            Console.WriteLine("report = to display shopping list");
            Console.WriteLine("exit = to exit program");
            usercommand = Console.ReadLine();
            //Console.WriteLine($"DEBUG: You typed '{usercommand}'");

            //Shopping list - module
        
            if(usercommand == "shoppinglist")
            {
        
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
                        SaveFoodItemList(items);
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
                                SaveFoodItemList(items);
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
                            SaveFoodItemList(items);
                            Console.WriteLine("food item removed!");
                        }
                        else
                        {
                            Console.WriteLine("Food item not found");
                        }
                    }              
                }while(fcommand!="menu");  
            }    
            else if (usercommand=="recipes")
            {//Recipes - Module
                do{
                    Console.WriteLine("Select one: add (to add a recipe), remove (to remove a recipe), menu (To go back to main menu)");
                    rcommand=Console.ReadLine()?.Trim().ToLower();
                

                    if (rcommand=="add")
                    {
                        Console.Write("Enter a recipe name:");
                        string recipename = Console.ReadLine().Trim().ToLower();
                        RecipeClass newRecipe = new RecipeClass 
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
                        SaveRecipeList(rlist);
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
                        SaveFoodItemList(items); //save added ingredients to shopping list
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
                        RecipeClass recipeToRemove=null;
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
                            SaveRecipeList(rlist);
                            Console.WriteLine("Recipe removed!");
                        }           
                        else
                        {
                            Console.WriteLine("Recipe not Found, please try again");
                        }
                    }
                } while(rcommand !="menu");         
            } 
            else if (usercommand=="mealplan") 
            {//mealplan - module
              //Console.WriteLine("under development"); 
                List<MealPlan> mealList = LoadMealPlanList();//use current meal plan list from method
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

                        RecipeClass selectRecipe = null;
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
                            RecipeName = new RecipeClass{RecipeName = selectRecipe.RecipeName}
                        });
                        SaveMealPlanList(mealList);
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
                        SaveMealPlanList(mealList);
                        Console.WriteLine("Meal plan(s) removed for " + removedate.ToString("yyyy-MM-dd"));
                        }
                        else {
                        Console.WriteLine("no mealplans found");
                        }
                    };
                }while(mcommand != "menu");
            } 
            else if (usercommand == "report")
                {
                Console.WriteLine("Shopping List Report => Needed Items by Grocery Category");
                string [] shoplistreport = File.ReadAllLines(foodfilepath); 
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
                        var meals = LoadMealPlanList();
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
                                Console.WriteLine($"- {meal.Date:yyyy-MM-dd} ({meal.MealTime}): {meal.RecipeName.RecipeName}");
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
        }while(usercommand !="exit"); 
    } 
}