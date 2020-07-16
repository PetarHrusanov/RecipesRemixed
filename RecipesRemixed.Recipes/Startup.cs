namespace RecipesRemixed.Recipes
{
    using System.Reflection;
    using AutoMapper;
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RecipesRemixed.Infrastructure;
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
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddTransient<IRecipesService, RecipesService>()
                .AddTransient<IChefsService, ChefsService>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize()
                .SeedData();
    }
}
