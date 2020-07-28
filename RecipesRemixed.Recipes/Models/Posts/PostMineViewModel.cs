﻿namespace RecipesRemixed.Recipes.Models.Posts
{
    using System;
    using System.Collections.Generic;

    public class PostMineViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        //public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string ChefName { get; set; }

    }
}
