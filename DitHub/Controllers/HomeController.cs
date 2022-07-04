using DitHub.Data;
using DitHub.Models;
using DitHub.Repositories;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DitHub.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly FollowingR followingR;
        private readonly DitR ditR;

        public HomeController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            //_logger = logger;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.ditR = new DitR(dbContext);
            this.followingR = new FollowingR(dbContext);
        }

        public IActionResult Index(string? query = null)
        {
            string Id = userManager.GetUserId(User);
            var UpcomingDits = ditR.GetUserFave();
            //.Where(d => d.Date > DateTime.Parse("1/1/2021") && !d.RemoveFlag);
            if (!String.IsNullOrWhiteSpace(query))
            {
                UpcomingDits = UpcomingDits.Where(d =>
                d.AppUser.Name.Contains(query) ||
                d.Genre.Name.Contains(query) ||
                d.Venue.Contains(query));
            }
            var model = new ListDitViewModel()
            {
                Dits = UpcomingDits,
                Title = "Home Dittes",
                SearchTerm = query,
                FaveDits = UserFaveD(Id).ToLookup(f => f.DitId),
                FolloweeL = followingR.GetUserFollowee(Id).ToLookup(f => f.FolloweeId),
            };
            return View("ListDit", model);
        }
        private List<FaveDit> UserFaveD(string user)
        {
            return dbContext.FaveDits
                            .Where(f => f.AppUserId == user)
                            .ToList();
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
