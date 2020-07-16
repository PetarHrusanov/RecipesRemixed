namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using System.Collections.Generic;


    public class RecipesSearchOutputModel : RecipesOutputModel<RecipeOutputModel>
    {
        public RecipesSearchOutputModel(
            IEnumerable<RecipeOutputModel> carAds,
            int page,
            int totalPages)
            : base(carAds, page, totalPages)
        {
        }
    }
}
