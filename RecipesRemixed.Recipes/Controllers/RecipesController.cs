namespace RecipesRemixed.Recipes.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Controllers;
    using RecipesRemixed.Recipes.Models.Recipes;
    using RecipesRemixed.Recipes.Services.Chefs;
    using RecipesRemixed.Recipes.Services.Recipes;
    using RecipesRemixed.Services;
    using RecipesRemixed.Services.Identity;

    public class RecipesController : ApiController
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
        public async Task<ActionResult<RecipesSearchOutputModel>> Search(
            [FromQuery] RecipesQuery query)
        {
            var carAdListings = await this.recipes.GetListings(query);

            var totalPages = await this.recipes.Total(query);

            return new RecipesSearchOutputModel(carAdListings, query.Page, totalPages);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<RecipeOutputModel>> Details(int id)
            => await this.recipes.(id);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecipeOutputModel>> Create(RecipesInputModel input)
        {
            var dealer = await this.chefs.FindByUser(this.currentUser.UserId);


        }

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id, CarAdInputModel input)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            var category = await this.categories.Find(input.Category);

            var manufacturer = await this.manufacturers.FindByName(input.Manufacturer);

            manufacturer ??= new Manufacturer
            {
                Name = input.Manufacturer
            };

            var carAd = await this.carAds.Find(id);

            carAd.Manufacturer = manufacturer;
            carAd.Model = input.Model;
            carAd.Category = category;
            carAd.ImageUrl = input.ImageUrl;
            carAd.PricePerDay = input.PricePerDay;
            carAd.Options = new Options
            {
                HasClimateControl = input.HasClimateControl,
                NumberOfSeats = input.NumberOfSeats,
                TransmissionType = input.TransmissionType
            };

            await this.carAds.Save(carAd);

            return Result.Success;
        }

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            return await this.carAds.Delete(id);
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine(
            [FromQuery] CarAdsQuery query)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var carAdListings = await this.carAds.Mine(dealerId, query);

            var totalPages = await this.carAds.Total(query);

            return new MineCarAdsOutputModel(carAdListings, query.Page, totalPages);
        }

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<IEnumerable<CategoryOutputModel>> Categories()
            => await this.categories.GetAll();

        [HttpPut]
        [Authorize]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        public async Task<ActionResult> ChangeAvailability(int id)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            var carAd = await this.carAds.Find(id);

            carAd.IsAvailable = !carAd.IsAvailable;

            await this.carAds.Save(carAd);

            return Result.Success;
        }
    }
}
