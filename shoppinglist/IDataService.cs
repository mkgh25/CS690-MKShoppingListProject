using System.Collections.Generic;
using shoppinglist.Models;

public interface IDataService
{
    List<FoodItem> LoadFoodItemList();
    void SaveFoodItemList(List<FoodItem> items);

    List<Recipe> LoadRecipeList();
    void SaveRecipeList(List<Recipe> recipes);

    List<MealPlan> LoadMealPlanList();
    void SaveMealPlanList(List<MealPlan> meals);
}