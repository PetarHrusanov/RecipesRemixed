namespace RecipesRemixed.Recipes.Services.Chefs
{
    using System;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Chefs;
    using RecipesRemixed.Services;

    public interface IChefsService : IDataService<Chef>
    {
        Task<int> CreateChef(ChefInputModel input, string userId); 

        Task<Chef> FindByUser(string userId);

        Task<int> GetIdByUser(string userId);

        Task<bool> HasRecipe(int chefId, int recipeId);

        Task<bool> IsChef(string userId);

        Task<ChefOutputModel> GetDetails(int id);

        Task<ChefOutputModel> GetDetailsByRecipeId(int recipeId);
    }
}
