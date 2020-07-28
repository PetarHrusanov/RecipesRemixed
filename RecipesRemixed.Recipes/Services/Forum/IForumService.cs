namespace RecipesRemixed.Recipes.Services.Forum
{
    using System;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Models.Comments;
    using RecipesRemixed.Recipes.Models.ForumUser;
    using RecipesRemixed.Recipes.Models.Identity;
    using RecipesRemixed.Recipes.Models.Posts;
    using Refit;

    public interface IForumService
    {
        [Get("/Forum/Index")]
        Task<PostsAllViewModel> Index();

        [Get("/Forum/Details/{id}")]
        Task<PostViewModel> Details(int id);

        [Post("/Forum/Create")]
        Task<PostViewModel> Create([Body] PostCreateRoutingModel inputModel);

        [Post("/Forum/CreateComment")]
        Task<int> CreateComment([Body] CommentCreateRoutingModel input);

        [Get("/Forum/UserExists")]
        Task<bool> UserExists(string userId);

        [Post("/Forum/CreateUser")]
        Task<int> CreateUser([Body] ForumUserInputModel user);

        //[Post("/Identity/Register")]
        //Task<UserInputModel> Register([Body] UserInputModel loginInput);
    }
}
