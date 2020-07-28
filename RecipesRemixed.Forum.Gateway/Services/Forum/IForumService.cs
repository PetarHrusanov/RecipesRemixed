namespace RecipesRemixed.Forum.Gateway.Services.Recipes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Gateway.Models.Posts;
    using Refit;

    public interface IForumService
    {
        [Get("/Forum/Mine")]
        Task<IEnumerable<PostMineViewModel>> Mine();
    }
}
