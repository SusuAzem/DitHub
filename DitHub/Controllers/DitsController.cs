using DitHub.Data;
using DitHub.Models;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DitHub.Controllers
{
    public class DitsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public DitsController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
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
            var dit = new Dit()
            {
                ArtistId = userManager.GetUserId(User),
                Date = DateTime.ParseExact($"{viewModel.Date} {viewModel.Time}", "dd MMM yyyy HH:mm", null),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            dbContext.Add(dit);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
