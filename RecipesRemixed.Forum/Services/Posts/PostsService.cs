namespace RecipesRemixed.Forum.Services.Posts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Forum.Data;
    using RecipesRemixed.Forum.Data.Models;
    using RecipesRemixed.Forum.Models.Comments;
    using RecipesRemixed.Forum.Models.Posts;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;
    using RecipesRemixed.Services.Mapping;

    public class PostsService : IPostsService
    {

        private readonly ForumDbContext db;
        private readonly ICurrentUserService user;


        public PostsService(ForumDbContext db, ICurrentUserService user)
        {
            this.db = db;
            this.user = user;
        }

        public async Task<IEnumerable<PostViewModel>> All()
        {
            var list = this.db.Posts.AsNoTracking();
            var shemi = new List<PostViewModel>();
            foreach (var item in list)
            {
                var post = new PostViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Content = item.Content,
                    UserId = item.UserId

                };
                shemi.Add(post);
            }
            return shemi;

        }

        public async Task<int> Create(PostCreateRoutingModel postInput)
        {
            var post = new Post
            {
                Content = postInput.Content,
                Title = postInput.Title,
                UserId = postInput.UserId,
            };

            await this.db.Posts.AddAsync(post);
            this.db.SaveChanges();
            return post.Id;
        }

        public async Task<PostViewModel> GetById (int id)
        {
            var post = await this.db.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
            var commentsToPost = await this.db.Comments.Where(p => p.PostId == post.Id).ToListAsync();
            var comments = new List<CommentOutputViewModel>();
            foreach (var item in commentsToPost)
            {
                var selskiCommentConvertor = new CommentOutputViewModel
                {
                    //ChefName = item.ChefName,
                    Content = item.Content,
                    ParentId = item.ParentId,
                    UserId = item.UserId
                    
                };
                comments.Add(selskiCommentConvertor);
            }
            //var comments = post.Select(p => p.Comments);
            var postView = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
                //ChefName = post.ChefName,
                Comments = comments
            };

            return postView;
        }
    }
}
