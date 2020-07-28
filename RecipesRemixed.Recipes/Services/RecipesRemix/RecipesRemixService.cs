namespace RecipesRemixed.Recipes.Services.RecipesRemix
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using RecipesRemixed.Services.Mapping;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Services;
    using RecipesRemixed.Recipes.Models.RecipesRemix;

    public class RecipesRemixService : DataService<RecipeRemix>, IRecipesRemixService
    {

        private const int RecipesPerPage = 10;

        public RecipesRemixService(RecipesDbContext db)
        : base(db)
        {

        }

        public async Task<RecipeRemixOutputModel> Create(RecipesRemixInputModel recipeInput, int chefId)
        {
            var recipe = new RecipeRemix
            {
                Name = recipeInput.Name,
                Ingredients = recipeInput.Ingredients,
                Instructions = recipeInput.Instructions,
                Calories = recipeInput.Calories,
                Allergies = recipeInput.Allergies,
                Vegan = recipeInput.Vegan,
                Vegetarian = recipeInput.Vegetarian,
                ChefId = chefId,
                ImageUrl = recipeInput.ImageUrl,
                TypeOfDish = recipeInput.TypeOfDish,
                RecipeId = recipeInput.RecipeId
            };

            await this.Save(recipe);
            var output = await this.Data.Set<RecipeRemix>()
                .Where(r => r.Id == recipe.Id)
                .To<RecipeRemixOutputModel>()
                .FirstOrDefaultAsync();

            return output;
        }

        public async Task<bool> Delete(int id)
        {
            var carAd = await this.Data.Set<RecipeRemix>().FindAsync(id);

            if (carAd == null)
            {
                return false;
            }

            this.Data.Set<RecipeRemix>().Remove(carAd);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<RecipeRemix> Find(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<RecipeRemixOutputModel> GetDetails(int id)
        {
            var recipe = await this.All().Where(r => r.Id == id).To<RecipeRemixOutputModel>().FirstOrDefaultAsync();
            return recipe;

        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var list = await Data.Set<RecipeRemix>().To<T>().ToListAsync();
            return list;
        }

        public async Task<IEnumerable<RecipeRemixOutputModel>> Mine(int chefId)
        => await this.Data.Set<RecipeRemix>()
                .Where(c => c.ChefId == chefId)
                .To<RecipeRemixOutputModel>()
                .ToListAsync();

        public async Task<int> Modify(RecipesRemixEditModel recipeInput)
        {
            var recipe = await this.Data.Set<RecipeRemix>()
                .Where(r => r.Id == recipeInput.Id)
                .FirstOrDefaultAsync();
            recipe.Name = recipeInput.Name;
            recipe.Ingredients = recipeInput.Ingredients;
            recipe.Instructions = recipeInput.Instructions;
            recipe.ImageUrl = recipeInput.ImageUrl;
            recipe.Vegan = recipeInput.Vegan;
            recipe.Vegetarian = recipeInput.Vegetarian;
            recipe.TypeOfDish = recipeInput.TypeOfDish;
            recipe.Calories = recipeInput.Calories;

            this.Data.Update(recipe);
            await this.Data.SaveChangesAsync();
            return recipe.Id;
        }

        //public async Task<int> Total(RecipesQuery query)
        //    => await this
        //            .GetRecipeQuery(query)
        //            .CountAsync();

    }
}
