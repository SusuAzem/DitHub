using DitHub.Core;
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
    public class FollowingsController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly UserManager<AppUser> userManager;

        public FollowingsController(IUnitOfWork unit, UserManager<AppUser> userManager)
        {
            this.unit = unit;
            this.userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult PostFollow([FromBody] FollowDTO dTO)
        {
            var userId = userManager.GetUserId(User);
            if (dTO.FeeId == userId)
            {
                return BadRequest(": you can't follow yourself");
            }
            if (unit.Followings.IsInFaveA(dTO.FeeId, userId))
            {
                return BadRequest(": you've already followed that artist");
            }
            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dTO.FeeId
            };

            unit.Followings.Add(following);
            unit.Complete();

            return Ok(JsonConvert.SerializeObject(following));
        }

        [HttpDelete]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public IActionResult Delete([FromBody] FollowDTO dTO)
        {
            string userId = userManager.GetUserId(User);
            var following = unit.Followings.GetFollowing(dTO.FeeId, userId);
            if (following != null)
            {
                unit.Followings.Remove(following);
                unit.Complete();
                return Ok(null);
            }

            if (following!.FollowerId != userId)
            {
                return new UnauthorizedResult();
            }
            else
            {
                return NotFound("Oops .. ");
            }
        }


    }
}
