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
using AjVan.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace AjVan.Controllers
{
    public class KomentariController : Controller
    {
        private AjVanContext db = new AjVanContext();

        // GET: Komentari
        public ActionResult Index()
        {
            var komentari = db.Komentari.Include(k => k.Korisnik).Include(k => k.Soba);
            return View(komentari.ToList());
        }

        // GET: Komentari/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // GET: Komentari/Create
        public ActionResult Create()
        {
            ViewBag.KorisnikId = new SelectList(db.Users, "Id", "Email");
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Naziv");
            return View();
        }

        // POST: Komentari/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(long id, SobaKomentarViewModel sobaKomentar)
        {
            if (ModelState.IsValid)
            {
                sobaKomentar.NoviKomentar.Vrijeme = DateTime.Now;
                sobaKomentar.NoviKomentar.KorisnikId = User.Identity.GetUserId();
                sobaKomentar.NoviKomentar.Korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
                sobaKomentar.NoviKomentar.SobaId = id;
                db.Komentari.Add(sobaKomentar.NoviKomentar);
                db.SaveChanges();
                return RedirectToAction("Details", "Sobas", new { id });
            }

            ViewBag.KorisnikId = new SelectList(db.Users, "Id", "Email", sobaKomentar.NoviKomentar.KorisnikId);
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Naziv", sobaKomentar.NoviKomentar.SobaId);
            return View(sobaKomentar);
        }

        // GET: Komentari/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikId = new SelectList(db.Users, "Id", "Email", komentar.KorisnikId);
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Naziv", komentar.SobaId);
            return View(komentar);
        }

        // POST: Komentari/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sadrzaj,Vrijeme,KorisnikId,SobaId")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikId = new SelectList(db.Users, "Id", "Email", komentar.KorisnikId);
            ViewBag.SobaId = new SelectList(db.Sobe, "Id", "Naziv", komentar.SobaId);
            return View(komentar);
        }

        // GET: Komentari/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Komentar komentar = db.Komentari.Find(id);
            if (komentar == null)
            {
                return HttpNotFound();
            }
            return View(komentar);
        }

        // POST: Komentari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Komentar komentar = db.Komentari.Find(id);
            db.Komentari.Remove(komentar);
            db.SaveChanges();
            return RedirectToAction("Index");
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
