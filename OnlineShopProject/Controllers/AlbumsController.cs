﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Routing;
using OnlineShopProject.Controllers;
using OnlineShopProject.Filters;
using PagedList;

namespace OnlineShopProject.Models
{
    public class AlbumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Albums
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var albumModels = db.AlbumModels.Include(a => a.Artist).Include(a => a.Genre).OrderByDescending(x => x.ReleaseDate);

            SetCarModelId();

            ViewBag.user = GetCurrentUser();

            ViewBag.ActionName = "Index";

            return View(albumModels.ToPagedList(page, pageSize));
        }

        private ApplicationUser GetCurrentUser()
        {
            if (Request.IsAuthenticated)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser =
                    db.Users.Where(x => x.Id == currentUserId).Include(x => x.CurrencyModel).SingleOrDefault();
                return currentUser;
            }

            return null;
        }

        [RejectUnauthorizedUsers]
        public ActionResult AdminIndex(string SearchPattern)
        {
            var albumModels = db.AlbumModels.Include(a => a.Artist).Include(a => a.Genre);

            if (SearchPattern != null)
            {
                albumModels = albumModels.Where(x => x.Name.Contains(SearchPattern) || x.Genre.Name.Contains(SearchPattern) || x.Artist.Name.Contains(SearchPattern));
                ViewBag.SearchPattern = SearchPattern;
            }

            return View(albumModels.ToList());
        }

        public ActionResult Filter(int? genre, int? artist, int? decade, double? price, int page = 1, int pageSize = 6)
        {
            IQueryable<AlbumModel> filterQuery = db.AlbumModels;

            if (decade != null)
            {
                int intDecate = (int)decade;
                ViewBag.decade = intDecate;
                DateTime bottomDateTime = new DateTime(intDecate, 1, 1);
                DateTime higherDateTime = new DateTime(intDecate + 10, 1, 1);

                filterQuery = filterQuery.Where(a => a.ReleaseDate >= bottomDateTime &&
                                                        a.ReleaseDate < higherDateTime);
            }

            if (genre != null)
            {
                int inGenreId = (int)genre;
                ViewBag.genre = inGenreId;
                filterQuery = filterQuery.Where(g => g.GenreId == inGenreId);
            }

            if (artist != null)
            {
                int intArtistId = (int)artist;
                ViewBag.artist = intArtistId;
                filterQuery = filterQuery.Where(a => a.ArtistId == intArtistId);
            }

            if (price != null)
            {
                double doublePrice = (double)price;
                ViewBag.price = doublePrice;

                if (price == 20)
                {
                    filterQuery = filterQuery.Where(p => p.Price >= doublePrice);
                }
                else
                {
                    filterQuery = filterQuery.Where(p => p.Price >= doublePrice && p.Price <= doublePrice + 5);
                }
            }

            SetCarModelId();
            ViewBag.user = GetCurrentUser();
            ViewBag.ActionName = "Filter";

            return View(filterQuery.OrderByDescending(x => x.ReleaseDate).ToPagedList(page, pageSize));
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
            ViewBag.user = GetCurrentUser();

            if (albumModel == null)
            {
                return HttpNotFound();
            }
            return View(albumModel);
        }

        // GET: Albums/Create
        [RejectUnauthorizedUsers]
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
        [RejectUnauthorizedUsers]
        public ActionResult Create([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price")] AlbumModel albumModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string path = Server.MapPath("~//Uploads/Albums/CD_Covers");
                    string fullPath = Path.Combine(path, image.FileName);
                    image.SaveAs(fullPath);
                    albumModel.ImagePath = "/Uploads/Albums/CD_Covers/" + image.FileName;
                }

                db.AlbumModels.Add(albumModel);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name", albumModel.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name", albumModel.GenreId);
            return View(albumModel);
        }

        // GET: Albums/Edit/5
        [RejectUnauthorizedUsers]
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
        [RejectUnauthorizedUsers]
        public ActionResult Edit([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price,ImagePath")] AlbumModel albumModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumModel).State = EntityState.Modified;

                if (image != null && image.ContentLength > 0)
                {
                    string path = Server.MapPath("~//Uploads/Albums/CD_Covers");
                    string fullPath = Path.Combine(path, image.FileName);
                    image.SaveAs(fullPath);
                    albumModel.ImagePath = "/Uploads/Albums/CD_Covers/" + image.FileName;
                }

                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name", albumModel.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name", albumModel.GenreId);
            return View(albumModel);
        }

        // GET: Albums/Delete/5
        [RejectUnauthorizedUsers]
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
        [RejectUnauthorizedUsers]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumModel albumModel = db.AlbumModels.Find(id);
            db.AlbumModels.Remove(albumModel);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
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
