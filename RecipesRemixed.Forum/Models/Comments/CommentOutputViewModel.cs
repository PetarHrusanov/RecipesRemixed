using System;
using RecipesRemixed.Forum.Data.Models;
using RecipesRemixed.Services.Mapping;

namespace RecipesRemixed.Forum.Models.Comments
{
    public class CommentOutputViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        public string ChefName { get; set; }
    }
}
