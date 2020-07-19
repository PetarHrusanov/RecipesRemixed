using System;
using System.Threading.Tasks;
using RecipesRemixed.Recipes.Models.Identity;
using Refit;

namespace RecipesRemixed.Recipes.Services.Identity
{
    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserInputModel loginInput);

        [Post("/Identity/Register")]
        Task<UserInputModel> Register([Body] UserInputModel loginInput);
    }
}
