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
    public class CurrencyTypesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CurrencyTypesController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        // GET: CurrencyTypes
        public ActionResult Index()
        {
            //TODO: show only current user's currency information
            var currencies = _unitOfWork.CurrencyTypes.GetAll();
            var vm = Mapper.Map<List<CurrencyTypeViewModel>>(currencies);
            return View(vm);
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
                var currency = Mapper.Map<CurrencyType>(model);
                currency.ApplicationUserId = User.Identity.GetUserId();
                _unitOfWork.CurrencyTypes.Add(currency);
                _unitOfWork.Complete();
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
            CurrencyType currencyType = _unitOfWork.CurrencyTypes.Get(id.Value);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<CurrencyTypeViewModel>(currencyType);
            return View(vm);
        }

        // POST: CurrencyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrencyName")] CurrencyTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var currencyType = Mapper.Map<CurrencyType>(vm);
                _unitOfWork.CurrencyTypes.Update(currencyType);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: CurrencyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyType currencyType = _unitOfWork.CurrencyTypes.Get(id.Value);
            if (currencyType == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<CurrencyTypeViewModel>(currencyType);
            return View(vm);
        }

        // POST: CurrencyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrencyType currencyType = _unitOfWork.CurrencyTypes.Get(id);
            _unitOfWork.CurrencyTypes.Remove(currencyType);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
