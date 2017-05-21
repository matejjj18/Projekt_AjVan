using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AjVan.Models
{
    public class Korisnik : IdentityUser
    {
        public string KontaktBroj { get; set; }
        public bool IsSystemAdmin { get; set; }

        public ICollection<Soba> MojeSobe { get; set; }
        public ICollection<Soba> Sobe { get; set; }

        public virtual ICollection<Komentar> Komentari { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Korisnik> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("isSystemAdmin", Convert.ToString(this.IsSystemAdmin)));
            return userIdentity;
        }
    
    }
}