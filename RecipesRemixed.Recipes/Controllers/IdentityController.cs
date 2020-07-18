﻿namespace RecipesRemixed.Recipes.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Recipes.Models.Identity;
    using RecipesRemixed.Recipes.Services.Identity;
    using static RecipesRemixed.Infrastructure.InfrastructureConstants;

    public class IdentityController : HandleController
    {

        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public IdentityController(
                    IIdentityService identityService,
                    IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
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
    }
}