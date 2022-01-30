using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DitHub.Models
{
    public class AppUser : IdentityUser
    {
        private readonly IdentityUser user = null!;
        public virtual ICollection<Dit>? Dits { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
        public AppUser()
        {

        }
        public AppUser(IdentityUser user)
        {
            this.user = user;
        }
    }
}
