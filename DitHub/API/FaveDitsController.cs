using DitHub.Core.DTO;
using DitHub.Core.IRepositories;
using DitHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaveDitsController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public FaveDitsController(UserManager<AppUser> userManager, IUnitOfWork unit)
        {
            this.unit = unit;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult Post([FromBody] FDTO ditdto)
        {
            var userId = userManager.GetUserId(User);
            if (unit.Dits.GetDit(ditdto.Ditid) == null)
            {
                return NotFound();
            }
            if (unit.Favedits.IsInFaveD(ditdto.Ditid, userId))
            {
                return BadRequest(" you liked this ditty already");
            }
            var Fave = new FaveDit(userId, ditdto.Ditid);
            unit.Favedits.AddFavedit(Fave);
            unit.Complete();

            return Ok(JsonConvert.SerializeObject(Fave));
        }

        [HttpDelete]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete([FromBody] FDTO ditdto)
        {
            var userId = userManager.GetUserId(User);
            var favedit = unit.Favedits.GetFaveDit(ditdto.Ditid, userId);
            if (favedit != null)
            {
                unit.Favedits.Remove(favedit);
                unit.Complete();
                //return Ok();
                return Ok(null);
            }
            if (favedit!.AppUserId != userId)
            {
                return new UnauthorizedResult();
            }
            else
            {
                return NotFound("Oops ..");
            }
        }
    }
}
