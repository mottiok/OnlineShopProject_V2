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
    public class SongsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Songs
        public ActionResult Index()
        {
            var songModels = db.SongModels.Include(s => s.Album);
            return View(songModels.ToList());
        }

        public ActionResult AdminIndex(string SearchPattern)
        {
            var songModels = db.SongModels.Include(s => s.Album);

            if (SearchPattern != null)
            {
                songModels = songModels.Where(x => x.Name.Contains(SearchPattern) || x.Album.Name.Contains(SearchPattern));
                ViewBag.SearchPattern = SearchPattern;
            }

            return View(songModels.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongModel songModel = db.SongModels.Find(id);
            if (songModel == null)
            {
                return HttpNotFound();
            }
            return View(songModel);
        }

        // GET: Songs/Create
        [RejectUnauthorizedUsers]
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Create([Bind(Include = "Id,Name,AlbumId,Duration")] SongModel songModel)
        {
            if (ModelState.IsValid)
            {
                db.SongModels.Add(songModel);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", songModel.AlbumId);
            return View(songModel);
        }

        // GET: Songs/Edit/5
        [RejectUnauthorizedUsers]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongModel songModel = db.SongModels.Find(id);
            if (songModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", songModel.AlbumId);
            return View(songModel);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Edit([Bind(Include = "Id,Name,AlbumId,Duration")] SongModel songModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(songModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", songModel.AlbumId);
            return View(songModel);
        }

        // GET: Songs/Delete/5
        [RejectUnauthorizedUsers]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongModel songModel = db.SongModels.Find(id);
            if (songModel == null)
            {
                return HttpNotFound();
            }
            return View(songModel);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult DeleteConfirmed(int id)
        {
            SongModel songModel = db.SongModels.Find(id);
            db.SongModels.Remove(songModel);
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
