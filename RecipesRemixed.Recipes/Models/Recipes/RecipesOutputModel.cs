using System;
using System.Collections.Generic;

namespace RecipesRemixed.Recipes.Models.Recipes
{
    public class RecipesOutputModel<TRecipeOutputModel>
    {
        protected RecipesOutputModel(
            IEnumerable<TRecipeOutputModel> recipes,
            int page,
            int totalPages)
        {
            this.Recipes = recipes;
            this.Page = page;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TRecipeOutputModel> Recipes { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}
