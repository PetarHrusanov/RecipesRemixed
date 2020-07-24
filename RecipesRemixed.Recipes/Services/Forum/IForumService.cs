using System;
using System.Threading.Tasks;
using RecipesRemixed.Recipes.Models.Identity;
using RecipesRemixed.Recipes.Models.Posts;
using Refit;

namespace RecipesRemixed.Recipes.Services.Forum
{
    public interface IForumService
    {
        [Get("/Forum/Index")]
        Task<PostsAllViewModel> Index();

        //[Post("/Identity/Register")]
        //Task<UserInputModel> Register([Body] UserInputModel loginInput);
    }
}
