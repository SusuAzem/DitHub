using DitHub.Data;
using DitHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DitHub.Controllers
{
    public class DitsController : Controller
    {
        private ApplicationDbContext dbContext;

        public DitsController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Create()
        {
            //List<Genre> Glist;
            var viewmodel = new CreateViewModel()
            {
                Genres = dbContext.Genres.ToList<Genre>()
            };
            return View(viewmodel);
        }
    }
}
