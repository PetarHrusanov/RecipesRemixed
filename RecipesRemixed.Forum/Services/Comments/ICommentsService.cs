namespace RecipesRemixed.Forum.Services.Comments
{
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Forum.Models.Comments;
    using RecipesRemixed.Services;

    public interface ICommentsService
    {
        Task<int> Create(CommentCreateRoutingModel input);

        Task<bool> IsInPostId(int commentId, int postId);
    }
}
