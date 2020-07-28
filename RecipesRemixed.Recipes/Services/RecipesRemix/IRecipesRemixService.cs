namespace RecipesRemixed.Recipes.Services.RecipesRemix
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.RecipesRemix;
    using RecipesRemixed.Services;
    using RecipesRemixed.Recipes.Models.Recipes;

    public interface IRecipesRemixService : IDataService<RecipeRemix>
    {
        Task<RecipeRemixOutputModel> Create(RecipesRemixInputModel recipeInput, int chefId);

        Task<RecipeRemix> Find(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<IEnumerable<RecipeRemixOutputModel>> GetListings(RecipesQuery query);

        Task<IEnumerable<RecipeRemixOutputModel>> Mine(int chefId, RecipesQuery query);

        Task<RecipeRemixOutputModel> GetDetails(int id);

        Task<int> Modify(RecipesRemixEditModel recipeInput);

        Task<int> Total(RecipesQuery query);

    }
}
