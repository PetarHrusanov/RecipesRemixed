namespace RecipesRemixed.Forum.Services.Comments
{
    using System.Linq;
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Services;
    using RecipesRemixed.Forum.Data;

    public class CommentsService : DataService<Comment>, ICommentsService
    {

        public CommentsService(ForumDbContext db)
            : base(db)
        {
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                UserId = userId,
            };
            await this.Save(comment);
        }

        //public async Task<bool> IsInPostId(int commentId, int postId)
        //{
        //    var commentPostId = await Data.Set<Comment>().AsParallel().Where(x => x.Id == commentId)
        //        .Select(x => x.PostId).FirstOrDefaultAsync();


        //    return commentPostId == postId;
        //}
    }
}
