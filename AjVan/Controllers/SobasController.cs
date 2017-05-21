using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AjVan.DAL.DbContexts;
using AjVan.Models;
using Microsoft.AspNet.Identity;
using AjVan.Models.ViewModels;
using AjVan.ViewModels.Sobe;

namespace AjVan.Controllers
{
    public class SobasController : Controller
    {
        private AjVanContext db = new AjVanContext();

        // GET: Sobas
        public ActionResult Index()
        {
            // ovim sessionom se provjerava u indexu kada ima gumb kreiraj sobu, a kad ne
            Session["logged"] = User.Identity.IsAuthenticated;

            var korisnikId = User.Identity.GetUserId();
            Session["loggedUser"] = db.Korisnici.FirstOrDefault(k => k.Id == korisnikId);

            return View(this.GetIndexViewModel());
        }

        // GET: Sobas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Soba soba = db.Sobe.Include(s => s.Igraci).FirstOrDefault(s => s.Id == id.Value);
            if (soba == null)
            {
                return HttpNotFound();
            }

            List<Komentar> komentari = db.Komentari.Where(k => k.SobaId == id).ToList();

            List<Korisnik> igraci = soba.Igraci.ToList();

            SobaKomentarViewModel sobaKomentar = new SobaKomentarViewModel();

            sobaKomentar.Id = soba.Id;
            sobaKomentar.Naziv = soba.Naziv;
            sobaKomentar.Opis = soba.Opis;
            sobaKomentar.Pocetak = soba.Pocetak;
            sobaKomentar.Trajanje = soba.Trajanje;
            sobaKomentar.Admin = soba.Admin;
            sobaKomentar.Sport = soba.Sport;
            sobaKomentar.Teren = soba.Teren;
            sobaKomentar.Komentari = komentari;
            sobaKomentar.Igraci = igraci;

            var korisnikId = User.Identity.GetUserId();
            Session["loggedUser"] = db.Korisnici.FirstOrDefault(k => k.Id == korisnikId);

