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
    using RecipesRemixed.Services;

    public class RecipesService : DataService<Recipe>, IRecipesService
    {

        private const int RecipesPerPage = 10;

        private readonly IMapper mapper;

        public RecipesService(RecipesDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

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

            var output = mapper.Map<Recipe, RecipeOutputModel>(recipe);

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
            var recipe = await this.Data.Set<Recipe>().Where(r => r.Id == id).FirstOrDefaultAsync();

            var output = mapper.Map<Recipe, RecipeOutputModel>(recipe);

            return output;

        }

        public async Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query)
            => (await this.mapper
                .ProjectTo<RecipeOutputModel>(this
                    .GetRecipeQuery(query))
                .ToListAsync())
                .Skip((query.Page - 1) * RecipesPerPage)
                .Take(RecipesPerPage);

        //public async Task<IEnumerable<RecipeOutputModel>> GetAll()
        //=> (await this.mapper
        //        .ProjectTo<RecipeOutputModel>(this.All())
        //        .ToListAsync());

        public async Task<IEnumerable<RecipeOutputModel>> GetAll()
        {
            var list = await  this.Data.Set<Recipe>().ToListAsync();
            List<RecipeOutputModel> result = this.mapper.Map<List<Recipe>, List<RecipeOutputModel>>(list);
            //var result = this.mapper
            //        .ProjectTo<RecipeOutputModel>(this.Data.Set<Recipe>());

            return result;
        }
            




        public async Task<IEnumerable<MyRecipeOutputModel>> Mine(int chefId, RecipesQuery query)
             => (await this.mapper
                    .ProjectTo<MyRecipeOutputModel>(this
                        .GetRecipeQuery(query, chefId))
                    .ToListAsync())
                    .Skip((query.Page - 1) * RecipesPerPage)
                    .Take(RecipesPerPage);

        // da vidq modify kak se pravi v controller-a i da go izkaram 
        public Task<bool> Modify(RecipesInputModel recipeInput)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Total(RecipesQuery query)
            => await this
                    .GetRecipeQuery(query)
                    .CountAsync();

        private IQueryable<Recipe> GetRecipeQuery(
            RecipesQuery query, int? chefId = null)
        {
            var dataQuery = this.All();

            if (chefId.HasValue)
            {
                dataQuery = dataQuery.Where(c => c.ChefId == chefId);
            }

            if (query.Vegan.HasValue)
            {
                dataQuery = dataQuery.Where(c => c.Vegan == query.Vegan);
            }

            if (query.Vegetarian.HasValue)
            {
                dataQuery = dataQuery.Where(c => c.Vegetarian == query.Vegetarian);
            }

            if (!string.IsNullOrWhiteSpace(query.Ingredients))
            {
                dataQuery = dataQuery.Where(c => c
                    .Ingredients.ToLower().Contains(query.Ingredients.ToLower()));
            }

            return dataQuery;
        }
    }
}
