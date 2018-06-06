using BloodDonation.Models;
using DAL.Models;
using DAL.UnitOfWork.Contract;
using Microsoft.AspNet.Identity;
using System;
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
            var userId = User.Identity.GetUserId();
            var currentUser = this.unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();
            var blood = unitOfWork.BloodRepository.Get(x => x.BloodBankId == currentUser.Hospital_Id);
            BloodAndProductsViewModel bloodAndProductsViewModel = new BloodAndProductsViewModel();
            bloodAndProductsViewModel.BloodA = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodO = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodB = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodAB = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodAN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodON = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodBN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodABN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.RedCell = new Product()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.WhiteCell = new Product()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.Plasma = new Product()
            {
                Amount = 0
            };

            var products = unitOfWork.ProductRepository.Get(x => x.BloodBank_Id == currentUser.Hospital_Id);

            foreach (var b in blood.Where(x=>x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Positive)){
                bloodAndProductsViewModel.BloodO.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodON.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodA.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodAN.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodB.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodBN.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodAB.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodABN.Amount += b.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.BloodCell && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.RedCell.Amount += p.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Whitecell && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.WhiteCell.Amount += p.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Plasma && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.Plasma.Amount += p.Amount;
            }
            return View(bloodAndProductsViewModel);
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
                    List<Request> hrequest = unitOfWork.RequestRepository.Get(x => x.User_Id == d.Id && x.Amount > 0).ToList();
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
            donation.Succesfull = Common.Enums.Stage.SendAnalyse;
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

        public ActionResult SaveBlood(Donation don)
        {
            var donation = unitOfWork.DonationRepository.Get(x => x.Id == don.Id).First();

            donation.Succesfull = Common.Enums.Stage.SendAnalyse;
            unitOfWork.DonationRepository.Update(donation);

            var blood = new Blood()
            {
                Rh = donation.RhType,
                Type = donation.BloodType,
                BloodBankId = donation.BloodBank_Id,
                Amount = (float)don.Amount
            };

            unitOfWork.BloodRepository.Insert(blood);
            unitOfWork.Save();

            return RedirectToAction("Donations");
        }

        public ActionResult SaveProducts(Donation don)
        {
            var donation = unitOfWork.DonationRepository.Get(x => x.Id == don.Id).First();

            donation.Succesfull = Common.Enums.Stage.SendAnalyse;
            unitOfWork.DonationRepository.Update(donation);

            var p1 = new Product()
            {
                BloodBank_Id = donation.BloodBank_Id,
                Type = Common.Enums.ProductType.BloodCell,
                ExpireTime = DateTime.Today.AddDays(7),
                Amount = (float)don.Amount / 3
            };

            var p2 = new Product()
            {
                BloodBank_Id = donation.BloodBank_Id,
                Type = Common.Enums.ProductType.Whitecell,
                ExpireTime = DateTime.Today.AddDays(7),
                Amount = (float)don.Amount / 3
            };

            var p3 = new Product()
            {
                BloodBank_Id = donation.BloodBank_Id,
                Type = Common.Enums.ProductType.Plasma,
                ExpireTime = DateTime.Today.AddDays(7),
                Amount = (float)don.Amount / 3
            };

            unitOfWork.ProductRepository.Insert(p1);
            unitOfWork.ProductRepository.Insert(p2);
            unitOfWork.ProductRepository.Insert(p3);
            unitOfWork.Save();

            return RedirectToAction("Donations");
        }

        [HttpGet]
        public ActionResult SendAnalyse(int id)
        {
            var model = new BloodTestResult()
            {
                Donation_Id = id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SendAnalyse(BloodTestResult bloodTestResult)
        {
            var donation = unitOfWork.DonationRepository.Get(x => x.Id == bloodTestResult.Donation_Id).First();
            donation.Succesfull = Common.Enums.Stage.Succesfull;

            unitOfWork.BloodTestResultRepository.Insert(bloodTestResult);
            unitOfWork.Save();

            return RedirectToAction("Donations");
        }


        public ActionResult AcceptRequest(int id)
        {
            var req = unitOfWork.RequestRepository.Get(x => x.Id == id).First();

            var userId = User.Identity.GetUserId();
            var currentUser = this.unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();
            var blood = unitOfWork.BloodRepository.Get(x => x.BloodBankId == currentUser.Hospital_Id);
            BloodAndProductsViewModel bloodAndProductsViewModel = new BloodAndProductsViewModel();
            bloodAndProductsViewModel.BloodA = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodO = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodB = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodAB = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodAN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodON = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodBN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.BloodABN = new Blood()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.RedCell = new Product()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.WhiteCell = new Product()
            {
                Amount = 0
            };
            bloodAndProductsViewModel.Plasma = new Product()
            {
                Amount = 0
            };

            var products = unitOfWork.ProductRepository.Get(x => x.BloodBank_Id == currentUser.Hospital_Id);

            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodO.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodON.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodA.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodAN.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodB.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodBN.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Positive))
            {
                bloodAndProductsViewModel.BloodAB.Amount += b.Amount;
            }
            foreach (var b in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Negative))
            {
                bloodAndProductsViewModel.BloodABN.Amount += b.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.BloodCell && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.RedCell.Amount += p.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Whitecell && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.WhiteCell.Amount += p.Amount;
            }
            foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Plasma && x.ExpireTime > DateTime.Today))
            {
                bloodAndProductsViewModel.Plasma.Amount += p.Amount;
            }

            if(req.ProductType == Common.Enums.ProductType.Blood)
            {
                if(req.BloodType == Common.Enums.BloodType.O && req.Rh == Common.Enums.RhType.Positive && req.Amount <= bloodAndProductsViewModel.BloodO.Amount)
                {
                    foreach(var p in blood.Where(x => x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Positive))
                    {
                        if(p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.O && req.Rh == Common.Enums.RhType.Negative && req.Amount <= bloodAndProductsViewModel.BloodON.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.O && x.Rh == Common.Enums.RhType.Negative))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.A && req.Rh == Common.Enums.RhType.Positive && req.Amount <= bloodAndProductsViewModel.BloodA.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Positive))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.A && req.Rh == Common.Enums.RhType.Negative && req.Amount <= bloodAndProductsViewModel.BloodAN.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.A && x.Rh == Common.Enums.RhType.Negative))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.B && req.Rh == Common.Enums.RhType.Positive && req.Amount <= bloodAndProductsViewModel.BloodB.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Positive))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.B && req.Rh == Common.Enums.RhType.Negative && req.Amount <= bloodAndProductsViewModel.BloodBN.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.B && x.Rh == Common.Enums.RhType.Negative))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.AB && req.Rh == Common.Enums.RhType.Positive && req.Amount <= bloodAndProductsViewModel.BloodAB.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Positive))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
                if (req.BloodType == Common.Enums.BloodType.AB && req.Rh == Common.Enums.RhType.Negative && req.Amount <= bloodAndProductsViewModel.BloodABN.Amount)
                {
                    foreach (var p in blood.Where(x => x.Type == Common.Enums.BloodType.AB && x.Rh == Common.Enums.RhType.Negative))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.BloodRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.BloodRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
            }
            if(req.ProductType == Common.Enums.ProductType.BloodCell)
            {
                if(bloodAndProductsViewModel.RedCell.Amount >= req.Amount)
                {
                    foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.BloodCell).OrderBy(x=>x.ExpireTime))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.ProductRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.ProductRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
            }
            if (req.ProductType == Common.Enums.ProductType.Whitecell)
            {
                if (bloodAndProductsViewModel.WhiteCell.Amount >= req.Amount)
                {
                    foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Whitecell).OrderBy(x => x.ExpireTime))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.ProductRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.ProductRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
            }
            if (req.ProductType == Common.Enums.ProductType.Plasma)
            {
                if (bloodAndProductsViewModel.RedCell.Amount >= req.Amount)
                {
                    foreach (var p in products.Where(x => x.Type == Common.Enums.ProductType.Plasma).OrderBy(x => x.ExpireTime))
                    {
                        if (p.Amount <= req.Amount)
                        {
                            req.Amount -= p.Amount;
                            unitOfWork.ProductRepository.Delete(p);
                        }
                        else
                        {
                            p.Amount -= req.Amount;
                            req.Amount = 0;
                            unitOfWork.ProductRepository.Update(p);
                        }
                    }
                    unitOfWork.Save();
                }
            }

            unitOfWork.RequestRepository.Update(req);
            unitOfWork.Save();

            return RedirectToAction("Requests");
        }

        public ActionResult SendNotification(int id)
        {
            var request = unitOfWork.RequestRepository.Get(x => x.Id == id).First();
            
            var userId = User.Identity.GetUserId();
            var currentUser = this.unitOfWork.UserRepository.Get(x => x.Id == userId).FirstOrDefault();
            var countyId = unitOfWork.BloodBankRepository.Get(x => x.Id == currentUser.Hospital_Id).FirstOrDefault().County_Id;

            var notif = new Notification()
            {
                Request_Id = id,
            };
            unitOfWork.NotificationRepository.Insert(notif);
            unitOfWork.Save();
            var users = unitOfWork.UserRepository.Get(x => x.County_Id == countyId && x.BloodType == request.BloodType && x.RhType == request.Rh);

            foreach(var user in users)
            {
                unitOfWork.UserNotificationRepository.Insert(new UserNotification()
                {
                    Notification_Id = notif.Id,
                    User_Id = user.Id
                });
            }

            return RedirectToAction("Requests");
        }
    }
}
