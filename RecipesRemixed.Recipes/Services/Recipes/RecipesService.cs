namespace RecipesRemixed.Recipes.Services.Recipes
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
    using RecipesRemixed.Recipes.Services.Recipes;

    public class RecipesService : DataService<Recipe>, IRecipesService
    {

        private const int RecipesPerPage = 10;

        public RecipesService(RecipesDbContext db)
        : base(db)
        {

        }

        public async Task<RecipeOutputModel> Create(RecipesInputModel recipeInput, int chefId)
        {
            var recipe = new Recipe
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
            };

            await this.Save(recipe);
            var output = await this.Data.Set<Recipe>().Where(r => r.Id == recipe.Id).To<RecipeOutputModel>().FirstOrDefaultAsync();

            return output;
        }

        public async Task<bool> Delete(int id)
        {
            var carAd = await this.Data.Set<Recipe>().FindAsync(id);

            if (carAd == null)
            {
                return false;
            }

            this.Data.Set<Recipe>().Remove(carAd);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<Recipe> Find(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<RecipeOutputModel> GetDetails(int id)
        {
            var recipe = await this.All().Where(r => r.Id == id).To<RecipeOutputModel>().FirstOrDefaultAsync();
            return recipe;

        }

        public async Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query)
        {
            //=> (await this.mapper
            //    .ProjectTo<RecipeOutputModel>(this
            //        .GetRecipeQuery(query))
            //    .ToListAsync())
            //    .Skip((query.Page - 1) * RecipesPerPage)
            //    .Take(RecipesPerPage);

            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var list = await Data.Set<Recipe>().To<T>().ToListAsync();
            //List<RecipeOutputModel> result = this.mapper.Map<List<Recipe>, List<RecipeOutputModel>>(list);
            //var result = this.mapper
            //        .ProjectTo<RecipeOutputModel>(this.Data.Set<Recipe>());

            return list;
        }

        public async Task<IEnumerable<RecipeOutputModel>> Mine(int chefId)
            => await this.Data.Set<Recipe>()
                .Where(c => c.ChefId == chefId)
                .To<RecipeOutputModel>()
                .ToListAsync();

        public async Task<int> Modify(RecipesEditModel recipeInput)
        {
            var recipe = await this.Data.Set<Recipe>()
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

        public async Task<int> Total(RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<RecipeOutputModel>> Filter(RecipesAllViewModel recipesFilter)
        {
            var recipes = this.All();

            if (recipesFilter.Name != null)
            {
                recipes = recipes.Where(c => c.Name.Contains(recipesFilter.Name));
            }

            if (recipesFilter.Ingredients != null)
            {
                recipes = recipes.Where(c => c.Ingredients.Contains(recipesFilter.Name));
            }

            //if (recipesFilter.TypeOfDish != null)
            //{
            //    recipes = recipes.Where(c => (c.TypeOfDish).Equals(recipesFilter.TypeOfDish));
            //}

            //if (recipesFilter.Vegan != null)
            //{
            //    recipes = recipes.Where(c => c.Vegan.Equals(recipesFilter.Vegan));
            //}

            //if (recipesFilter.Vegetarian != null)
            //{
            //    recipes = recipes.Where(c => c.Vegetarian.Equals(recipesFilter.Vegetarian));
            //}

            var chosenRecipes = await recipes.To<RecipeOutputModel>().ToListAsync();

            return chosenRecipes;

        }
    }
}
