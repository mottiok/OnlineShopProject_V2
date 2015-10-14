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
    public class CartItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CartItems
        public ActionResult Index()
        {
            var cartItemModels = db.CartItemModels.Include(c => c.Album).Include(c => c.CartModel);
            return View(cartItemModels.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemModel cartItemModel = db.CartItemModels.Find(id);
            if (cartItemModel == null)
            {
                return HttpNotFound();
            }
            return View(cartItemModel);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name");
            ViewBag.CartModelId = new SelectList(db.CartModels, "Id", "Id");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AlbumId,Quantity")] CartItemModel cartItemModel)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.Where(x => x.Id == currentUserId).Include(x => x.CartModel).Include(x => x.CartModel.CartItems).SingleOrDefault();

                var possibleItems = currentUser.CartModel.CartItems.Where(x => x.AlbumId == cartItemModel.AlbumId);

                if (possibleItems.Any())
                {
                    possibleItems.SingleOrDefault().Quantity++;
                }
                else
                {
                    cartItemModel.CartModelId = currentUser.CartModelId;
                    db.CartItemModels.Add(cartItemModel);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Carts");
            }

            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", cartItemModel.AlbumId);
            ViewBag.CartModelId = new SelectList(db.CartModels, "Id", "Id", cartItemModel.CartModelId);
            return View(cartItemModel);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemModel cartItemModel = db.CartItemModels.Find(id);
            if (cartItemModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", cartItemModel.AlbumId);
            ViewBag.CartModelId = new SelectList(db.CartModels, "Id", "Id", cartItemModel.CartModelId);
            return View(cartItemModel);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AlbumId,Quantity,CartModelId")] CartItemModel cartItemModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItemModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.AlbumModels, "Id", "Name", cartItemModel.AlbumId);
            ViewBag.CartModelId = new SelectList(db.CartModels, "Id", "Id", cartItemModel.CartModelId);
            return View(cartItemModel);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItemModel cartItemModel = db.CartItemModels.Find(id);
            if (cartItemModel == null)
            {
                return HttpNotFound();
            }
            return View(cartItemModel);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItemModel cartItemModel = db.CartItemModels.Find(id);
            db.CartItemModels.Remove(cartItemModel);
            db.SaveChanges();
            return RedirectToAction("Index", "Carts");
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
