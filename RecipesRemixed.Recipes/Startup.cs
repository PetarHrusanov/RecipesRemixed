namespace RecipesRemixed.Recipes
{
    using System.Reflection;
    using System.Text;
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using RecipesRemixed.Infrastructure;
    using RecipesRemixed.Recipes.Data;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Services;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Identity;
    using RecipesRemixed.Recipes.Services.Recipes;
    using RecipesRemixed.Services.Identity;
    using RecipesRemixed.Services.Mapping;
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

            services.AddDbContext<RecipesDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddWebService<RecipesDbContext>(this.Configuration)
                //.AddTransient<IDataSeeder, DealersDataSeeder>()
                .AddTransient<IRecipesService, RecipesService>()
                .AddTransient<IChefsService, ChefsService>()
                //.AddTokenAuthentication(this.Configuration)
                //.AddScoped<ICurrentTokenService, CurrentTokenService>()
                //.AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            var secret = Configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));
  
            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services.AddHttpContextAccessor();

            services.AddRazorPages();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var dbContext = serviceScope.ServiceProvider.GetRequiredService<RecipesDbContext>();

            //    dbContext.Database.Migrate();

            //    //if (env.IsDevelopment())
            //    //{
            //    //    dbContext.Database.Migrate();
            //    //}

            //    //new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            //}

            //app
            //      .UseWebService(env)
            //      .Initialize();

            app
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                //.UseJwtCookieAuthentication()
                //.UseAuthentication()
                //.UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapDefaultControllerRoute());

            //app
            //    .UseMiddleware<JwtCookieAuthenticationMiddleware>()
            //    .UseAuthentication();

            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute("recipesDetails", "Recipes/{name:minlength(3)}", new { controller = "Recipes", action = "Details" });
                    endpoints.MapRazorPages();
                });
        }
    }
}
