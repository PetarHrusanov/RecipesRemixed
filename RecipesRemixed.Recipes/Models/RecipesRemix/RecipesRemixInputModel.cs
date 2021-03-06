﻿namespace RecipesRemixed.Recipes.Models.RecipesRemix
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using RecipesRemixed.Recipes.Data.Models.Enums;
    using static Data.DataConstants.Chef;
    using static Data.DataConstants.Recipes;
    using static Data.DataConstants.Url;
    using static Data.DataConstants.Calories;

    public class RecipesRemixInputModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Ingredients { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Instructions { get; set; }

        public TypeOfDish TypeOfDish { get; set; }

        [Required]
        [MinLength(MinUrlLength)]
        [MaxLength(MaxUrlLength)]
        public string ImageUrl { get; set; }

        [Required]
        //[MinLength(MinCalLength)]
        //[MaxLength(MaxCalLength)]
        public int Calories { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Allergies { get; set; }

        [Required]
        public int RecipeId { get; set; }

        //[Required]
        public int ChefId { get; set; }
    }
}
