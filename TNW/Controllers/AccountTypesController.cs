using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.Controllers
{
    [Authorize]
    public class AccountTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccountTypes
        public ActionResult Index()
        {
            var accountTypes = db.AccountTypes.Include(a => a.Owner);
            return View(accountTypes.ToList());
        }

        // GET: AccountTypes/Details/5
        public ActionResult Details(int? id)
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
            return View(accountType);
        }

        // GET: AccountTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Comments")] AccountTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new AccountType
                {
                    Name = model.Name,
                    Comments = model.Comments,
                    OwnerId = User.Identity.GetUserId(),
                };
                db.AccountTypes.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AccountTypes/Edit/5
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
            return View(accountType);
        }

        // POST: AccountTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Comments")] AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountType);
        }

        // GET: AccountTypes/Delete/5
        public ActionResult Delete(int? id)
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
            return View(accountType);
        }

        // POST: AccountTypes/Delete/5
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
