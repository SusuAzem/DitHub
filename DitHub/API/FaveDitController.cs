using DitHub.Data;
using DitHub.DTO;
using DitHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace DitHub.API
{
    //[Produces("application/json")]
    //[Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FaveDitController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<AppUser> userManager;

        public FaveDitController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        [HttpGet("")]
        public IActionResult Get([FromQuery] int ditid)
        {
            return Ok("number is " + ditid.ToString());
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        //[Consumes("application/json")]
        public IActionResult Post([FromBody] FDTO ditdto)
        {
            var userId = userManager.GetUserId(User);
            if (dbContext.FaveDits.Any(f => f.AppUserId == userId && f.DitId == ditdto.Ditid))
            {
                return BadRequest(" you liked this ditty already");
            }

            var Fave = new FaveDit(userId, ditdto.Ditid);

            dbContext.FaveDits.Add(Fave);
            dbContext.SaveChanges();
            //return Ok();

            return Ok(JsonConvert.SerializeObject(Fave));
        }
    }
}
