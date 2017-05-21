using AjVan.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AjVan.DAL.DbContexts
{
    public class AjVanContext : AAjVanContext
    {
        public AjVanContext() :base("DefaultConnection")
        {

        }

        public DbSet<Sport> Sportovi { get; set; }
        public DbSet<Kvart> Kvartovi { get; set; }
        public DbSet<Teren> Tereni { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Komentar> Komentari { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Rename tables
            modelBuilder.Entity<Sport>().ToTable("Sportovi");
            modelBuilder.Entity<Kvart>().ToTable("Kvartovi");
            modelBuilder.Entity<Teren>().ToTable("Tereni");
            modelBuilder.Entity<Soba>().ToTable("Sobe");
            modelBuilder.Entity<Komentar>().ToTable("Komentari");

            //modelBuilder.Entity<Soba>()
            //      .HasRequired(u => u.Admin)
            //      .WithMany(g => g.MojeSobe)
            //      .HasForeignKey(u => u.AdminId)
            //      .WillCascadeOnDelete(false);

            modelBuilder.Entity<Korisnik>()
                        .HasMany(u => u.Sobe)
                        .WithMany(g => g.Igraci)
                        .Map(m => m.MapLeftKey("KorisnikId")
                                   .MapRightKey("SobaId")
                                   .ToTable("KorisniciSobe"));
        }
        public static AjVanContext Create()
        {
            return new AjVanContext();
        }
    }
}