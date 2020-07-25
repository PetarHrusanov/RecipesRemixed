namespace RecipesRemixed.Forum.Services.Comments
{
    using System.Linq;
    using System.Threading.Tasks;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Services;
    using RecipesRemixed.Forum.Data;
    using RecipesRemixed.Forum.Models.Comments;

    public class CommentsService :ICommentsService
    {

        private readonly ForumDbContext db;

        public CommentsService(ForumDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(CommentCreateRoutingModel input)
        {
            
            var comment = new Comment
            {
                PostId = input.PostId,
                Content = input.Content,
                UserId = input.UserId,
                ChefName = input.ChefName
            };
            if (input.ParentId == 0)
            {
                comment.ParentId = null;
            }
            else if(input.ParentId!=0)
            {
                comment.ParentId = input.ParentId;
            }
            
            await this.db.Comments.AddAsync(comment);
            this.db.SaveChanges();
            return comment.PostId;
        }

        public async Task<bool> IsInPostId(int commentId, int postId)
        {
            //var commentPostId = await Data.Set<Comment>().AsParallel().Where(x => x.Id == commentId)
            //    .Select(x => x.PostId).FirstOrDefaultAsync();


            //return commentPostId == postId;
            return false;
        }
    }
}
