namespace RecipesRemixed.Recipes.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Infrastructure;
    using RecipesRemixed.Recipes.Controller;
    using RecipesRemixed.Recipes.Models.Identity;
    using RecipesRemixed.Recipes.Services.Identity;
    using static RecipesRemixed.Infrastructure.InfrastructureConstants;

    public class IdentityController : HandleController
    {

        private readonly IIdentityService identityService;
        //private readonly IMapper mapper;

        public IdentityController(IIdentityService identityService)
        { 

            this.identityService = identityService;
            //this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
            => this.View();

        [HttpPost]
        public async Task<IActionResult> Login(UserInputModel model)
        {
            var result = await this.identityService
                        .Login(model);

            this.Response
                .Cookies.Append(
                AuthenticationCookieName,
                result.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    //Secure = true,
                    MaxAge = TimeSpan.FromDays(1)
                });

            return RedirectToAction(nameof(ChefsController.Index), "Chefs");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInputModel user)
        {
            var input = await this.identityService.Register(user);

            return await this.Login(input);

        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(InfrastructureConstants.AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
