namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using System.Collections.Generic;

    public class MyRecipesOutputModel :RecipesOutputModel<MyRecipeOutputModel>
    {
        public MyRecipesOutputModel(
            IEnumerable<MyRecipeOutputModel> recipes,
            int page,
            int totalPages)
            : base(recipes, page, totalPages)
        {
        }
    }
}
