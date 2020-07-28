// <copyright file="RecipesController.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace RecipesRemixed.Recipes.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RecipesRemixed.Recipes.Data;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Recipes.Services.Recipes;

    public class RecipesRemixedController : AdministrationController
    {
        private readonly RecipesDbContext db;
        private readonly IRecipesService recipesService;

        public RecipesRemixedController(RecipesDbContext db, IRecipesService recipesService)
        {
            this.db = db;
            this.recipesService = recipesService;
        }

        // Recipes Logic
        public async Task<IActionResult> Index()
        {
            var recipes = await this.recipesService.GetAll<RecipeOutputModel>();
            return this.View(recipes);
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipesInputModel recipe)
        {
            await this.recipesService.Create(recipe, 1);

            this.TempData["CreatedRecipes"] = $"You have successfully created {recipe.Name}!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetDetails(id);
            var newEdit = new RecipesEditModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Allergies = recipe.Allergies,
                Calories = recipe.Calories,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                TypeOfDish = recipe.TypeOfDish,
                Vegan = recipe.Vegan,
                Vegetarian = recipe.Vegetarian
            };

            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(newEdit);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Instructions,ImageUrl,Vegan,Vegetarian,TypeOfDish,Allergies,Calories,Ingredients")]
        RecipesEditModel recipe)
        {
            if (recipe.Id ==null)
            {
                return NotFound();
            }

            
                try
                {
                    await this.recipesService.Modify(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this.recipesService.GetDetails(recipe.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception("A problem occurred while trying to edit this recipe.");
                    }
                }

                return this.RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetDetails(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var recipe = await this.recipesService.GetDetails(id);
            if (recipe == null)
            {
                return this.NotFound();
            }

            return this.View(recipe);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.recipesService.Delete(id);

            this.TempData["DeletedRecipes"] = $"You have successfully deleted this recipe!";

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Back()
        {
            return this.RedirectToAction("Index", "Dashboard");
        }
    }
}
