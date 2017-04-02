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
    public class CurrencyTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CurrencyTypes
        public ActionResult Index()
        {
            //TODO: show only current user's currency information
            return View(db.CurrencyTypes.ToList());
        }

        // GET: CurrencyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyTypes.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // GET: CurrencyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CurrencyName")] CurrencyTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currency = new CurrencyType
                {
                    ApplicationUserId = User.Identity.GetUserId(),
                    CurrencyName = model.CurrencyName,
                };
                db.CurrencyTypes.Add(currency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: CurrencyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyTypes.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // POST: CurrencyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrencyName")] CurrencyType currencyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencyType);
        }

        // GET: CurrencyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = db.CurrencyTypes.Find(id);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            return View(currencyType);
        }

        // POST: CurrencyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyType currencyType = db.CurrencyTypes.Find(id);
            db.CurrencyTypes.Remove(currencyType);
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
