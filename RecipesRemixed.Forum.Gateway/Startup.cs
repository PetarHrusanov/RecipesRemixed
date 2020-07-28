namespace RecipesRemixed.Forum.Gateway
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Refit;
    using RecipesRemixed.Infrastructure;
    using RecipesRemixed.Forum.Gateway.Services.Recipes;
    using RecipesRemixed.Forum.Gateway.Models.Forum;
    using RecipesRemixed.Services.Identity;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
               .GetSection(nameof(ServiceEndpoints))
               .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                //.AddAutoMapperProfile(Assembly.GetExecutingAssembly())
                .AddTokenAuthentication(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtHeaderAuthenticationMiddleware>()
                .AddControllers();

            services
                .AddRefitClient<IForumService>()
                .WithConfiguration(serviceEndpoints.Forum);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseJwtHeaderAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}