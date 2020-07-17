namespace RecipesRemixed.Recipes.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RecipesRemixed.Recipes.Data.Models;
    using RecipesRemixed.Recipes.Models.Identity;
    using RecipesRemixed.Recipes.Services.Identity;

    public class HomeController : Controller
    {

        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public HomeController(
                    IIdentityService identityService,
                    IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Privacy()
        {

            return this.View();
        }

        public async Task<IActionResult> Login()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInputModel user)
        {
            await this.identityService.Register(user);
            return this.RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
