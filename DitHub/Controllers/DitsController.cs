using DitHub.Data;
using DitHub.Models;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DitsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;

        public DitsController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Create()
        {
            var viewmodel = new CreateViewModel()
            {
                Genres = dbContext.Genres.ToList()
            };
            return View(viewmodel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = dbContext.Genres.ToList();
                return View("Create", viewModel);
            }
            var dit = new Dit()
            {
                AppUserId = userManager.GetUserId(User),
                Date = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            dbContext.Add(dit);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
