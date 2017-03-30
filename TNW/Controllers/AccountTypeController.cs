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
    [Authorize]
    public class AccountTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccountType
        public ActionResult Index()
        {
            var accountTypes = db.AccountTypes.Include(a => a.AssetType);
            return View(accountTypes.ToList());
        }

        // GET: AccountType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountType accountType = db
                .AccountTypes
                .Where(a => a.Id == id)
                .Include(a => a.AssetType)
                .FirstOrDefault();

            if (accountType == null)
            {
                return HttpNotFound();
            }
            return View(accountType);
        }

        // GET: AccountType/Create
        public ActionResult Create()
        {
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName");
            return View();
        }

        // POST: AccountType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AssetTypeId")] AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                db.AccountTypes.Add(accountType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", accountType.AssetTypeId);
            return View(accountType);
        }

        // GET: AccountType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountType accountType = db.AccountTypes.Find(id);
            if (accountType == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", accountType.AssetTypeId);
            return View(accountType);
        }

        // POST: AccountType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AssetTypeId")] AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", accountType.AssetTypeId);
            return View(accountType);
        }

        // GET: AccountType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountType = db
                .AccountTypes
                .Where(a => a.Id == id)
                .Include(a => a.AssetType)
                .FirstOrDefault();

            if (accountType == null)
            {
                return HttpNotFound();
            }
            return View(accountType);
        }

        // POST: AccountType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountType accountType = db.AccountTypes.Find(id);
            db.AccountTypes.Remove(accountType);
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
