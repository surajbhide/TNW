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
using TNW.Controllers.Filters;
using TNW.Extensions;
using TNW.Infrastructure;
using TNW.Interfaces;
using TNW.Models;
using TNW.ViewModels;

namespace TNW.Controllers
{
    [Authorize]
    public class AccountTypesController : ControllerBase
    {
        public AccountTypesController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: AccountTypes
        public ActionResult Index()
        {
            var accountTypes = _unitOfWork.AccountTypes.GetAll(a => a.OwnerId == UserId);
            var viewModel = Mapper.Map<List<AccountTypeSummaryViewModel>>(accountTypes);
            return View(viewModel);
        }

        // GET: AccountTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = Mapper.Map<AccountType>(model);
                account.OwnerId = User.Identity.GetUserId();
                _unitOfWork.AccountTypes.Add(account);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: AccountTypes/Edit/5
        public ActionResult Edit(int id)
        {
            var ac = _unitOfWork.AccountTypes.Get(id);
            if (ac == null)
            {
                return HttpNotFound();
            }
            // if the user is not authorized to view this account say so
            if (ac.OwnerId != UserId)
            {
                return new HttpUnauthorizedResult("You are not authorized to edit this record.");
            }
            var vm = Mapper.Map<AccountTypeViewModel>(ac);
            return View(vm);
        }

        // POST: AccountTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AccountTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ensure the id is valid and belongs to this user.
                var accountType = _unitOfWork.AccountTypes.Get(id);
                if (accountType == null)
                {
                    return HttpNotFound();
                }
                if (accountType.OwnerId != UserId)
                {
                    return new HttpUnauthorizedResult("You are not authorized to edit this record.");
                }
                Mapper.Map(model, accountType);
                _unitOfWork.AccountTypes.Update(accountType);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: AccountTypes/Delete/5
        public ActionResult Delete(int id)
        {
            var accountType = _unitOfWork.AccountTypes.Get(id);
            if (accountType == null)
            {
                return HttpNotFound();
            }
            if (accountType.OwnerId != UserId)
            {
                return new HttpUnauthorizedResult("You are not authorized to delete this record.");
            }
            return View(Mapper.Map<AccountTypeViewModel>(accountType));
        }

        // POST: AccountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var record = _unitOfWork.AccountTypes.Get(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            if (record.OwnerId != UserId)
            {
                return new HttpUnauthorizedResult("You are not authorized to delete this record.");
            }
            _unitOfWork.AccountTypes.Remove(record);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
