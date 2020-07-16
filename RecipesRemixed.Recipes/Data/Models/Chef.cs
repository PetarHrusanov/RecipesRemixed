namespace RecipesRemixed.Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Chef
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Qualification { get; set; }

        public string Biography { get; set; }

        public string UserId { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();

        public IEnumerable<RecipeRemix> RecipesRemix { get; set; } = new List<RecipeRemix>();
    }
}
