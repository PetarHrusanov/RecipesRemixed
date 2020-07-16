namespace RecipesRemixed.Recipes.Services.Chefs
{
    using System;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Chefs;
   
    public interface IChefsService : IDataService<Chef>
    {
        Task<Chef> FindByUser(string userId);

        Task<int> GetIdByUser(string userId);

        Task<bool> HasRecipe(int chefId, int recipeId);

        Task<bool> IsChef(string userId);

        Task<ChefDetailsOutputModel> GetDetails(int id);

        Task<ChefOutputModel> GetDetailsByRecipeId(int recipeId);
    }
}
