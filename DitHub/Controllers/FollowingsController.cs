using DitHub.Data;
using DitHub.DTO;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace DitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<AppUser> userManager;

        public FollowingsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
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
            if (context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dTO.FeeId))
            {
                return BadRequest(": you've already followed that artist");
            }

            var folloing = new Following()
            {
                FollowerId = userId,
                FolloweeId = dTO.FeeId
            };

            context.Followings.Add(folloing);
            context.SaveChanges();

            return Ok(JsonConvert.SerializeObject(folloing, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            }));
        }
    }
}
