namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using System.Collections.Generic;
    using RecipesRemixed.Data.Models.Enums;
    using RecipesRemixed.Recipes.Data.Models;

    public class RecipesAllViewModel
    {
        // da opravq
        public IEnumerable<RecipeOutputModel> Recipes { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public TypeOfDish? TypeOfDish { get; set; }

        public int? Page { get; set; } = 1;

        // Eventualen update
        //public bool? Vegetarian { get; set; }
        //public bool? Vegan { get; set; }

        //Vegetarian: <input type = "checkbox" asp-for="Vegetarian" />
        //Vegan: <input type = "checkbox" asp-for="Vegan" />
    }
}
