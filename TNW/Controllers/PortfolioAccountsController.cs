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
using TNW.Extensions;
using TNW.Infrastructure;
using TNW.Interfaces;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.Controllers
{
    [Authorize]
    public class PortfolioAccountsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public PortfolioAccountsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: PortfolioAccounts
        public ActionResult Index()
        {
            var portfolioAccounts = _unitOfWork.PortfolioAccounts.GetAll(null, p => p.AccountType);
            var vm = Mapper.Map<List<PortfolioAccountViewModel>>(portfolioAccounts);
            return View(vm);
        }

        // GET: PortfolioAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var portfolioAccount = _unitOfWork
                .PortfolioAccounts.Get(p => p.Id == id, p => p.AccountType, p => p.AssetType, p => p.CurrencyType);
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<PortfolioAccountViewModel>(portfolioAccount);
            return View(vm);
        }

        // GET: PortfolioAccounts/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypeId = new SelectList(_unitOfWork.AccountTypes.GetAll(), "Id", "Name");
            ViewBag.AssetTypeId = new SelectList(_unitOfWork.AssetTypes.GetAll(), "Id", "TypeName");
            ViewBag.CurrencyTypeId = new SelectList(_unitOfWork.CurrencyTypes.GetAll(), "Id", "CurrencyName");
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
                var newAccount = Mapper.Map<PortfolioAccount>(model);
                newAccount.OwnerId = User.Identity.GetUserId();
                _unitOfWork.PortfolioAccounts.Add(newAccount);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            SetupSelectList(model);
            return View(model);
        }

        private void SetupSelectList(PortfolioAccountViewModel model)
        {
            ViewBag.AccountTypeId = new SelectList(_unitOfWork.AccountTypes.GetAll(), "Id", "Name", model.AccountTypeId);
            ViewBag.AssetTypeId = new SelectList(_unitOfWork.AssetTypes.GetAll(), "Id", "TypeName", model.AssetTypeId);
            ViewBag.CurrencyTypeId = new SelectList(_unitOfWork.CurrencyTypes.GetAll(), "Id", "CurrencyName", model.CurrencyTypeId);
        }

        // GET: PortfolioAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var portfolioAccount = _unitOfWork.PortfolioAccounts.Get(id.Value);
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<PortfolioAccountViewModel>(portfolioAccount);
            SetupSelectList(vm);
            return View(vm);
        }

        // POST: PortfolioAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OwnerId,AccountHolder,IsActive,FinancialInstitution,AccountNumber,AccountTypeId,AssetTypeId,CurrencyTypeId")] PortfolioAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var portfolioAccount = Mapper.Map<PortfolioAccount>(model);
                _unitOfWork.PortfolioAccounts.Update(portfolioAccount);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            SetupSelectList(model);
            return View(model);
        }

        // GET: PortfolioAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var portfolioAccount = _unitOfWork
                .PortfolioAccounts.Get(p => p.Id == id.Value, p => p.AccountType, p => p.AssetType, p => p.CurrencyType);
            if (portfolioAccount == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<PortfolioAccountViewModel>(portfolioAccount);
            return View(vm);
        }

        // POST: PortfolioAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var portfolioAccount = _unitOfWork.PortfolioAccounts.Get(id);
            _unitOfWork.PortfolioAccounts.Remove(portfolioAccount);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
