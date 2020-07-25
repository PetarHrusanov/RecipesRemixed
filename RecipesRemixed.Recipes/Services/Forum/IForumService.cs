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

        [Get("/Forum/Details/{id}")]
        Task<PostViewModel> Details(int id);

        [Post("/Forum/Create")]
        Task<PostViewModel> Create([Body] PostCreateRoutingModel inputModel);

        //[Post("/Identity/Register")]
        //Task<UserInputModel> Register([Body] UserInputModel loginInput);
    }
}
