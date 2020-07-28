using System;
namespace RecipesRemixed.Recipes.Services.Chefs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Recipes.Models.Chefs;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Mapping;

    public class ChefsService : DataService<Chef>, IChefsService
    {

        public ChefsService(RecipesDbContext db)
            : base(db)
        {
        }


        public async Task<int> CreateChef (ChefInputModel input, string userId)
        {
            var chef = new Chef
            {
                Name = input.Name,
                Qualification = input.Qualification,
                Biography = input.Biography,
                UserId = userId
            };

            await this.Save(chef);
            return chef.Id;
        }

        public async Task<bool> HasRecipe(int chefId, int recipeId)
            => await this
                .All()
                .Where(d => d.Id == chefId)
                .AnyAsync(d => d.Recipes
                    .Any(c => c.Id == recipeId));

        public async Task<bool> IsChef(string userId)
            => await this
                .All()
                .AnyAsync(d => d.UserId == userId);

        public async Task<ChefOutputModel> GetDetails(int id)
        {
            var chef = await this.Data.Set<Chef>().Where(r => r.Id == id).To< ChefOutputModel >().FirstOrDefaultAsync();
            return chef;
        }

        public async Task<ChefOutputModel> GetDetailsByRecipeId(int recipeId)
        {
            //=> await this.mapper
            //    .ProjectTo<ChefOutputModel>(this
            //        .All()
            //        .Where(d => d.Recipes.Any(c => c.Id == recipeId)))
            //    .SingleOrDefaultAsync();

            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ChefOutputModel>> GetAll()
        {
            var list = await Data.Set<Chef>().To<ChefOutputModel>().ToListAsync();
            return list;
        }

        public Task<int> GetIdByUser(
            string userId)
            => this.FindByUser(userId, chef => chef.Id);

        public Task<Chef> FindByUser(string userId)
            => this.FindByUser(userId, chef => chef);

        public async Task<ChefOutputModel> GetDetailsByUserId(string userId)
        {
            var chefOutput = await this.Data.Set<Chef>()
                .Where(u => u.UserId == userId)
                .To<ChefOutputModel>()
                .FirstOrDefaultAsync();
            return chefOutput;
        }

        private async Task<T> FindByUser<T>(
            string userId,
            Expression<Func<Chef, T>> selector)
        {
            var chefData = await this
                .Data.Set<Chef>()
                .Where(u => u.UserId == userId)
                .Select(selector)
                .FirstOrDefaultAsync();

            if (chefData == null)
            {
                throw new InvalidOperationException("This user is not a Chef.");
            }

            return chefData;
        }

        public async Task<bool> Delete(int id)
        {
            var chef = await this.Data.Set<Chef>().FindAsync(id);

            if (chef == null)
            {
                return false;
            }

            this.Data.Set<Chef>().Remove(chef);

            await this.Data.SaveChangesAsync();

            return true;
        }

        public async Task<int> Modify(ChefEditModel chefInput)
        {
            var chef = await this.Data.Set<Chef>()
                .Where(r => r.Id == chefInput.Id)
                .FirstOrDefaultAsync();
            chef.Name = chefInput.Name;
            chef.Qualification = chefInput.Qualification;
            chef.Biography = chefInput.Biography;


            this.Data.Update(chef);
            await this.Data.SaveChangesAsync();
            return chef.Id;
        }

    }
}
