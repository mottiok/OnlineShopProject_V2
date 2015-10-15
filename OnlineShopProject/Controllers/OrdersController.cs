using System;
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
            
            ViewBag.user = GetCurrentUser();

            return View(orderModels.ToList());
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
                ViewBag.BillingDetailsId = new SelectList(db.BillingDetailsModels, "Id", "FirstName");

                ViewBag.user = GetCurrentUser();

                return View();
            }

            return RedirectToAction("Index", "Albums");
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,City,ZipCode,CountryId,Phone,CreditCardNumber,ExpirationMonth,ExpirationYear,CVV2")] BillingDetailsModel billingDetailsModel, int CartModelId)
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.BillingDetailsModels.Add(billingDetailsModel);

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

                db.CartItemModels.RemoveRange(cartModel.CartItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            CartModel userCartModel = db.CartModels.Where(x => x.Id == currentUser.CartModelId).Include(x => x.CartItems.Select(a => a.Album).Select(z => z.Artist)).SingleOrDefault();

            ViewBag.CartModel = userCartModel;
            ViewBag.CountryId = new SelectList(db.CountryModels, "Id", "Country");
            ViewBag.BillingDetailsId = new SelectList(db.BillingDetailsModels, "Id", "FirstName");

            ViewBag.user = GetCurrentUser();

            return View();

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
