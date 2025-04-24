using System.Collections.Generic;
using shoppinglist.Models;

public class InMemoryDataService : IDataService
{
    public List<FoodItem> FoodItems { get; set; } = new();
    public List<Recipe> Recipes { get; set; } = new();
    public List<MealPlan> Meals { get; set; } = new();

    public List<FoodItem> LoadFoodItemList() => new(FoodItems);
    public void SaveFoodItemList(List<FoodItem> items) => FoodItems = new(items);

    public List<Recipe> LoadRecipeList() => new(Recipes);
    public void SaveRecipeList(List<Recipe> recipes) => Recipes = new(recipes);

    public List<MealPlan> LoadMealPlanList() => new(Meals);
    public void SaveMealPlanList(List<MealPlan> meals) => Meals = new(meals);
}
