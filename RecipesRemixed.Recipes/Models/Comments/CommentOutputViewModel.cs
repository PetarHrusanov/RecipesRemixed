namespace RecipesRemixed.Recipes.Models.Comments
{
    public class CommentOutputViewModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        public string ChefName { get; set; }
    }
}
