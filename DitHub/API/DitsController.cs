using DitHub.Data;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DitsController : ControllerBase
    {
        private readonly ApplicationDbContext dbConxet;

        private readonly UserManager<AppUser> userManager;

        public DitsController(ApplicationDbContext dbConxet, UserManager<AppUser> userManager)
        {
            this.dbConxet = dbConxet;
            this.userManager = userManager;
        }

        [IgnoreAntiforgeryToken]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = userManager.GetUserId(User);
            var dit = dbConxet.Dits
                //.Include(d=>d.FaveDits!.Where(f=>f.DitId==id).Select(f => f.AppUser))
                .Include(d => d.FaveDits)
                .FirstOrDefault(d => d.Id == id && d.AppUserId == user);

            if (dit!.RemoveFlag)
            {
                return NotFound();
            }

            dit!.Remove();

            dbConxet.SaveChanges();
            return Ok();
        }
    }
}
