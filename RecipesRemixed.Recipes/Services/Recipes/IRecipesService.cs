﻿namespace RecipesRemixed.Recipes.Services.Recipes
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Services;

    public interface IRecipesService : IDataService<Recipe>
    {
        Task<RecipeOutputModel> Create(RecipesInputModel recipeInput, int chefId);

        Task<Recipe> Find(int id);

        Task<bool> Delete(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query);

        Task<IEnumerable<RecipeOutputModel>> Mine(int chefId);

        Task<IEnumerable<RecipeOutputModel>> Filter(RecipesAllViewModel query);

        Task<RecipeOutputModel> GetDetails(int id);

        Task<int> Modify(RecipesEditModel recipeInput);

        Task<int> Total(RecipesQuery query);

    }
}
