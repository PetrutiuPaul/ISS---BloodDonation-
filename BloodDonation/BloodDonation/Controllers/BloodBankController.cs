using DAL.Models;
using DAL.UnitOfWork.Contract;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "BloodBankDoctor")]
    public class BloodBankController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public BloodBankController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: BloodBank
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Requests()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();
            var hospitals = this.unitOfWork.BloodBankRepository.Get(x => x.Id == currentUser.Hospital_Id, includeProperties: "Hospitals").FirstOrDefault().Hospitals;
            List<DAL.Models.User> doctors = new List<DAL.Models.User>();
            List<Request> allReq = new List<DAL.Models.Request>();
            foreach (var h in hospitals)
            {
                List<DAL.Models.User> hDoctors = unitOfWork.UserRepository.Get(x => x.Hospital_Id == h.Id).ToList();
                foreach (var d in hDoctors)
                {
                    List<Request> hrequest = unitOfWork.RequestRepository.Get(x => x.User_Id == d.Id).ToList();
                    allReq.AddRange(hrequest);
                }
            }

            return View(allReq);
        }

        // GET: BloodBank/Details/5
        public ActionResult Donations()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();
            var allDonations = this.unitOfWork.DonationRepository.Get(x => x.BloodBank_Id == currentUser.Hospital_Id, includeProperties: "User");

            return View(allDonations);
        }

        // GET: BloodBank/Create
        public ActionResult FirstAnalyse(int id)
        {
            var donation = this.unitOfWork.DonationRepository.Get(x => x.Id == id).FirstOrDefault();
            return View(donation);
        }

        [HttpPost]
        public ActionResult FirstAnalyse(Donation don)
        {
            var donation = this.unitOfWork.DonationRepository.Get(x => x.Id == don.Id).FirstOrDefault();
            donation.RhType = don.RhType;
            donation.BloodType = don.BloodType;
            donation.Succesfull = Common.Enums.Stage.Accepted;
            unitOfWork.DonationRepository.Update(donation);
            unitOfWork.Save();
            return RedirectToAction("Donations");
        }

        public ActionResult CancelAnalyse(int id)
        {
            var donation = this.unitOfWork.DonationRepository.Get(x => x.Id == id).FirstOrDefault();
            return View(donation);
        }

        [HttpPost]
        public ActionResult CancelAnalyse(Donation don)
        {
            var donation = this.unitOfWork.DonationRepository.Get(x => x.Id == don.Id).FirstOrDefault();
            donation.DenialReason = don.DenialReason;
            donation.Succesfull = Common.Enums.Stage.Rejected;
            unitOfWork.DonationRepository.Update(donation);
            unitOfWork.Save();
            return RedirectToAction("Donations");
        }

        // GET: BloodBank/Create
        public ActionResult SecondAnalyse(int id)
        {
            var donation = this.unitOfWork.DonationRepository.Get(x => x.Id == id).FirstOrDefault();
            return View(donation);
        }

        // POST: BloodBank/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BloodBank/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodBank/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BloodBank/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
