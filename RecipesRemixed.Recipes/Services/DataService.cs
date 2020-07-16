namespace RecipesRemixed.Recipes.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;

    public abstract class DataService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        protected DataService(RecipesDbContext db) => this.Data = db;

        protected RecipesDbContext Data { get; }

        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();

        public async Task Save(
            TEntity entity)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync();
        }
    }
}
