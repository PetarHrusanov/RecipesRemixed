namespace RecipesRemixed.Forum.Services.Comments
{
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Services;

    public interface ICommentsService : IDataService<Comment>
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        //Task<bool> IsInPostIdAsync(int commentId, int postId);
    }
}
