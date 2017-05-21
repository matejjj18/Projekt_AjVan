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
    public class SportoviController : Controller
    {
        private AjVanContext db = new AjVanContext();

        // GET: Sportovi
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            return View(db.Sportovi.ToList());
        }

        // GET: Sportovi/Details/5
        public ActionResult Details(long? id)
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
            Sport sport = db.Sportovi.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // GET: Sportovi/Create
        public ActionResult Create()
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            return View();
        }

        // POST: Sportovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Sportovi.Add(sport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sport);
        }

        // GET: Sportovi/Edit/5
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
            Sport sport = db.Sportovi.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // POST: Sportovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sport);
        }

        // GET: Sportovi/Delete/5
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
            Sport sport = db.Sportovi.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // POST: Sportovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sport sport = db.Sportovi.Find(id);
            db.Sportovi.Remove(sport);
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
