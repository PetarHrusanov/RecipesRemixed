using System;
using RecipesRemixed.Forum.Data.Models;
using RecipesRemixed.Services.Mapping;

namespace RecipesRemixed.Forum.Models.Comments
{
    public class CommentOutputViewModel :IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        //public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ChefName { get; set; }
    }
}
