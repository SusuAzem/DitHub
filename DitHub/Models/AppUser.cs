using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DitHub.Models
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Dit>? Dits { get; set; }
    }
}
