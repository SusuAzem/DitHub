using DitHub.Data;
using DitHub.Models;
using DitHub.Repositories;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DitsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly UnitOfWork unit;

        public DitsController(ApplicationDbContext dbContext, UserManager<AppUser> userManager, UnitOfWork unit)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.unit = unit;
        }

        [Authorize]
        public IActionResult ArtistDits()
        {
            var list = unit.Dits.GetDitWithGenra(userManager.GetUserId(User));
            ViewData["Title"] = "My Dittes";
            return View(list);
        }

        [Authorize]
        public IActionResult ListFaveDit()
        {
            // FaveDits lookup is null because this action is for the user fave dits
            var id = userManager.GetUserId(User);
            var model = new ListDitViewModel()
            {
                Dits = unit.Dits.GetUserFave(id).ToList(),
                Title = "My Fave Dittes",
                SearchTerm = string.Empty,
                FaveDits = null,
                FolloweeL = unit.Followings.GetUserFollowee(id).ToLookup(f => f.FolloweeId),
            };
            return View("ListDit", model);

        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new CreateViewModel(unit.Genres.GetGenres());
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
                viewModel.Genres = unit.Genres.GetGenres();
                return View("DitForm", viewModel);
            }
            var dit = new Dit(userManager.GetUserId(User), viewModel);

            unit.Dits.Add(dit);
            unit.Complete();
            return RedirectToAction("ArtistDits");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // check the user id before editing the dit
            var user = userManager.GetUserId(User);
            var dit = unit.Dits.GetDit(id);
            if (dit == null)
            {
                return NotFound();
            }
            if (dit.AppUserId != user)
            {
                return new UnauthorizedResult();
            }
            var viewmodel = new CreateViewModel(unit.Genres.GetGenres())
            {
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
                viewModel.Genres = unit.Genres.GetGenres();
                return View("DitForm", viewModel);
            }
            var dit = unit.Dits.GetDitWithFaves(viewModel.Id);
            if (dit == null)
            {
                return NotFound();
            }
            if (dit.AppUserId != userManager.GetUserId(User))
            {
                return new UnauthorizedResult();
            }
            dit!.Update(viewModel);
            unit.Complete();
            return RedirectToAction("ArtistDits");
        }

        [HttpPost]
        public IActionResult Search(ListDitViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public IActionResult Details(int id)
        {
            var dit = unit.Dits.GetDit(id);
            if (dit == null)
            {
                return NotFound();
            }
            var details = new DetailsViewModel()
            {
                Dit = dit!,
            };
            var userId = userManager.GetUserId(User);
            if (User.Identity!.IsAuthenticated)
            {
                details.Following = unit.Followings.IsInFaveA(dit.AppUserId, userId);
                details.Infave = unit.Favedits.IsInFaveD(dit.Id, userId);
            }
            return View(details);
        }
    }
}
