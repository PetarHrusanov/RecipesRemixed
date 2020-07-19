namespace RecipesRemixed.Recipes.Models.RecipesRemix
{
    using Data.Models;
    using RecipesRemixed.Recipes.Data.Models.Enums;
    using RecipesRemixed.Services.Mapping;

    public class RecipeRemixOutputModel : IMapFrom<RecipeRemix>
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

        public string RecipeId { get; set; }

        //public virtual void Mapping(Profile mapper)
        //    => mapper
        //        .CreateMap<Recipe, RecipeOutputModel>()
        //        .ForMember(ad => ad.ChefName, cfg => cfg
        //            .MapFrom(ad => ad.Chef.Name));
    }
}
