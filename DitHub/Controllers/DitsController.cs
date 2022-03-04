using DitHub.Data;
using DitHub.Models;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult ListFaveDit()
        {
            var userId = userManager.GetUserId(User);

            //var L = dbContext.FaveDits
            //        .Where(u => u.AppUserId == userId)
            //        .Include(f => f.Dit)
            //        .ThenInclude(f => f.AppUser)
            //        .Include(d => d.Dit)
            //        .ThenInclude(f => f.Genre)
            //        .ToList();

            var list = dbContext.Dits
                .Where(d => d.FaveDits!.Where(f => f.AppUserId == userId).Any())
                .Include(d => d.AppUser)
                .Include(d => d.Genre)
                .ToList();

            ViewData["Title"] = "My Fave Dittes";
            ViewData["Heading"] = "My Fave Dittes";
            return View("ListDit", list);

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
