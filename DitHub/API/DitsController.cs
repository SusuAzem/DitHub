using DitHub.Data;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var dit = dbConxet.Dits.FirstOrDefault(d => d.Id == id && d.AppUserId == userManager.GetUserId(User));

            if (dit!.RemoveFlag)
            {
                return NotFound();
            }

            dit!.RemoveFlag = true;
            dbConxet.SaveChanges();
            return Ok();
        }
    }
}
