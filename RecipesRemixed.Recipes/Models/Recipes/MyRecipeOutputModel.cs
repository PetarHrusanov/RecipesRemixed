namespace RecipesRemixed.Recipes.Models.Recipes
{
    using System;
    using AutoMapper;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Data.Models.Enums;

    public class MyRecipeOutputModel : IMapFrom<Recipe>
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

        public string ChefId { get; set; }

        public string ChefName { get; set; }

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Recipe, MyRecipeOutputModel>()
                .ForMember(ad => ad.ChefName, cfg => cfg
                    .MapFrom(ad => ad.Chef.Name));
    }
}
