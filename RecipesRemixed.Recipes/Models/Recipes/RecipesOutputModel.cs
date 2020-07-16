namespace RecipesRemixed.Recipes.Models.Recipes
{
    using AutoMapper;
    using Data.Models;
    using RecipesRemixed.Recipes.Data.Models.Enums;

    public class RecipesOutputModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public TypeOfDish TypeOfDish { get; set; }

        public string ImageUrl { get; set; }

        public int Calories { get; set; }

        public bool Vegetarian { get; set; }

        public bool Vegan { get; set; }

        public string Allergies { get; set; }
    }
}
