using DitHub.Core;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DitsController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unit;

        public DitsController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            this.userManager = userManager;
            this.unit = unit;
        }

        [IgnoreAntiforgeryToken]
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var user = userManager.GetUserId(User);
            var dit = unit.Dits.GetDitWithFaves(id, user);

            if (dit!.RemoveFlag || dit == null)
            {
                return NotFound();
            }
            if (dit.AppUserId != user)
            {
                return new UnauthorizedResult();
            }
            dit!.Remove();

            unit.Complete();
            return Ok();
        }
    }
}
