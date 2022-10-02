using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using DitHub.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DitsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;

        public DitsController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
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

        [Authorize(Policy= "Artist")]
        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new CreateViewModel(unit.Genres.GetGenres())
            {
                Title = "Add a Ditte",
                Action = nameof(Create)
            };
            return View("DitForm", viewmodel);
        }

        [Authorize(Policy= "Artist")]
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
                Genre = dit!.GenreId,
                Title = "Edit a Ditte",
                Action = nameof(Edit)
            };
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
            var userId = userManager.GetUserId(User);
            //var dit = unit.Dits.GetDitWithFaves(viewModel.Id, userId);
            var dit = unit.Dits.GetDitWithFaves(viewModel.Id);
            if (dit == null)
            {
                return NotFound();
            }
            if (dit.AppUserId != userId)
            {
                return new UnauthorizedResult();
            }
            dit!.Update(viewModel);
            unit.Complete();
            return RedirectToAction("ArtistDits");
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
            if (userId != null)
            {
                details.Following = unit.Followings.IsInFaveA(dit.AppUserId, userId);
                details.Infave = unit.Favedits.IsInFaveD(dit.Id, userId);
            }
            return View(details);
        }
    }
}
