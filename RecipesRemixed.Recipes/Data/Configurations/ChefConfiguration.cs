namespace RecipesRemixed.Recipes.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RecipesRemixed.Recipes.Data.Models;
    using static DataConstants.Chef;
    using static DataConstants.Recipes;

    internal class ChefConfiguration
        : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(c => c.Qualification)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
               .Property(c => c.Biography)
               .IsRequired()
               .HasMaxLength(MaxDescriptionLength);

        }
    }
}
