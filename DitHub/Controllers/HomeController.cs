﻿using DitHub.Data;
using DitHub.Models;
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

        public IActionResult Index()
        {
            var UpcomingDits = dbContext.Dits
                .Include(d => d.AppUser)
                .Include(d => d.Genre)
                .Where(d => d.Date > DateTime.Parse("1/1/2021"));

            ViewData["Title"] = "Home Dittes";
            ViewData["Heading"] = "All Dittes";

            return View("ListDit", UpcomingDits);
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
