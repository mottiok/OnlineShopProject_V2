using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopProject.Models;
using OnlineShopProject.Filters;

namespace OnlineShopProject
{
    public class ArtistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artists
        public ActionResult Index()
        {
            return PartialView(db.ArtistModels.ToList());
        }

        [RejectUnauthorizedUsers]
        public ActionResult AdminIndex()
        {
            return View(db.ArtistModels.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistModel artistModel = db.ArtistModels.Find(id);
            if (artistModel == null)
            {
                return HttpNotFound();
            }
            return View(artistModel);
        }

        // GET: Artists/Create
        [RejectUnauthorizedUsers]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Create([Bind(Include = "Id,Name")] ArtistModel artistModel)
        {
            if (ModelState.IsValid)
            {
                db.ArtistModels.Add(artistModel);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            return View(artistModel);
        }

        // GET: Artists/Edit/5
        [RejectUnauthorizedUsers]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistModel artistModel = db.ArtistModels.Find(id);
            if (artistModel == null)
            {
                return HttpNotFound();
            }
            return View(artistModel);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Edit([Bind(Include = "Id,Name")] ArtistModel artistModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(artistModel);
        }

        // GET: Artists/Delete/5
        [RejectUnauthorizedUsers]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistModel artistModel = db.ArtistModels.Find(id);
            if (artistModel == null)
            {
                return HttpNotFound();
            }
            return View(artistModel);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistModel artistModel = db.ArtistModels.Find(id);
            db.ArtistModels.Remove(artistModel);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
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
