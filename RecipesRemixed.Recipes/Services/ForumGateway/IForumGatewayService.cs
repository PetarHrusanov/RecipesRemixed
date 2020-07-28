namespace RecipesRemixed.Recipes.Services.ForumGateway
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipesRemixed.Recipes.Models.Posts;
    using Refit;

    public interface IForumGatewayService
    {
        [Get("/Forum/Mine")]
        Task<IEnumerable<PostViewModel>> Mine();
    }
}
