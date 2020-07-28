namespace RecipesRemixed.Recipes
{
    using RecipesRemixed.Recipes.Data;
    using RecipesRemixed.Recipes.Infrastructure;
    using RecipesRemixed.Recipes.Infrastructure.Extensions;
    using RecipesRemixed.Recipes.Services;
    using RecipesRemixed.Recipes.Services.Recipes;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.RecipesRemix;
    using RecipesRemixed.Recipes.Services.Identity;
    using RecipesRemixed.Infrastructure;
    using RecipesRemixed.Services.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Refit;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using RecipesRemixed.Services.Mapping;
    using RecipesRemixed.Recipes.Data.Models;
    using System.Reflection;
    using RecipesRemixed.Recipes.Services.Forum;
    using RecipesRemixed.Recipes.Services.ForumGateway;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
               .GetSection(nameof(ServiceEndpoints))
               .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddWebService<RecipesDbContext>(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddTransient<IChefsService, ChefsService>()
                .AddTransient<IRecipesService, RecipesService>()
                .AddTransient<IRecipesRemixService, RecipesRemixService>()
                .AddMessaging()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                ;


            services.AddRazorPages();
            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services
                .AddRefitClient<IForumService>()
                .WithConfiguration(serviceEndpoints.Forum);

            services
                .AddRefitClient<IForumGatewayService>()
                .WithConfiguration(serviceEndpoints.ForumGateway);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            // Database Migrations
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseMiddleware<JwtCookieAuthenticationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}