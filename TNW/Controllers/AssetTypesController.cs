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
    public class AssetTypesController : ControllerBase
    {
        public AssetTypesController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: AssetTypes
        public ActionResult Index()
        {
            var assetTypes = _unitOfWork.AssetTypes.GetAll(a => a.OwnerId == UserId);
            var assetVM = Mapper.Map<List<AssetTypeSummaryViewModel>>(assetTypes);
            return View(assetVM);
        }

        // GET: AssetTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetTypeViewModel assetType)
        {
            if (ModelState.IsValid)
            {
                var asset = Mapper.Map<AssetType>(assetType);
                asset.OwnerId = User.Identity.GetUserId();
                _unitOfWork.AssetTypes.Add(asset);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(assetType);
        }

        // GET: AssetTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var assetType = _unitOfWork.AssetTypes.Get(id.Value);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<AssetTypeViewModel>(assetType);
            return View(vm);
        }

        // POST: AssetTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AssetTypeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var assetType = Mapper.Map<AssetType>(vm);
                _unitOfWork.AssetTypes.Update(assetType);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: AssetTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetType assetType = _unitOfWork.AssetTypes.Get(id.Value);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            var vm = Mapper.Map<AssetTypeViewModel>(assetType);
            return View(vm);
        }

        // POST: AssetTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssetType assetType = _unitOfWork.AssetTypes.Get(id);
            if (assetType == null)
            {
                return HttpNotFound();
            }
            _unitOfWork.AssetTypes.Remove(assetType);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
