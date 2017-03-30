using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TNW.Models;

namespace TNW.Controllers
{
    public class AssetTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AssetType
        public ActionResult Index()
        {
            return View(db.AssetTypes.ToList());
        }

        // GET: AssetType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetType assetType = db.AssetTypes.Find(id);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            return View(assetType);
        }

        [Authorize]
        // GET: AssetType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] AssetType assetType)
        {
            if (ModelState.IsValid)
            {
                db.AssetTypes.Add(assetType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assetType);
        }

        // GET: AssetType/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetType assetType = db.AssetTypes.Find(id);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            return View(assetType);
        }

        // POST: AssetType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] AssetType assetType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetType);
        }

        // GET: AssetType/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetType assetType = db.AssetTypes.Find(id);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            return View(assetType);
        }

        // POST: AssetType/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetType assetType = db.AssetTypes.Find(id);
            db.AssetTypes.Remove(assetType);
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
