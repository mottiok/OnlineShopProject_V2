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

namespace OnlineShopProject.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviewModels = db.ReviewModels.Include(r => r.AlbumModel);
            return View(reviewModels.ToList());
        }

        [RejectUnauthorizedUsers]
        public ActionResult AdminIndex()
        {
            var reviewModels = db.ReviewModels.Include(r => r.AlbumModel);
            return View(reviewModels.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = db.ReviewModels.Find(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.AlbumModelId = new SelectList(db.AlbumModels, "Id", "Name");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,AlbumModelId,Rating")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                reviewModel.CreatedAt = DateTime.Now;
                db.ReviewModels.Add(reviewModel);
                db.SaveChanges();
                return RedirectToAction("Details", "Albums", new { id = reviewModel.AlbumModelId });
            }

            ViewBag.AlbumModelId = new SelectList(db.AlbumModels, "Id", "Name", reviewModel.AlbumModelId);
            return View(reviewModel);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = db.ReviewModels.Find(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumModelId = new SelectList(db.AlbumModels, "Id", "Name", reviewModel.AlbumModelId);
            return View(reviewModel);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,AlbumModelId,Rating")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumModelId = new SelectList(db.AlbumModels, "Id", "Name", reviewModel.AlbumModelId);
            return View(reviewModel);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = db.ReviewModels.Find(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewModel reviewModel = db.ReviewModels.Find(id);
            db.ReviewModels.Remove(reviewModel);
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
