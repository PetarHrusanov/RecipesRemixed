using System;
namespace RecipesRemixed.Recipes.Models.Posts
{
    public class PostCreateRoutingModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string ChefName { get; set; }
    }
}
