using DitHub.Data;
using DitHub.Models;
using DitHub.ViewModels;
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


        public HomeController(ApplicationDbContext dbContext)
        {
            //_logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index(string? query = null)
        {
            var UpcomingDits = dbContext.Dits
                .Include(d => d.AppUser)
                .Include(d => d.Genre)
                .Where(d => d.Date > DateTime.Parse("1/1/2021") && !d.RemoveFlag);

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
