namespace RecipesRemixed.Recipes
{
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RecipesRemixed.Infrastructure;
    using RecipesRemixed.Recipes.Data;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Recipes;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<RecipesDbContext>(this.Configuration)
                //.AddTransient<IDataSeeder, DealersDataSeeder>()
                .AddTransient<IChefsService, ChefsService>()
                .AddTransient<IRecipesService, RecipesService>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}
