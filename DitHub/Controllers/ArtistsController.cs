using DitHub.Data;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationDbContext dbContext;

        public ArtistsController(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        [Authorize]
        public IActionResult ListFaveArtist()
        {
            var list = dbContext.Users
                        .Where(u => u.Followers!.Where(f => f.FollowerId == userManager.GetUserId(User)).Any())
                        .ToList();
            ViewData["Title"] = "My Fave Artist";
            ViewData["Heading"] = "My Fave Artist";

            return View(list);
        }
    }
}
