namespace RecipesRemixed.Recipes.Models.Chefs
{
    using System;
    using System.Linq;
    using RecipesRemixed.Recipes.Data.Models;

    public class ChefDetailsOutputModel :ChefOutputModel
    {
        public int TotalRecipes { get; private set; }

        public int TotalRecipesRemix { get; private set; }

        //public void Mapping(Profile mapper)
        //    => mapper
        //        .CreateMap<Chef, ChefDetailsOutputModel>()
        //        .IncludeBase<Chef, ChefDetailsOutputModel>()
        //        .ForMember(d => d.TotalRecipes, cfg => cfg
        //            .MapFrom(d => d.Recipes.Count()))
        //        .ForMember(d => d.TotalRecipesRemix, cfg => cfg
        //            .MapFrom(d => d.RecipesRemix.Count()));

    }
}
