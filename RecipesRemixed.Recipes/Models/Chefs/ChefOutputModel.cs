namespace RecipesRemixed.Recipes.Models.Chefs
{
    using System;
    using System.Collections.Generic;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Recipes.Models.RecipesRemix;
    using RecipesRemixed.Services.Mapping;

    public class ChefOutputModel : IMapFrom<Chef>
    {
        public ChefOutputModel()
        {
            this.Recipes = new HashSet<RecipeOutputModel>();
            this.RecipesRemix = new HashSet<RecipeRemixOutputModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Qualification { get; set; }

        public string Biography { get; set; }

        public string UserId { get; set; }

        public ICollection<RecipeOutputModel> Recipes { get; set; }

        public ICollection<RecipeRemixOutputModel> RecipesRemix { get; set; }

    }
}
