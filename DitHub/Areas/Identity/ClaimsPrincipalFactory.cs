using DitHub.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DitHub.Areas.Identity
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        public ClaimsPrincipalFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var v = (DateTime.Now.Year -  user.DateOfBirth.Year > 18) ? "true" : "false";
            ((ClaimsIdentity)principal.Identity!).AddClaims(new[] {
                new Claim( "Above18", v)
            });
            return principal;
        }
    }
}
