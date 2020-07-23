namespace RecipesRemixed.Recipes.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Recipes;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IChefsService chefs;
        private readonly ICurrentUserService currentUser;

        public RecipesController(
            IRecipesService recipes,
            IChefsService chefs,
            ICurrentUserService currentUser)
        {
            this.recipes = recipes;
            this.chefs = chefs;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var recipeList = new RecipesAllViewModel
            {
                Recipes = await this.recipes.GetAll<RecipeOutputModel>()
            };

            return this.View(recipeList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RecipesAllViewModel filterRecipes)
        {

            var recipeList = new RecipesAllViewModel
            {
                Recipes = await this.recipes.Filter(filterRecipes)
            };

            return this.View(recipeList);
        }

        [HttpGet]
        public async Task<ActionResult> Create(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult<RecipeOutputModel>> Create(RecipesInputModel input)
        {
            var chef = await this.chefs.FindByUser(this.currentUser.UserId);
            await this.recipes.Create(input, chef.Id);
            return this.RedirectToAction("Index");

        }


        //[HttpGet]
        //public async Task<ActionResult<RecipesSearchOutputModel>> Search(RecipesAllViewModel query)
        //{
        //    var carAdListings = await this.recipes.GetListings(query);

        //    var totalPages = await this.recipes.Total(query);

        //    return new RecipesSearchOutputModel(carAdListings, query.Page, totalPages);
        //}

        [HttpGet]
        [Route("Recipes/{id:int}")]
        public async Task<ActionResult<RecipeOutputModel>> Details(int id)
        { 
            var recipe = await this.recipes.GetDetails(id);
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

            var recipe = await this.recipes.Find(id);

            recipe.Name = input.Name;
            recipe.Ingredients = input.Ingredients;
            recipe.TypeOfDish = input.TypeOfDish;
            recipe.ImageUrl = input.ImageUrl;
            recipe.Calories = input.Calories;
            recipe.Vegetarian = input.Vegetarian;
            recipe.Vegan = input.Vegan;
            recipe.Allergies = input.Allergies;

            await this.recipes.Save(recipe);

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

            return await this.recipes.Delete(id);
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MyRecipesOutputModel>> Mine(
            [FromQuery] RecipesQuery query)
        {
            var chefId = await this.chefs.GetIdByUser(this.currentUser.UserId);

            var recipesListings = await this.recipes.Mine(chefId, query);

            var totalPages = await this.recipes.Total(query);

            return new MyRecipesOutputModel(recipesListings, query.Page, totalPages);
        }
    }
}
