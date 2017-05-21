using AjVan.DAL.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjVan.Controllers
{
    public class KorisnikController : Controller
    {
        private AjVanContext db = new AjVanContext();

        public ActionResult ShowProfile(string player)
        {
            System.Diagnostics.Debug.WriteLine(player);
            var korisnik = db.Korisnici.Where(k => k.UserName.Equals(player)).First();

            return View(korisnik);
        }
    }
}