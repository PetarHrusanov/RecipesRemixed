namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using RecipesRemixed.Recipes.Data.Models.Enums;
    using static Data.DataConstants.Chef;
    using static Data.DataConstants.Recipes;
    using static Data.DataConstants.Url;
    using static Data.DataConstants.Calories;

    public class RecipesInputModel
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
        [MinLength(MinCalLength)]
        [MaxLength(MaxCalLength)]
        [RegularExpression(CaloriesRegEx)]
        public int Calories { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        [Required]
        public bool Vegan { get; set; }

        [Required]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Allergies { get; set; }
    }
}
