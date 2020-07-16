namespace RecipesRemixed.Recipes.Services.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Recipes.Models.Recipes;

    public class RecipesService : DataService<Recipe>, IRecipesService
    {

        private const int RecipesPerPage = 10;

        private readonly IMapper mapper;

        public RecipesService(RecipesDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<RecipeOutputModel> CreateAsync(RecipesInputModel recipeInput, int chefId)
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

            var output = recipe.ProjectTo<RecipeOutputModel>();

            this.GetDetails(recipe.Id);
        }

        public async Task<bool> Delete(int id)
        {
            var carAd = await this.Data.Recipes.FindAsync(id);

            if (carAd == null)
            {
                return false;
            }

            this.Data.Recipes.Remove(carAd);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<Recipe> Find(int id)
            => await this
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<RecipeOutputModel> GetDetails(int id)
            => await this.mapper
                .ProjectTo<RecipeOutputModel>(this.mapper.Where(r=> r.Id == id))
                .FirstOrDefaultAsync();

        public Task<IEnumerable<RecipeOutputModel>> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<RecipeOutputModel>> Mine(int chefId, RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Modify(RecipesInputModel recipeInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Total(RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
