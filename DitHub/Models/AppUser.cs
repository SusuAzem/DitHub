using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DitHub.Models
{
    public class AppUser : IdentityUser
    {
        private readonly IdentityUser user = null!;
        public virtual ICollection<Dit>? Dits { get; set; }

        public AppUser()
        {

        }
        public AppUser(IdentityUser user)
        {
            this.user = user;
        }
    }
}
