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
    public class AccountTypesController : Controller
    {
        IUnitOfWork _unitOfWork;

        public AccountTypesController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: AccountTypes
        public ActionResult Index()
        {
            var accountTypes = _unitOfWork.AccountTypes.GetAll();
            var viewModel = Mapper.Map<List<AccountTypeViewModel>>(accountTypes);
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
                account = Mapper.Map<AccountType>(model);
                _unitOfWork.AccountTypes.Add(account);
                _unitOfWork.Complete();
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
            var ac = _unitOfWork.AccountTypes.Get(id.Value);
            if (ac == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<AccountTypeViewModel>(ac);
            return View(vm);
        }

        // POST: AccountTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Comments")] AccountTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountType = Mapper.Map<AccountType>(model);
                _unitOfWork.AccountTypes.Update(accountType);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: AccountTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountType = _unitOfWork.AccountTypes.Get(id.Value);
            if (accountType == null)
            {
                return HttpNotFound();
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
            _unitOfWork.AccountTypes.Remove(record);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
