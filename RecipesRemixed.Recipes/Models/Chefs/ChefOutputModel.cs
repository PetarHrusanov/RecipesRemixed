namespace RecipesRemixed.Recipes.Models.Chefs
{
    using System;
    using System.Collections.Generic;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Services.Mapping;

    public class ChefOutputModel : IMapFrom<Chef>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Qualification { get; set; }

        public string Biography { get; set; }

        // da opraq mapp-vaneto

        //public IEnumerable<RecipeOutputModel> Recipes { get; set; }

        //public IEnumerable<RecipeOutputModel> RecipesRemix { get; set; }

        //public virtual void Mapping(Profile mapper)
        //    => mapper
        //        .CreateMap<Chef, ChefOutputModel>()
        //        .ForMember(r => r.Recipes, cfg => cfg
        //            .MapFrom(r => r.Recipes))
        //        .ForMember(r => r.RecipesRemix, cfg => cfg
        //            .MapFrom(r => r.RecipesRemix));

    }
}
