namespace RecipesRemixed.Recipes.Services.Recipes
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Recipes;

    public interface IRecipesService : IDataService<Recipe>
    {
        Task<RecipeOutputModel> Create(RecipesInputModel recipeInput, int chefId);

        Task<Recipe> Find(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<RecipeOutputModel>> GetAll<T>();

        Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query);

        Task<IEnumerable<RecipeOutputModel>> Mine(int chefId, RecipesQuery query);

        Task<RecipeOutputModel> GetDetails(int id);

        IEnumerable<T> GetAll<T>(int? take, int skip);

        Task<bool> Modify(RecipesInputModel recipeInput);

        Task<int> Total(RecipesQuery query);

    }
}
