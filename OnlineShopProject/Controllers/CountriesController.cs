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
    public class CountriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Countries
        public ActionResult Index()
        {
            return View(db.CountryModels.ToList());
        }

        [RejectUnauthorizedUsers]
        public ActionResult AdminIndex(string SearchPattern)
        {
            if (SearchPattern != null)
            {
                var countries = db.CountryModels.Where(x => x.Country.Contains(SearchPattern));
                ViewBag.SearchPattern = SearchPattern;
                return View(countries.ToList());
            }

            return View(db.CountryModels.ToList());
        }

        // GET: Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryModel countryModel = db.CountryModels.Find(id);
            if (countryModel == null)
            {
                return HttpNotFound();
            }
            return View(countryModel);
        }

        // GET: Countries/Create
        [RejectUnauthorizedUsers]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Create([Bind(Include = "Id,Country")] CountryModel countryModel)
        {
            if (ModelState.IsValid)
            {
                db.CountryModels.Add(countryModel);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            return View(countryModel);
        }

        // GET: Countries/Edit/5
        [RejectUnauthorizedUsers]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryModel countryModel = db.CountryModels.Find(id);
            if (countryModel == null)
            {
                return HttpNotFound();
            }
            return View(countryModel);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult Edit([Bind(Include = "Id,Country")] CountryModel countryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(countryModel);
        }

        // GET: Countries/Delete/5
        [RejectUnauthorizedUsers]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryModel countryModel = db.CountryModels.Find(id);
            if (countryModel == null)
            {
                return HttpNotFound();
            }
            return View(countryModel);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RejectUnauthorizedUsers]
        public ActionResult DeleteConfirmed(int id)
        {
            CountryModel countryModel = db.CountryModels.Find(id);
            db.CountryModels.Remove(countryModel);
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
