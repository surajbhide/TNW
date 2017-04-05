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

namespace TNW.Controllers
{
    [Authorize]
    public class AccountValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccountValues
        public ActionResult Index()
        {
            var accountValues = db.AccountValues.Include(a => a.PortfolioAccount);
            return View(accountValues.ToList());
        }

        // GET: AccountValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountValue accountValue = db.AccountValues.Find(id);
            if (accountValue == null)
            {
                return HttpNotFound();
            }
            return View(accountValue);
        }

        // GET: AccountValues/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();

            //TODO: remove duplicate code into a single method
            var accounts = db.PortfolioAccounts
                .Where(p => p.OwnerId == userId)
                .Include(p => p.AccountType)
                .AsEnumerable()
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();
            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description");
            return View();
        }

        // POST: AccountValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MonthlyBalance,MonthYear,PortfolioAccountId")] AccountValue accountValue)
        {
            if (ModelState.IsValid)
            {
                db.AccountValues.Add(accountValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var accounts = db.PortfolioAccounts
                .Where(p => p.OwnerId == userId)
                .Include(p => p.AccountType)
                .AsEnumerable()
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();
            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", accountValue.PortfolioAccountId);
            return View(accountValue);
        }

        // GET: AccountValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountValue accountValue = db.AccountValues.Find(id);
            if (accountValue == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var accounts = db.PortfolioAccounts
                .Where(p => p.OwnerId == userId)
                .Include(p => p.AccountType)
                .AsEnumerable()
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();

            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", accountValue.PortfolioAccountId);
            return View(accountValue);
        }

        // POST: AccountValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MonthlyBalance,MonthYear,PortfolioAccountId")] AccountValue accountValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var accounts = db.PortfolioAccounts
                .Where(p => p.OwnerId == userId)
                .Include(p => p.AccountType)
                .AsEnumerable()
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();

            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", accountValue.PortfolioAccountId);
            return View(accountValue);
        }

        // GET: AccountValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountValue accountValue = db.AccountValues.Find(id);
            if (accountValue == null)
            {
                return HttpNotFound();
            }
            return View(accountValue);
        }

        // POST: AccountValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountValue accountValue = db.AccountValues.Find(id);
            db.AccountValues.Remove(accountValue);
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
