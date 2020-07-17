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
    using RecipesRemixed.Recipes.Services;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Identity;
    using RecipesRemixed.Recipes.Services.Recipes;
    using Refit;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                .GetSection(nameof(ServiceEndpoints))
                .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                  .AddWebService<RecipesDbContext>(this.Configuration)
                  //.AddTransient<IDataSeeder, DealersDataSeeder>()
                  .AddTransient<IChefsService, ChefsService>()
                  .AddTransient<IRecipesService, RecipesService>()
                  .AddControllersWithViews(options => options
                      .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            //services
            //    .AddRefitClient<IStatisticsService>()
            //    .WithConfiguration(serviceEndpoints.Statistics);

            //services
            //    .AddRefitClient<IDealersService>()
            //    .WithConfiguration(serviceEndpoints.Dealers);

            services.AddRazorPages();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                  .UseWebService(env)
                  .Initialize();

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                //.UseJwtCookieAuthentication()
                //.UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapDefaultControllerRoute());
        }




    }
}
