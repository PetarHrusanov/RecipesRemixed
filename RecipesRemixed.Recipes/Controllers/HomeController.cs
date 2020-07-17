namespace RecipesRemixed.Recipes.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Identity;
    using RecipesRemixed.Recipes.Services.Identity;
    using static RecipesRemixed.Infrastructure.InfrastructureConstants;

    public class HomeController : HandleController
    {

        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeController(
                    IIdentityService identityService,
                    IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Privacy()
        {

            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Login(UserInputModel model)
        => await this.Handle(
                async () =>
                {
                    var result = await this.identityService
                        .Login(this.mapper.Map<UserInputModel>(model));

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction("Index"),
                failure: View("../Home/Index", model));

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInputModel user)
        {
            await this.identityService.Register(user);

            return this.RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
