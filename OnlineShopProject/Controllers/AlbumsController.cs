using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace OnlineShopProject.Models
{
    public class AlbumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Albums
        public ActionResult Index()
        {
            var albumModels = db.AlbumModels.Include(a => a.Artist).Include(a => a.Genre);

            SetCarModelId();

            return View(albumModels.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AlbumModel albumModel = db.AlbumModels.Where(x => x.Id == id).
                Include(x => x.Genre).
                Include(x => x.Songs).
                Include(x => x.Artist).
                Include(x => x.Reviews.Select(u => u.ApplicationUser)).SingleOrDefault();

            SetCarModelId();

            if (albumModel == null)
            {
                return HttpNotFound();
            }
            return View(albumModel);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name");
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price")] AlbumModel albumModel)
        {
            if (ModelState.IsValid)
            {
                db.AlbumModels.Add(albumModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name", albumModel.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name", albumModel.GenreId);
            return View(albumModel);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumModel albumModel = db.AlbumModels.Find(id);
            if (albumModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name", albumModel.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name", albumModel.GenreId);
            return View(albumModel);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price")] AlbumModel albumModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name", albumModel.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name", albumModel.GenreId);
            return View(albumModel);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumModel albumModel = db.AlbumModels.Find(id);
            if (albumModel == null)
            {
                return HttpNotFound();
            }
            return View(albumModel);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumModel albumModel = db.AlbumModels.Find(id);
            db.AlbumModels.Remove(albumModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetCarModelId()
        {
            if (Request.IsAuthenticated)
            {
                ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
                ViewBag.CartModelId = currentUser.CartModelId;
            }
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
