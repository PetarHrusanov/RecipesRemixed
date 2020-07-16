namespace RecipesRemixed.Identity.Services.Identity
{
    using System.Threading.Tasks;
    using Data.Models;
    using Models.Identity;
    using RecipesRemixed.Services;

    public interface IIdentityService
    {
        Task<Result<User>> Register(UserInputModel userInput);

        Task<Result<UserOutputModel>> Login(UserInputModel userInput);

        Task<Result> ChangePassword(string userId, ChangePasswordInputModel changePasswordInput);
    }
}
