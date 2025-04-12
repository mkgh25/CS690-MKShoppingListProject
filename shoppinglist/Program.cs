namespace shoppinglist;

class Program
{//create classes of objects I want to use.
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
    //create the source of the data I want to import/use in program.
    static string foodfilepath="food-database.txt";
    static string recipefilepath="recipe-database.txt";
    
    

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

    //create method to save fooditems lists and recipe lists to .txt files.
    static void SaveFoodItemList(List<FoodItem> items)
    {
        List<string> lines = new List<string>();
        foreach (var item in items)
        {
            string line = item.FoodName + "," + item.Status;
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

        //Call lists from methods
        List<FoodItem> items = LoadFoodItemList();
        List<RecipeClass> rlist = LoadRecipeList();

        //create variables
        string usercommand;
        string fcommand;
        string rcommand;


        do {

            Console.WriteLine("Enter a selection: ");
            Console.WriteLine("shoppinglist = to manage shopping list");
            Console.WriteLine("recipes = to manage recipes");
            Console.WriteLine("mealplan = to manage mealplan");
            Console.WriteLine("report = to display shopping list");
            Console.WriteLine("exit = to exit program");
            usercommand = Console.ReadLine();

        //Shopping list 
        
            if(usercommand == "shoppinglist")
            {
        
                do{
                    Console.WriteLine("Select one: add = (Add Food Item), remove = (Remove Food item), menu (go back to main menu)");
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
            {//Recipes
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
                        if(recipetoremove != null)
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
            {//mealplan
              Console.WriteLine("under development");        
            } 
            else if (usercommand=="report")
            {//Report
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
                    Console.WriteLine($"- {foodName}");
                    }
                }
                else
                {
                  Console.WriteLine("Error");  
                }
              }       
            } 

        }while(usercommand !="exit");
    }
}
