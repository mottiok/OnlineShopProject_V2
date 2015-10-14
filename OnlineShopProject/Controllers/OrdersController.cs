﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShopProject.Models;
using Microsoft.AspNet.Identity;

namespace OnlineShopProject.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var orderModels = db.OrderModels.Include(x => x.ApplicationUser).
                Where(x => x.ApplicationUser.Id == userId).
                OrderByDescending(x => x.CreatedAt).
                Include(o => o.BillingDetails).
                Include(x => x.OrderItems.
                    Select(a => a.Album).
                    Select(z => z.Artist));

            return View(orderModels.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = db.OrderModels.Find(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
                CartModel cartModel = db.CartModels.Where(x => x.Id == currentUser.CartModelId).Include(x => x.CartItems.Select(a => a.Album).Select(z => z.Artist)).SingleOrDefault();

                ViewBag.CartModel = cartModel;
                ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country");
                ViewBag.BillingDetailsId = new SelectList(db.BillingDetailsModels, "Id", "FirstName"); // TODO: REMOVE UNUSED STUFF LIKE THIS

                return View();
            }

            return RedirectToAction("Index", "Albums");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string FirstName, string LastName, string Address, string ZipCode, int CountryId, string Phone, string CreditCardNumber, int ExpirationMonth, int ExpirationYear, int CVV2, int CartModelId)
        {
            BillingDetailsModel billingDetailsModel = new BillingDetailsModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                ZipCode = ZipCode,
                CountryId = CountryId,
                Phone = Phone,
                CreditCardNumber = CreditCardNumber,
                ExpirationMonth = ExpirationMonth,
                ExpirationYear = ExpirationYear,
                CVV2 = CVV2
            };

            db.BillingDetailsModels.Add(billingDetailsModel);

            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            OrderModel orderModel = new OrderModel()
            {
                BillingDetailsId = billingDetailsModel.Id,
                ApplicationUser = currentUser,
                CreatedAt = DateTime.Now
            };

            db.OrderModels.Add(orderModel);

            CartModel cartModel = db.CartModels.Where(x => x.Id == CartModelId).Include(x => x.CartItems.Select(a => a.Album)).SingleOrDefault();

            foreach (CartItemModel item in cartModel.CartItems)
            {
                db.OrderItemModels.Add(new OrderItemModel()
                {
                    AlbumId = item.AlbumId,
                    OrderModelId = orderModel.Id,
                    Price = item.Album.Price,
                    Quantity = item.Quantity
                });
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = db.OrderModels.Find(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillingDetailsId = new SelectList(db.BillingDetailsModels, "Id", "FirstName", orderModel.BillingDetailsId);
            return View(orderModel);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BillingDetailsId")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillingDetailsId = new SelectList(db.BillingDetailsModels, "Id", "FirstName", orderModel.BillingDetailsId);
            return View(orderModel);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderModel orderModel = db.OrderModels.Find(id);
            if (orderModel == null)
            {
                return HttpNotFound();
            }
            return View(orderModel);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderModel orderModel = db.OrderModels.Find(id);
            db.OrderModels.Remove(orderModel);
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
