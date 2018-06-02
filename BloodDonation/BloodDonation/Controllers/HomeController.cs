using BloodDonation.Models;
using Common.Enums;
using DAL.Models;
using DAL.UnitOfWork;
using DAL.UnitOfWork.Contract;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CreateViewBag();
        }

        public ActionResult Index()
        {
            return View();   
        }

        private void CreateViewBag()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            var all = unitOfWork.BloodBankRepository.Get();
            foreach(var h in all)
            {
                listItems.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }
            ViewBag.BloodBanks = listItems;
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateDonation(DonationViewModel donationViewModel)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();

                Donation donation = new Donation()
                {
                    Amount = 0,
                    BloodBank_Id = donationViewModel.BloodBank_Id,
                    DenialReason = "",
                    RhType = user.RhType ?? RhType.X,
                    User_Id = user.Id,
                    BloodType = user.BloodType ?? BloodType.X,
                    Succesfull = Stage.Pending
                };
                unitOfWork.DonationRepository.Insert(donation);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Error");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}