using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DitHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntiForgeryController : ControllerBase
    {
        private readonly IAntiforgery _antiForgery;
        public AntiForgeryController(IAntiforgery antiForgery)
        {
            _antiForgery = antiForgery;
        }

        [Route("api/antiforgery")]
        [IgnoreAntiforgeryToken]
        public IActionResult GenerateAntiForgeryTokens()
        {
            var tokens = _antiForgery.GetAndStoreTokens(HttpContext);
            // The Cookie token is the one stored in the cookie and the Request token is either sent in a hidden form
            // field __RequestVerificationToken or in a header value
            Response.Cookies.Append("XSRF-REQUEST-TOKEN", tokens.RequestToken!, new CookieOptions
            {
                HttpOnly = false
            });
            return NoContent();
        }

    }
}
