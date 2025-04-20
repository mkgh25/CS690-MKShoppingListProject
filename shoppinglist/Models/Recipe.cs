using System.Collections.Generic;

namespace shoppinglist.Models
{
    public class Recipe
    {
        public string RecipeName { get; set; }
        public List<string> Ingredients { get; set; }
    }
}