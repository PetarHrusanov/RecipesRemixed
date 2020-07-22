namespace RecipesRemixed.Recipes.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Recipes.Models.RecipesRemix;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Recipes;
    using RecipesRemixed.Recipes.Services.RecipesRemix;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;

    public class RecipesRemixController : Controller
    {
        private readonly IRecipesRemixService recipesRemix;
        private readonly IChefsService chefs;
        private readonly ICurrentUserService currentUser;
        private readonly IRecipesService basicRecipes;

        public RecipesRemixController(
            IRecipesRemixService recipesRemix,
            IChefsService chefs,
            ICurrentUserService currentUser,
            IRecipesService basicRecipes)
        {
            this.recipesRemix = recipesRemix;
            this.chefs = chefs;
            this.currentUser = currentUser;
            this.basicRecipes = basicRecipes;
        }

        public async Task<IActionResult> Index()
        {

            var recipeList = new RecipesRemixAllViewModel
            {
                Recipes = await this.recipesRemix.GetAll<RecipeRemixOutputModel>()
            };

            return this.View(recipeList);
        }

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            var chef = await this.chefs.GetIdByUser(currentUser.UserId);
            var recipe =  await this.basicRecipes.Find(id);
            var recipeRemix = new RecipesRemixInputModel
            {
                Name = recipe.Name,
                Allergies = recipe.Allergies,
                Instructions = recipe.Instructions,
                Calories = recipe.Calories,
                TypeOfDish = recipe.TypeOfDish,
                Ingredients = recipe.Ingredients,
                Vegan = recipe.Vegan,
                Vegetarian = recipe.Vegetarian,
                ImageUrl = recipe.ImageUrl,
                RecipeId = recipe.Id,
                ChefId = chef
            };
            return this.View(recipeRemix);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeOutputModel>> Create(RecipesRemixInputModel input)
        {
            var chef = await this.chefs.FindByUser(this.currentUser.UserId);
            await this.recipesRemix.Create(input, chef.Id);
            return this.RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult<RecipesSearchOutputModel>> Search(
            [FromQuery] RecipesQuery query)
        {
            //var carAdListings = await this.recipesRemix.GetListings(query);

            //var totalPages = await this.recipesRemix.Total(query);

            //return new RecipesSearchOutputModel();
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Route("RecipesRemix/{id:int}")]
        public async Task<ActionResult<RecipeOutputModel>> Details(int id)
        { 
            var recipe = await this.recipesRemix.GetDetails(id);
            return this.View(recipe);
        }

        // eventualno da go izkaram v service
        [HttpPut]
        [Authorize]
        //[Route(Id)]
        public async Task<ActionResult> Edit(int id, RecipesInputModel input)
        {
            var chefId = await this.chefs.GetIdByUser(this.currentUser.UserId);

            var chefHasRecipe = await this.chefs.HasRecipe(chefId, id);

            if (!chefHasRecipe)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            var recipe = await this.recipesRemix.Find(id);

            recipe.Name = input.Name;
            recipe.Ingredients = input.Ingredients;
            recipe.TypeOfDish = input.TypeOfDish;
            recipe.ImageUrl = input.ImageUrl;
            recipe.Calories = input.Calories;
            recipe.Vegetarian = input.Vegetarian;
            recipe.Vegan = input.Vegan;
            recipe.Allergies = input.Allergies;

            await this.recipesRemix.Save(recipe);

            return Result.Success;
        }

        [HttpDelete]
        [Authorize]
        //[Route(Id)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var chefId = await this.chefs.GetIdByUser(this.currentUser.UserId);

            var chefHasRecipe = await this.chefs.HasRecipe(chefId, id);

            if (!chefHasRecipe)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            return await this.recipesRemix.Delete(id);
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MyRecipesOutputModel>> Mine(
            [FromQuery] RecipesQuery query)
        {
            var chefId = await this.chefs.GetIdByUser(this.currentUser.UserId);

            var recipesListings = await this.recipesRemix.Mine(chefId, query);

            var totalPages = await this.recipesRemix.Total(query);

            //return new MyRecipesOutputModel(recipesListings, query.Page, totalPages);

            return this.RedirectToAction("Index");
        }


        //[HttpPut]
        //[Authorize]
        //[Route(Id + PathSeparator + nameof(ChangeAvailability))]
        //public async Task<ActionResult> ChangeAvailability(int id)
        //{
        //    var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

        //    var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

        //    if (!dealerHasCar)
        //    {
        //        return BadRequest(Result.Failure("You cannot edit this car ad."));
        //    }

        //    var carAd = await this.carAds.Find(id);

        //    carAd.IsAvailable = !carAd.IsAvailable;

        //    await this.carAds.Save(carAd);

        //    return Result.Success;
        //}
    }
}
