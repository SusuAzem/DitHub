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
        public IActionResult ArtistDits()
        {
            var list = dbContext.Dits
                .Where(d => d.AppUserId == userManager.GetUserId(User))
                .Include(d => d.Genre)
                .ToList();

            ViewData["Title"] = "My Dittes";
            ViewData["Heading"] = "My Dittes";
            return View(list);

        }

        [Authorize]
        public IActionResult ListFaveDit()
        {
            //var L = dbContext.FaveDits
            //        .Where(u => u.AppUserId == userId)
            //        .Include(f => f.Dit)
            //        .ThenInclude(f => f.AppUser)
            //        .Include(d => d.Dit)
            //        .ThenInclude(f => f.Genre)
            //        .ToList();

            var list = dbContext.Dits
                .Where(d => d.FaveDits!.Where(f => f.AppUserId == userManager.GetUserId(User)).Any())
                .Include(d => d.AppUser)
                .Include(d => d.Genre)
                .ToList();

            ViewData["Title"] = "My Fave Dittes";
            ViewData["Heading"] = "My Fave Dittes";
            return View("ListDit", list);

        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new CreateViewModel()
            {
                Genres = dbContext.Genres.ToList()
            };
            ViewData["Title"] = "Add a Ditte";
            ViewData["Action"] = nameof(Create);
            return View("DitForm", viewmodel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = dbContext.Genres.ToList();
                return View("DitForm", viewModel);
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
            return RedirectToAction("ArtistDits");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // check the user id before editing the dit
            var dit = dbContext.Dits.FirstOrDefault(d => d.Id == id && d.AppUserId == userManager.GetUserId(User));

            var viewmodel = new CreateViewModel()
            {
                Genres = dbContext.Genres.ToList(),
                Venue = dit!.Venue,
                Date = dit!.Date.ToString("dd MMM yyyy"),
                Time = dit!.Date.ToString("HH:mm"),
                Genre = dit!.GenreId
            };

            ViewData["Title"] = "Edit a Ditte";
            ViewData["Action"] = nameof(Edit);

            return View("DitForm", viewmodel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = dbContext.Genres.ToList();
                return View("DitForm", viewModel);
            }
            var dit = dbContext.Dits.FirstOrDefault(d => d.Id == viewModel.Id && d.AppUserId == userManager.GetUserId(User));

            dit!.Date = viewModel.GetDateTime();
            dit!.GenreId = viewModel.Genre;
            dit!.Venue = viewModel.Venue;

            dbContext.SaveChanges();
            return RedirectToAction("ArtistDits");
        }
    }
}
