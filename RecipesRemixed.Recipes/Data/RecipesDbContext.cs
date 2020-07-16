namespace RecipesRemixed.Recipes.Data
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Recipes.Data.Models;

    public class RecipesDbContext :DbContext
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Chef> Chefs { get; set; }

        public DbSet<RecipeRemix> RecipesRemix { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
