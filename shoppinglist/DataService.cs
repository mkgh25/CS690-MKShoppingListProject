using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using shoppinglist.Models;

namespace shoppinglist
{
    public static class DataService
    {
        public static string foodfilepath = "food-database.txt";
        public static string recipefilepath = "recipe-database.txt";
        public static string mealsfilepath = "meals-database.txt";

        public static List<FoodItem> LoadFoodItemList()
        {
            List<FoodItem> items = new List<FoodItem>();
            if (!File.Exists(foodfilepath)) return items;

            string[] lines = File.ReadAllLines(foodfilepath);
            foreach (string line in lines)
            {
                string[] foodparts = line.Split(",");
                if (foodparts.Length == 3)
                {
                    string grocerycategory = foodparts[0].Trim();
                    string foodName = foodparts[1].Trim();
                    string status = foodparts[2].Trim();
                    items.Add(new FoodItem { GroceryCategory = grocerycategory, FoodName = foodName, Status = status });
                }
            }
            return items;
        }

        public static void SaveFoodItemList(List<FoodItem> items)
        {
            List<string> lines = new List<string>();
            foreach (var item in items)
            {
                string line = $"{item.GroceryCategory},{item.FoodName},{item.Status}";
                lines.Add(line);
            }
            File.WriteAllLines(foodfilepath, lines);
        }

        public static List<Recipe> LoadRecipeList()
        {
            List<Recipe> recipes = new List<Recipe>();
            if (!File.Exists(recipefilepath)) return recipes;

            string[] lines = File.ReadAllLines(recipefilepath);
            foreach (string line in lines)
            {
                var recipeparts = line.Split(":");
                if (recipeparts.Length == 2)
                {
                    string name = recipeparts[0].Trim();
                    List<string> ingredients = recipeparts[1].Split(",").Select(i => i.Trim()).ToList();
                    recipes.Add(new Recipe { RecipeName = name, Ingredients = ingredients });
                }
            }
            return recipes;
        }

        public static void SaveRecipeList(List<Recipe> recipes)
        {
            List<string> lines = new List<string>();
            foreach (var recipe in recipes)
            {
                string line = recipe.RecipeName + ":" + string.Join(",", recipe.Ingredients);
                lines.Add(line);
            }
            File.WriteAllLines(recipefilepath, lines);
        }

        public static List<MealPlan> LoadMealPlanList()
        {
            List<MealPlan> meals = new List<MealPlan>();
            if (!File.Exists(mealsfilepath)) return meals;

            string[] lines = File.ReadAllLines(mealsfilepath);
            foreach (string line in lines)
            {
                var parts = line.Split(",");
                if (parts.Length == 4)
                {
                    if (DateTime.TryParse(parts[0].Trim(), out DateTime date) &&
                        int.TryParse(parts[2].Trim(), out int mealNumber))
                    {
                        meals.Add(new MealPlan
                        {
                            Date = date,
                            MealTime = parts[1].Trim(),
                            MealNumber = mealNumber,
                            RecipeName = new Recipe { RecipeName = parts[3].Trim() }
                        });
                    }
                }
            }
            return meals;
        }

        public static void SaveMealPlanList(List<MealPlan> meals)
        {
            List<string> lines = new List<string>();
            foreach (var meal in meals)
            {
                string line = $"{meal.Date:yyyy-MM-dd},{meal.MealTime},{meal.MealNumber},{meal.RecipeName.RecipeName}";
                lines.Add(line);
            }
            File.WriteAllLines(mealsfilepath, lines);
        }
    }
}
