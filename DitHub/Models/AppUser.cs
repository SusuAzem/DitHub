using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DitHub.Models
{
    public class AppUser : IdentityUser
    {
        //private readonly IdentityUser user = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
        public virtual ICollection<Dit>? Dits { get; set; }
        public virtual ICollection<FaveDit>? FaveDits { get; set; }


        public AppUser()
        {

        }
    }
}
