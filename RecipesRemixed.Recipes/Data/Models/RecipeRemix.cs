namespace RecipesRemixed.Recipes.Data.Models
{
    using System;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Data.Models.Enums;

    public class RecipeRemix
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public TypeOfDish TypeOfDish { get; set; }

        public string ImageUrl { get; set; }

        public int Calories { get; set; }

        public bool Vegetarian { get; set; }

        public bool Vegan { get; set; }

        public string Allergies { get; set; }

        public int ChefId { get; set; }
        public Chef Chef { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