            // Check if user is room admin
            ViewBag.IsAdmin = soba.AdminId == korisnikId;
            return View(sobaKomentar);
        }

        
        [Authorize]
        public ActionResult JoinRoom(long? id)
        {

            var korisnikId = User.Identity.GetUserId();
            var soba = db.Sobe.Include(s=> s.Igraci).FirstOrDefault(s => s.Id == id.Value);
            var korisnik = db.Korisnici.FirstOrDefault(k => k.Id == korisnikId);

            if(soba.Igraci.Count == soba.MaksimalniBrojIgraca)
            {
                TempData[Constants.Message] = "Soba je puna!";
                TempData[Constants.ErrorOccurred] = false;
                return RedirectToAction("Index");
            }

            if (soba != null)
            {
                soba.Igraci.Add(korisnik);
                if (korisnik.Sobe == null)
                {
                    korisnik.Sobe = new List<Soba>();
                }

                korisnik.Sobe.Add(soba);
                TempData[Constants.Message] = "Pridružili ste se sobi " + soba.Naziv;
                TempData[Constants.ErrorOccurred] = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            //Redirect to soba details
            //return RedirectToAction("Details", new { id = id });
        }

        [Authorize]
        public ActionResult ExitRoom(long? id)
        {
            var korisnikId = User.Identity.GetUserId();
            var soba = db.Sobe.Include(s => s.Igraci).FirstOrDefault(s => s.Id == id.Value);
            var korisnik = db.Korisnici.FirstOrDefault(k => k.Id == korisnikId);

            if (korisnik.UserName.Equals(soba.Admin.UserName))
            {
                //soba se brise
                foreach(Korisnik k in soba.Igraci)
                {
                    k.Sobe.Remove(soba);
                }
                korisnik.MojeSobe.Remove(soba);
                db.Sobe.Remove(soba);
                db.SaveChanges();

                TempData[Constants.Message] = "Soba obrisana.";
                TempData[Constants.ErrorOccurred] = false;

                return RedirectToAction("Index");
            }

            if (soba != null)
            {
                soba.Igraci.Remove(korisnik);
                korisnik.Sobe.Remove(soba);
                TempData[Constants.Message] = "Izašli ste iz sobe " + soba.Naziv;
                TempData[Constants.ErrorOccurred] = false;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult KickPlayer(long? roomId, string player)
        {
            var soba = db.Sobe.Include(s => s.Igraci).FirstOrDefault(s => s.Id == roomId.Value);
            var korisnik = db.Korisnici.Where(k => k.UserName.Equals(player)).First();

            if (soba != null)
            {
                soba.Igraci.Remove(korisnik);
                korisnik.Sobe.Remove(soba);
                TempData[Constants.Message] = "Igrač " + korisnik.Email + " je izbačen iz sobe";
                TempData[Constants.ErrorOccurred] = false;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = roomId });
        }


        public ActionResult EditDescription(long? roomId, string opis)
        {
            var soba = db.Sobe.Include(s => s.Igraci).FirstOrDefault(s => s.Id == roomId.Value);
            if (soba.AdminId != User.Identity.GetUserId())
            {
                TempData[Constants.Message] = "Nemate pravo mijenjati opis sobe";
                TempData[Constants.ErrorOccurred] = true;
            }
            else
            {
                soba.Opis = opis;
                db.SaveChanges();
                TempData[Constants.Message] = "Opis sobe promijenjen";
                TempData[Constants.ErrorOccurred] = false;

            }

         

            return RedirectToAction("Details", new { id = roomId });
        }

        // GET: Sobas/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Users, "Id", "Email");
            ViewBag.SportId = new SelectList(db.Sportovi, "Id", "Naziv");
            ViewBag.Kvartovi = db.Kvartovi.Include(k => k.Tereni);
            return View();
        }

        // POST: Sobas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Opis,Pocetak,Trajanje,AdminId,SportId, TerenId, MaksimalniBrojIgraca")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                if(soba.Pocetak < DateTime.Now)
                {
                    TempData[Constants.Message] = "Početak mora biti u budućnosti!";
                    TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction("Create");
                }

                var adminId = User.Identity.GetUserId();
                var admin = db.Korisnici.Include(a => a.MojeSobe).Include(a => a.Sobe).FirstOrDefault(k => k.Id == adminId);
                soba.AdminId = admin.Id;
                // add admina u sobu
                soba.Igraci = new List<Korisnik>() { admin };
                admin.MojeSobe.Add(soba);
                admin.Sobe.Add(soba);

                db.Sobe.Add(soba);
                db.SaveChanges();
                TempData[Constants.Message] = "Soba " + soba.Naziv + " kreirana";
                TempData[Constants.ErrorOccurred] = false;
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Users, "Id", "Email", soba.AdminId);
            ViewBag.SportId = new SelectList(db.Sportovi, "Id", "Naziv", soba.SportId);
            return View(soba);
        }

        // GET: Sobas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Users, "Id", "Email", soba.AdminId);
            ViewBag.SportId = new SelectList(db.Sportovi, "Id", "Naziv", soba.SportId);
            return View(soba);
        }

        // POST: Sobas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,Pocetak,Trajanje,AdminId,SportId")] Soba soba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Users, "Id", "Email", soba.AdminId);
            ViewBag.SportId = new SelectList(db.Sportovi, "Id", "Naziv", soba.SportId);
            return View(soba);
        }

        // GET: Sobas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soba soba = db.Sobe.Find(id);
            if (soba == null)
            {
                return HttpNotFound();
            }
            return View(soba);
        }

        // POST: Sobas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Soba soba = db.Sobe.Find(id);
            db.Sobe.Remove(soba);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult FilterSobe(DateTime? TimeFrom, DateTime? TimeTo, long? SportId, long? TerenId, long? KvartId)
        {
            var sobe = db.Sobe.Include(s => s.Igraci).Include(s => s.Admin).Include(s => s.Teren);

            var sobeAll = db.Sobe.Include(s => s.Igraci).Include(s => s.Admin).Include(s => s.Teren);

            var d = DateTime.Now.AddDays(-1);

            sobe = sobe.Where(s => DbFunctions.AddMilliseconds(s.Pocetak, DbFunctions.DiffMilliseconds(TimeSpan.Zero, s.Trajanje)) > d);
            if (TimeFrom.HasValue)
                sobe = sobeAll.Where(s => s.Pocetak >= TimeFrom);
            if (TimeTo.HasValue)
                sobe = sobeAll.Where(s => s.Pocetak <= TimeTo);
            if (SportId.HasValue)
                sobe = sobe.Where(s => s.SportId  == SportId);
            if (TerenId.HasValue)
                sobe = sobe.Where(s => s.TerenId == TerenId);
            if (KvartId.HasValue)
                sobe = sobe.Where(s => s.Teren.KvartId == KvartId);

            var korisnikId = User.Identity.GetUserId();
            Session["loggedUser"] = db.Korisnici.FirstOrDefault(k => k.Id == korisnikId);

            return PartialView("_SobaTablePartial", sobe.ToList());
        }
        private IndexSobeViewModel GetIndexViewModel()
        {
            var sobe = db.Sobe.Include(s => s.Admin).Include(s => s.Sport).Include(s => s.Igraci).Include(s => s.Teren);
            var kvartovi = db.Kvartovi.Include(s => s.Tereni);
            var sportovi = db.Sportovi;

            var d = DateTime.Now.AddDays(-1);

            sobe = sobe.Where(s => DbFunctions.AddMilliseconds(s.Pocetak, DbFunctions.DiffMilliseconds(TimeSpan.Zero, s.Trajanje)) > d);

            return new IndexSobeViewModel()
            {
                Sobe = sobe,
                Kvartovi = kvartovi,
                Sportovi = sportovi
            };
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
