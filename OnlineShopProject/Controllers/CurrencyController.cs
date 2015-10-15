using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class CurrencyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Currency
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.user = GetCurrentUser();
            }
            return View(db.CurrencyModels.ToList());
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

        // GET: Currency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyModel currencyModel = db.CurrencyModels.Find(id);
            if (currencyModel == null)
            {
                return HttpNotFound();
            }
            return View(currencyModel);
        }

        // GET: Currency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Sign")] CurrencyModel currencyModel)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyModels.Add(currencyModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencyModel);
        }

        public ActionResult Update(int? id)
        {
            if (Request.IsAuthenticated)
            {
                ApplicationUser user = GetCurrentUser();
                user.CurrencyModelId = (int)id;
                db.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Currency/Edit/5
        public ActionResult Edit(int? id)
        {
            ApplicationUser user = GetCurrentUser();
            //db.Users.Where(x => x.Id == user.Id).Where(x => x.CurrencyModelId == id).
            user.CurrencyModelId = (int)id;

            db.SaveChanges();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //CurrencyModel currencyModel = db.CurrencyModels.Find(id);
            //if (currencyModel == null)
            //{
            //    return HttpNotFound();
            //}
            return null;
        }

        // POST: Currency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Sign")] CurrencyModel currencyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyModel);
        }

        // GET: Currency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyModel currencyModel = db.CurrencyModels.Find(id);
            if (currencyModel == null)
            {
                return HttpNotFound();
            }
            return View(currencyModel);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyModel currencyModel = db.CurrencyModels.Find(id);
            db.CurrencyModels.Remove(currencyModel);
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
