using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TNW.Extensions;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.Controllers
{
    [Authorize]
    public class PortfolioAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PortfolioAccounts
        public ActionResult Index()
        {
            var portfolioAccounts = db.PortfolioAccounts.Include(p => p.AccountType).Include(p => p.AssetType).Include(p => p.CurrencyType).Include(p => p.Owner);
            return View(portfolioAccounts.ToList());
        }

        // GET: PortfolioAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioAccount portfolioAccount = db
                .PortfolioAccounts
                .Where(p => p.Id == id)
                .Include(p => p.AccountType)
                .Include(p => p.AssetType)
                .Include(p => p.CurrencyType)
                .SingleOrDefault();
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            return View(portfolioAccount);
        }

        // GET: PortfolioAccounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Name");
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName");
            ViewBag.CurrencyTypeId = new SelectList(db.CurrencyTypes, "Id", "CurrencyName");
            var model = new PortfolioAccountViewModel
            {
                IsActive = true,
                AccountHolder = User.Identity.GetName(),
            };
            return View(model);
        }

        // POST: PortfolioAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountHolder,IsActive,FinancialInstitution,AccountNumber,AccountTypeId,AssetTypeId,CurrencyTypeId")] PortfolioAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newAccount = new PortfolioAccount
                {
                    AccountHolder = model.AccountHolder,
                    AccountNumber = model.AccountNumber,
                    AccountTypeId = model.AccountTypeId,
                    AssetTypeId = model.AssetTypeId,
                    CurrencyTypeId = model.CurrencyTypeId,
                    FinancialInstitution = model.FinancialInstitution,
                    IsActive = model.IsActive,
                    OwnerId = User.Identity.GetUserId(),
                };
                db.PortfolioAccounts.Add(newAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Name", model.AccountTypeId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", model.AssetTypeId);
            ViewBag.CurrencyTypeId = new SelectList(db.CurrencyTypes, "Id", "CurrencyName", model.CurrencyTypeId);
            return View(model);
        }

        // GET: PortfolioAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioAccount portfolioAccount = db.PortfolioAccounts.Find(id);
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Name", portfolioAccount.AccountTypeId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", portfolioAccount.AssetTypeId);
            ViewBag.CurrencyTypeId = new SelectList(db.CurrencyTypes, "Id", "CurrencyName", portfolioAccount.CurrencyTypeId);
            return View(portfolioAccount);
        }

        // POST: PortfolioAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OwnerId,AccountHolder,IsActive,FinancialInstitution,AccountNumber,AccountTypeId,AssetTypeId,CurrencyTypeId")] PortfolioAccount portfolioAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolioAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "Id", "Name", portfolioAccount.AccountTypeId);
            ViewBag.AssetTypeId = new SelectList(db.AssetTypes, "Id", "TypeName", portfolioAccount.AssetTypeId);
            ViewBag.CurrencyTypeId = new SelectList(db.CurrencyTypes, "Id", "CurrencyName", portfolioAccount.CurrencyTypeId);
            return View(portfolioAccount);
        }

        // GET: PortfolioAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioAccount portfolioAccount = db.PortfolioAccounts.Find(id);
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            return View(portfolioAccount);
        }

        // POST: PortfolioAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortfolioAccount portfolioAccount = db.PortfolioAccounts.Find(id);
            db.PortfolioAccounts.Remove(portfolioAccount);
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
