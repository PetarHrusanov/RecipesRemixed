﻿namespace RecipesRemixed.Forum.Models.Comments
{
    public class CommentCreateRoutingModel
    {
        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string ChefName { get; set; }
    }
}
