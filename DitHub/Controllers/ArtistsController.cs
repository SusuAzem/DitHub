using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;

        public ArtistsController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            this.userManager = userManager;
            this.unit = unit;
        }
        [Authorize]
        public IActionResult ListFaveArtist()
        {
            var id = userManager.GetUserId(User);
            var list = unit.Users.GetUserFollowees(id).ToList();
            ViewData["Title"] = "My Fave Artist";
            ViewData["Heading"] = "My Fave Artist";
            return View(list);
        }
    }
}
