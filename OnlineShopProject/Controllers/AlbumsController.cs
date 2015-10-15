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

            ViewBag.user = GetCurrentUser();

            return View(albumModels.ToList());
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
        public ActionResult AdminIndex()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

            var albumModels = db.AlbumModels.Include(a => a.Artist).Include(a => a.Genre);
            return View(albumModels.ToList());
        }

        public ActionResult Filter(int? genre, int? artist, int? decade, double? price)
        {
            IQueryable<AlbumModel> filterQuery = db.AlbumModels;

            if (decade != null)
            {
                int intDecate = (int)decade;
                DateTime bottomDateTime = new DateTime(intDecate, 1, 1);
                DateTime higherDateTime = new DateTime(intDecate + 10, 1, 1);

                filterQuery = filterQuery.Where(a => a.ReleaseDate >= bottomDateTime &&
                                                        a.ReleaseDate < higherDateTime);
            }

            if (genre != null)
            {
                int inGenreId = (int)genre;
                filterQuery = filterQuery.Where(g => g.GenreId == inGenreId);
            }

            if (artist != null)
            {
                int intArtistId = (int)artist;
                filterQuery = filterQuery.Where(a => a.ArtistId == intArtistId);
            }

            if (price != null)
            {
                double doublePrice = (double)price;
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

            return View(filterQuery);
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
        public ActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

            ViewBag.ArtistId = new SelectList(db.ArtistModels, "Id", "Name");
            ViewBag.GenreId = new SelectList(db.GenreModels, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price")] AlbumModel albumModel, HttpPostedFileBase image)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

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
        public ActionResult Edit(int? id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

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
        public ActionResult Edit([Bind(Include = "Id,ArtistId,ReleaseDate,GenreId,Name,Price,ImagePath")] AlbumModel albumModel, HttpPostedFileBase image)
        {
            if (!IsAdmin())
        {
                return RedirectToAction("Index", "Albums");
            }

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
        public ActionResult Delete(int? id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

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
            if (!IsAdmin())
            {
                return RedirectToAction("Index", "Albums");
            }

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

        public bool IsAdmin()
        {
            return true; // TODO: NEED FIXING
            //return User.IsInRole("Admins");
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
