using AjVan.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AjVan.DAL.DbContexts
{
    public abstract class AAjVanContext : IdentityDbContext<IdentityUser>
    {
        protected AAjVanContext()
			: base("DefaultConnection")
		{
        }

        protected AAjVanContext(string connectionString)
          : base(connectionString)
        {
        }
        public new DbSet<Korisnik> Korisnici { get; set; }
    }
}