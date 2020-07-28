namespace RecipesRemixed.Forum.Services.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Forum.Models.Posts;
    using RecipesRemixed.Services;

    public interface IPostsService
    {

        Task<IEnumerable<PostViewModel>> All();

        Task<int> Create(PostCreateRoutingModel post);

        Task<PostViewModel> GetById(int id);

        Task<IEnumerable<PostMineViewModel>> Mine(string userId);

    }
}
