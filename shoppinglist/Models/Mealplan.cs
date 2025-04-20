// Models/MealPlan.cs
using System;

namespace shoppinglist.Models
{
    public class MealPlan
    {
        public string MealTime { get; set; }
        public DateTime Date { get; set; }
        public Recipe RecipeName { get; set; }
        public int MealNumber { get; set; }
    }
}
