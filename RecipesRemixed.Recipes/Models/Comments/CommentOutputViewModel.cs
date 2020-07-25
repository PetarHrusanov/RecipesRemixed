namespace RecipesRemixed.Recipes.Models.Comments
{
    public class CommentOutputViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        //public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ChefName { get; set; }
    }
}
