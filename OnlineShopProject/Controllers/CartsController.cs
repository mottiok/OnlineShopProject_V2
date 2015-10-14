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
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carts
        public ActionResult Index()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            return View(db.CartModels.Where(x => x.Id == currentUser.CartModelId).Include(x => x.CartItems.Select(a => a.Album).Select(z => z.Artist)).SingleOrDefault());
        }

        public ActionResult UpdateQuantity(int cartItemId, int changeVector)
        {
            CartItemModel cartItemModel = db.CartItemModels.Find(cartItemId);

            if (!((cartItemModel.Quantity + changeVector) < 0))
            {
                cartItemModel.Quantity = cartItemModel.Quantity + changeVector;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult UpdateQuantityValue(int cartItemId, int newQuantity)
        {
            CartItemModel cartItemModel = db.CartItemModels.Find(cartItemId);
            cartItemModel.Quantity = newQuantity;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartModel cartModel = db.CartModels.Find(id);
            if (cartModel == null)
            {
                return HttpNotFound();
            }
            return View(cartModel);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] CartModel cartModel)
        {
            if (ModelState.IsValid)
            {
                db.CartModels.Add(cartModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartModel);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartModel cartModel = db.CartModels.Find(id);
            if (cartModel == null)
            {
                return HttpNotFound();
            }
            return View(cartModel);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] CartModel cartModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartModel);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartModel cartModel = db.CartModels.Find(id);
            if (cartModel == null)
            {
                return HttpNotFound();
            }
            return View(cartModel);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartModel cartModel = db.CartModels.Find(id);
            db.CartModels.Remove(cartModel);
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
