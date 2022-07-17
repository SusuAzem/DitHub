using DitHub.Core;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using DitHub.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace DitHub.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;

        public HomeController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            //_logger = logger;
            this.userManager = userManager;
            this.unit = unit;
        }

        public IActionResult Index(string? query = null)
        {
            string Id = userManager.GetUserId(User);
            var UpcomingDits = unit.Dits.GetDits();
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
                Dits = UpcomingDits.ToList(),
                Title = "Home Dittes",
                SearchTerm = query,
                FaveDits = Id == null ? null : unit.Favedits.UserFaveD(Id).ToLookup(f => f.DitId),
                FolloweeL = Id == null ? null : unit.Followings.GetUserFollowee(Id).ToLookup(f => f.FolloweeId),
            };
            return View("ListDit", model);
        }

        [HttpPost]
        public IActionResult Search(ListDitViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
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
