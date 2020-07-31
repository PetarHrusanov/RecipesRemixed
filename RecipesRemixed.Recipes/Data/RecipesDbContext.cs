namespace RecipesRemixed.Recipes.Data
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Data;
    using RecipesRemixed.Recipes.Data.Models;

    public class RecipesDbContext : MessageDbContext
    {
        public RecipesDbContext(DbContextOptions<RecipesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Chef> Chefs { get; set; }

        public DbSet<RecipeRemix> RecipesRemix { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}