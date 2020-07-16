namespace RecipesRemixed.Recipes.Services.Recipes
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Recipes;

    public interface IRecipesService : IDataService<Recipe>
    {
        Task<int> CreateAsync(RecipesInputModel recipeInput);

        Task<Recipe> Find(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<RecipeOutputModel>> GetAll<T>();

        Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query);

        Task<IEnumerable<RecipeOutputModel>> Mine(int chefId, RecipesQuery query);


        IEnumerable<T> GetAllPaginatedAsync<T>(int? take, int skip);

        Task<int> GetRecipesCountAsync();

        Task<T> GetByIdAsyn<T>(int id);

        Task<T> GetByNameAsync<T>(string name);

        Task<bool> ModifyAsync(RecipesInputModel recipeInput);

        Task<int> Total(RecipesQuery query);

    }
}
