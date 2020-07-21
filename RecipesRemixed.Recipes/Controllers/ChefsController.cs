namespace RecipesRemixed.Recipes.Controller
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Controllers;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Chefs;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;

    public class ChefsController : Controller
    {
        private readonly IChefsService chefs;
        private readonly ICurrentUserService currentUser;

        public ChefsController(
            IChefsService chefs,
            ICurrentUserService currentUser)
        {
            this.chefs = chefs;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string Id)
        {
            var userId = this.currentUser.UserId;
            var userIsDealer = await this.chefs.IsChef(userId);
            //if (userIsDealer)
            //{
            //    var chefId = await this.chefs.GetIdByUser(this.currentUser.UserId);
            //    return this.RedirectToAction("Details", new { id = chefId });
            //}
            //else
            //{
            //    return this.View();
            //}

            return this.View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userId = this.currentUser.UserId;
            var chefId = await this.chefs.GetIdByUser(userId);
            var chef = await this.chefs.GetDetails(chefId);
            return this.View(chef);
        }
            

        [HttpGet]
        [Authorize]
        [Route("Id")]
        public async Task<ActionResult<int>> GetChefId()
        {
            var userId = this.currentUser.UserId;

            var userIsDealer = await this.chefs.IsChef(userId);

            if (!userIsDealer)
            {
                return this.BadRequest("This user is not a dealer.");
            }

            return await this.chefs.GetIdByUser(this.currentUser.UserId);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(ChefInputModel input)
        {
            var userId = this.currentUser.UserId;
            await this.chefs.CreateChef(input, userId);
            var chefId = await GetChefId();
            return this.RedirectToAction("Details", chefId);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, ChefInputModel input)
        {
            var chef = await this.chefs.FindByUser(this.currentUser.UserId);

            if (id != chef.Id)
            {
                return BadRequest(Result.Failure("You cannot edit this dealer."));
            }

            // eventualno da iznesa tazi logika v service
            chef.Name = input.Name;
            chef.Qualification = input.Qualification;
            chef.Biography = input.Biography;

            await this.chefs.Save(chef);

            return Ok();
        }
    }
}
