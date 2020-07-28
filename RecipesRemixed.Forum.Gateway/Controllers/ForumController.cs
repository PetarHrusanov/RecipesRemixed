namespace RecipesRemixed.Forum.Gateway.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Controllers;
    using RecipesRemixed.Forum.Gateway.Models.Posts;
    using RecipesRemixed.Forum.Gateway.Services.Recipes;
    using RecipesRemixed.Services.Identity;

    public class ForumController : ApiController
    {
        private readonly IForumService forum;
        private readonly ICurrentUserService currentUser;

        public ForumController(
            IForumService forum,
            ICurrentUserService currentUser)
        {
            this.forum = forum;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<IEnumerable<PostMineViewModel>> Mine()
            => await this.forum.Mine();
    }
}