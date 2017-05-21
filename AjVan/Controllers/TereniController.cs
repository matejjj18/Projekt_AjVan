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

namespace AjVan.Controllers
{
    public class TereniController : Controller
    {
        private AjVanContext db = new AjVanContext();

        // GET: Tereni
        public ActionResult Index()
        {
            var tereni = db.Tereni.Include(t => t.Kvart);
            return View(tereni.ToList());
        }

        // GET: Tereni/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teren teren = db.Tereni.Find(id);
            if (teren == null)
            {
                return HttpNotFound();
            }
            return View(teren);
        }

        // GET: Tereni/Create
        public ActionResult Create()
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            ViewBag.KvartId = new SelectList(db.Kvartovi, "Id", "Naziv");
            return View();
        }

        // POST: Tereni/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Opis,VrstaTerena,Cijena,KvartId,GeoSirina,GeoDuzina")] Teren teren)
        {
            if (ModelState.IsValid)
            {
                db.Tereni.Add(teren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KvartId = new SelectList(db.Kvartovi, "Id", "Naziv", teren.KvartId);
            return View(teren);
        }

        // GET: Tereni/Edit/5
        public ActionResult Edit(long? id)
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teren teren = db.Tereni.Find(id);
            if (teren == null)
            {
                return HttpNotFound();
            }
            ViewBag.KvartId = new SelectList(db.Kvartovi, "Id", "Naziv", teren.KvartId);
            return View(teren);
        }

        // POST: Tereni/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,Opis,VrstaTerena,Cijena,KvartId,GeoSirina,GeoDuzina")] Teren teren)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teren).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KvartId = new SelectList(db.Kvartovi, "Id", "Naziv", teren.KvartId);
            return View(teren);
        }

        // GET: Tereni/Delete/5
        public ActionResult Delete(long? id)
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teren teren = db.Tereni.Find(id);
            if (teren == null)
            {
                return HttpNotFound();
            }
            return View(teren);
        }

        // POST: Tereni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Teren teren = db.Tereni.Find(id);
            db.Tereni.Remove(teren);
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
