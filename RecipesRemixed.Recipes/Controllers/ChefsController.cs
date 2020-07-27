namespace RecipesRemixed.Recipes.Controller
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Controllers;
    using RecipesRemixed.Messages.ForumUser;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Chefs;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;

    public class ChefsController : Controller
    {
        private readonly IChefsService chefs;
        private readonly ICurrentUserService currentUser;
        private readonly IBus publisher;

        public ChefsController(
            IChefsService chefs,
            ICurrentUserService currentUser,
            IBus publisher)
        {
            this.chefs = chefs;
            this.currentUser = currentUser;
            this.publisher = publisher;
        }

        [HttpGet]
        public async Task<ActionResult<ChefsAllViewModel>> Index(string Id)
        {

            var chefs = new ChefsAllViewModel
            {
                Chefs = await this.chefs.GetAll()
            };

            return this.View(chefs);
        }

        [HttpGet]
        [Route("Chefs/{id:int}")]
        public async Task<ActionResult<ChefOutputModel>> BasicDetails(int id)
        {
            var chef = await this.chefs.GetDetails(id);
            return this.View(chef);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userId = this.currentUser.UserId;
            var isChef = await this.chefs.IsChef(userId);
            if (isChef == false)
            {
                return RedirectToAction("Create");
            }
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
            var userId = this.currentUser.UserId;
            var isChef = await this.chefs.IsChef(userId);
            if (isChef ==true)
            {
                return RedirectToAction("Details");
            }
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(ChefInputModel input)
        {
            var userId = this.currentUser.UserId;
            await this.chefs.CreateChef(input, userId);
            var chef = await this.chefs.GetDetailsByUserId(userId);
            await this.publisher.Publish(new ForumUserCreatedMessage
            {
                UserId = userId,
                UserName = chef.Name
            });
            return this.RedirectToAction("Details", chef.Id);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Edit()
        {
            var chef = await this.chefs.FindByUser(this.currentUser.UserId);
            var chenInput = new ChefInputModel
            {
                Name = chef.Name,
                Qualification = chef.Qualification,
                Biography = chef.Biography
            };
            return this.View(chenInput);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ChefInputModel input)
        {
            var chef = await this.chefs.FindByUser(this.currentUser.UserId);

            if (chef == null)
            {
                return BadRequest(Result.Failure("You cannot edit this chef."));
            }

            // eventualno da iznesa tazi logika v service
            chef.Name = input.Name;
            chef.Qualification = input.Qualification;
            chef.Biography = input.Biography;

            await this.chefs.Save(chef);

            return RedirectToAction("Details", new { id = chef.Id});
        }
    }
}
