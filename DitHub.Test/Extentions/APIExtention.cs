using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace DitHub.Test.Extentions
{
    public static class APIExtention
    {
        public static void MockUser(this ControllerBase controller, string name, string id)
        {
            var identity = new GenericIdentity(name);
            identity.AddClaims(new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, id),
            });
            var principal = new GenericPrincipal(identity, null);
            var claimP = new ClaimsPrincipal(principal);

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimP
                }
            };
            controller.ControllerContext = context;
        }
    }
}
