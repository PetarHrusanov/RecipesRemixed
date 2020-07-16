namespace RecipesRemixed.Recipes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static DataConstants.Chef;
    using static DataConstants.Recipes;

    internal class RecipesConfiguration
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(c => c.Ingredients)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
               .Property(c => c.Instructions)
               .IsRequired()
               .HasMaxLength(MaxDescriptionLength);

            builder
               .Property(c => c.TypeOfDish)
               .IsRequired();

            // eventualno da opravq URL-ite i da iznesa globalni konstanti
            builder
               .Property(c => c.ImageUrl)
               .IsRequired()
               .HasMaxLength(MaxDescriptionLength);

            builder
               .Property(c => c.Calories)
               .IsRequired();

            builder
               .Property(c => c.Vegetarian)
               .IsRequired();

            builder
               .Property(c => c.Vegan)
               .IsRequired();

            builder
              .Property(c => c.Allergies)
              .IsRequired();

            builder
                .HasOne(d => d.Chef)
                .WithMany(c => c.Recipes)
                .HasForeignKey(c => c.ChefId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(d => d.RecipesRemix)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
