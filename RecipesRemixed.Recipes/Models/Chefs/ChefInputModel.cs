namespace RecipesRemixed.Recipes.Models.Chefs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using RecipesRemixed.Recipes.Data.Models;
    using static Data.DataConstants.Chef;
    using static Data.DataConstants.Recipes;

    public class ChefInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Qualification { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Biography { get; set; }

    }
}
