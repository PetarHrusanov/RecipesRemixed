﻿namespace RecipesRemixed.Forum.Services.Posts
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
                    ChefName = item.ChefName,
                    //Comments = (IEnumerable<PostCommentViewModel>)item.Comments,

                };
                shemi.Add(post);
            }
            return shemi;

        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new Post
            {
                Content = content,
                Title = title,
                UserId = userId,
            };

            await this.db.Posts.AddAsync(post);
            this.db.SaveChanges();
            return post.Id;
        }

        public async Task<PostViewModel> GetById (int id)
        {
            var post = await this.db.Posts.Where(x => x.Id == id).FirstOrDefaultAsync();
            var comments = new List<CommentOutputViewModel>();
            foreach (var item in post.Comments)
            {
                var selskiCommentConvertor = new CommentOutputViewModel
                {
                    ChefName = item.ChefName,
                    Content = item.Content,
                    PostId = item.PostId
                };
                comments.Add(selskiCommentConvertor);
            }
            //var comments = post.Select(p => p.Comments);
            var postView = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ChefName = post.ChefName,
                Comments = comments
            };

            return postView;
        }
    }
}
