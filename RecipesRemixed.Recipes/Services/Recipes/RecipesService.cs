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

        public Task<int> CreateAsync(RecipesInputModel recipeInput)
        {
            throw new System.NotImplementedException();
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

        public Task<IEnumerable<RecipeOutputModel>> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAllPaginatedAsync<T>(int? take, int skip)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsyn<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByNameAsync<T>(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<RecipeOutputModel>> GetListings(RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetRecipesCountAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<RecipeOutputModel>> Mine(int chefId, RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ModifyAsync(RecipesInputModel recipeInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Total(RecipesQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
