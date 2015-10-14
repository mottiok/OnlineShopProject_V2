using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopProject.Models;

namespace OnlineShopProject.Controllers
{
    public class BillingDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BillingDetails
        public ActionResult Index()
        {
            var billingDetailsModels = db.BillingDetailsModels.Include(b => b.Country);
            return View(billingDetailsModels.ToList());
        }

        // GET: BillingDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingDetailsModel billingDetailsModel = db.BillingDetailsModels.Find(id);
            if (billingDetailsModel == null)
            {
                return HttpNotFound();
            }
            return View(billingDetailsModel);
        }

        // GET: BillingDetails/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country");
            return View();
        }

        // POST: BillingDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,ZipCode,CountryId,Phone,CreditCardNumber,ExpirationMonth,ExpirationYear,CVV2")] BillingDetailsModel billingDetailsModel)
        {
            if (ModelState.IsValid)
            {
                db.BillingDetailsModels.Add(billingDetailsModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country", billingDetailsModel.CountryId);
            return View(billingDetailsModel);
        }

        // GET: BillingDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingDetailsModel billingDetailsModel = db.BillingDetailsModels.Find(id);
            if (billingDetailsModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country", billingDetailsModel.CountryId);
            return View(billingDetailsModel);
        }

        // POST: BillingDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,ZipCode,CountryId,Phone,CreditCardNumber,ExpirationMonth,ExpirationYear,CVV2")] BillingDetailsModel billingDetailsModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billingDetailsModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country", billingDetailsModel.CountryId);
            return View(billingDetailsModel);
        }

        // GET: BillingDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingDetailsModel billingDetailsModel = db.BillingDetailsModels.Find(id);
            if (billingDetailsModel == null)
            {
                return HttpNotFound();
            }
            return View(billingDetailsModel);
        }

        // POST: BillingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillingDetailsModel billingDetailsModel = db.BillingDetailsModels.Find(id);
            db.BillingDetailsModels.Remove(billingDetailsModel);
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
