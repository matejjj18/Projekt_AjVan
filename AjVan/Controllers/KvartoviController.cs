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
    public class KvartoviController : Controller
    {
        private AjVanContext db = new AjVanContext();

        // GET: Kvartovi
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            return View(db.Kvartovi.ToList());
        }

        // GET: Kvartovi/Details/5
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
            Kvart kvart = db.Kvartovi.Find(id);
            if (kvart == null)
            {
                return HttpNotFound();
            }
            return View(kvart);
        }

        // GET: Kvartovi/Create
        public ActionResult Create()
        {
            Korisnik korisnik = (Korisnik)db.Users.Find(User.Identity.GetUserId());
            if (korisnik == null || !korisnik.IsSystemAdmin)
            {
                return RedirectToAction("Index", "Sobas");
            }

            return View();
        }

        // POST: Kvartovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Kvart kvart)
        {
            if (ModelState.IsValid)
            {
                db.Kvartovi.Add(kvart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kvart);
        }

        // GET: Kvartovi/Edit/5
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
            Kvart kvart = db.Kvartovi.Find(id);
            if (kvart == null)
            {
                return HttpNotFound();
            }
            return View(kvart);
        }

        // POST: Kvartovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Kvart kvart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kvart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kvart);
        }

        // GET: Kvartovi/Delete/5
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
            Kvart kvart = db.Kvartovi.Find(id);
            if (kvart == null)
            {
                return HttpNotFound();
            }
            return View(kvart);
        }

        // POST: Kvartovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Kvart kvart = db.Kvartovi.Find(id);
            db.Kvartovi.Remove(kvart);
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
