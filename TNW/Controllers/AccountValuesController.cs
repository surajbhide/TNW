using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using TNW.Infrastructure;
using TNW.Interfaces;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.Controllers
{
    [Authorize]
    public class AccountValuesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AccountValuesController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: AccountValues
        public ActionResult Index()
        {
            var accountValues = _unitOfWork.AccountValues.GetAll(null, a => a.PortfolioAccount);
            var vm = Mapper.Map<List<AccountValueViewModel>>(accountValues);
            return View(vm);
        }

        // GET: AccountValues/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();

            //TODO: remove duplicate code into a single method
            var accounts = _unitOfWork
                .PortfolioAccounts.GetAll(p => p.OwnerId == userId, p => p.AccountType)
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
        public ActionResult Create([Bind(Include = "Id,MonthlyBalance,MonthYear,PortfolioAccountId")] AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountVal = Mapper.Map<AccountValue>(model);
                _unitOfWork.AccountValues.Add(accountVal);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var accounts = _unitOfWork
                .PortfolioAccounts.GetAll(p => p.OwnerId == userId, p => p.AccountType)
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();
            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", model.PortfolioAccountId);
            return View(model);
        }

        // GET: AccountValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountValue accountValue = _unitOfWork.AccountValues.Get(id.Value);
            if (accountValue == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<AccountValueViewModel>(accountValue);

            var userId = User.Identity.GetUserId();
            var accounts = _unitOfWork
                .PortfolioAccounts.GetAll(p => p.OwnerId == userId, p => p.AccountType)
                .Select(s => new
                {
                    Id = s.Id,
                    Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                })
                .ToList();
            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", vm.PortfolioAccountId);
            return View(vm);
        }

        // POST: AccountValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MonthlyBalance,MonthYear,PortfolioAccountId")] AccountValueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountVal = Mapper.Map<AccountValue>(model);
                _unitOfWork.AccountValues.Update(accountVal);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var accounts = _unitOfWork
                        .PortfolioAccounts.GetAll(p => p.OwnerId == userId, p => p.AccountType)
                        .Select(s => new
                        {
                            Id = s.Id,
                            Description = $"{s.AccountNumber} - {s.AccountHolder} - {s.FinancialInstitution} - {s.AccountType.Name}",
                        })
                        .ToList();

            ViewBag.PortfolioAccountId = new SelectList(accounts, "Id", "Description", model.PortfolioAccountId);
            return View(model);
        }

        // GET: AccountValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountValue accountValue = _unitOfWork.AccountValues.Get(id.Value);
            if (accountValue == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<AccountValueViewModel>(accountValue);
            return View(vm);
        }

        // POST: AccountValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountValue accountValue = _unitOfWork.AccountValues.Get(id);
            _unitOfWork.AccountValues.Remove(accountValue);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
