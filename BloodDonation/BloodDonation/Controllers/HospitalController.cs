using DAL.Models;
using DAL.UnitOfWork.Contract;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HospitalController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();

            var requests = unitOfWork.RequestRepository.Get(x => x.User_Id == userId );
            return View(requests);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Request request)
        {
            try
            {
                request.User_Id = User.Identity.GetUserId();

                unitOfWork.RequestRepository.Insert(request);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hospital/Edit/5
        public ActionResult Edit(int id)
        {
            var req = unitOfWork.RequestRepository.Get(x => x.Id == id).FirstOrDefault();
            return View(req);
        }

        // POST: Hospital/Edit/5
        [HttpPost]
        public ActionResult Edit(Request request)
        {
            try
            {
                unitOfWork.RequestRepository.Update(request);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hospital/Delete/5
        public ActionResult Delete(int id)
        {
            var req = unitOfWork.RequestRepository.Get(x => x.Id == id).FirstOrDefault();
            return View(req);
        }

        // POST: Hospital/Delete/5
        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                unitOfWork.RequestRepository.Delete(unitOfWork.RequestRepository.Get(x => x.Id == id).FirstOrDefault());
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
