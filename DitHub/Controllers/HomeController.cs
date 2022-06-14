using DitHub.Data;
using DitHub.Models;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace DitHub.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;

        public HomeController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            //_logger = logger;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public IActionResult Index(string? query = null)
        {
            var UpcomingDits = dbContext.Dits
                .Include(d => d.AppUser)
                .Include(d => d.Genre)
                .Where(d => d.Date > DateTime.Parse("1/1/2000"));

            //.Where(d => d.Date > DateTime.Parse("1/1/2021") && !d.RemoveFlag);

            if (!String.IsNullOrWhiteSpace(query))
            {
                UpcomingDits = UpcomingDits.Where(d =>
                d.AppUser.Name.Contains(query) ||
                d.Genre.Name.Contains(query) ||
                d.Venue.Contains(query));
            }

            var favedits = dbContext.FaveDits
                .Where(f => f.AppUserId == userManager.GetUserId(User))
                .ToList()
                .ToLookup(f => f.DitId);

            var followeeL = dbContext.Followings
                .Where(f => f.FollowerId == userManager.GetUserId(User))
                .ToList()
                .ToLookup(f => f.FolloweeId);
            var model = new ListDitViewModel()
            {
                Dits = UpcomingDits,
                Title = "Home Dittes",
                SearchTerm = query,
                FaveDits = favedits,
                FolloweeL =followeeL,
            };

            return View("ListDit", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
