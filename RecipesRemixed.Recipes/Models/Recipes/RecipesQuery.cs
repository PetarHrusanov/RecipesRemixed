namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Chef;
    using static Data.DataConstants.Recipes;
    using static Data.DataConstants.Url;
    using static Data.DataConstants.Calories;
    using RecipesRemixed.Recipes.Data.Models.Enums;

    public class RecipesQuery
    {
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public TypeOfDish? TypeOfDish { get; set; }

        public int? Calories { get; set; }

        public bool? Vegetarian { get; set; }

        public bool? Vegan { get; set; }

        public int Page { get; set; } = 1;
    }
}
