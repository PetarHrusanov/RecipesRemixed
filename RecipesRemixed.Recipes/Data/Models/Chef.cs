namespace RecipesRemixed.Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Chef
    {

        public Chef()
        {
            this.Recipes = new HashSet<Recipe>();
            this.RecipesRemix = new HashSet<RecipeRemix>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Qualification { get; set; }

        public string Biography { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<RecipeRemix> RecipesRemix { get; set; }
    }
}
