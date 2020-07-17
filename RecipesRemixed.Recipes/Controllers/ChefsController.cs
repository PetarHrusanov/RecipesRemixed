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
        public async Task<ActionResult<ChefDetailsOutputModel>> Details(int id)
        {
            var chef = await this.chefs.GetDetails(id);
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(ChefInputModel input,string userId)
        {
            await this.chefs.CreateChef(input, userId);
            return Ok();
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
