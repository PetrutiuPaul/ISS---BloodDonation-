using BloodDonation.Models;
using DAL.Models;
using DAL.UnitOfWork.Contract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ApplicationUserManager _userManager;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CreateViewBag();
        }

        public AdminController(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            this.unitOfWork = unitOfWork;
            UserManager = userManager;
            CreateViewBag();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hospitals()
        {
            var hospitals = unitOfWork.HospitalRepository.Get();
            return View(hospitals);
        }
        
        public ActionResult CreateHospital()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateHospital(HospitalViewModel hospitalViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Hospital hospital = new Hospital()
                    {
                        Name = hospitalViewModel.Name,
                        BloodBank_Id = hospitalViewModel.BloodBank_Id
                    };
                    unitOfWork.HospitalRepository.Insert(hospital);
                    unitOfWork.Save();

                    return RedirectToAction("Hospitals");
                }
                catch
                {
                    return View();
                }
            }
            else
                return View();
        }
        
        public ActionResult EditHospital(int id)
        {
            var h = unitOfWork.HospitalRepository.GetByID(id);
            return View(new HospitalViewModel()
            {
                Id = id,
                BloodBank_Id = h.BloodBank_Id,
                Name = h.Name
            });
        }
        
        [HttpPost]
        public ActionResult EditHospital(HospitalViewModel hospitalViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Hospital toUpdate = unitOfWork.HospitalRepository.GetByID(hospitalViewModel.Id);
                    toUpdate.Name = hospitalViewModel.Name;
                    toUpdate.BloodBank_Id = hospitalViewModel.BloodBank_Id;
                    unitOfWork.HospitalRepository.Update(toUpdate);
                    unitOfWork.Save();
                    return RedirectToAction("Hospitals");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult DeleteHospital(int id)
        {
            var h = unitOfWork.HospitalRepository.GetByID(id);
            return View(new HospitalViewModel()
            {
                Id = id,
                BloodBank_Id = h.BloodBank_Id,
                Name = h.Name
            });
        }
        
        [HttpPost]
        public ActionResult ConfirmDeleteHospital(int id)
        {
            try
            {
                unitOfWork.HospitalRepository.Delete(id);
                unitOfWork.Save();
                return RedirectToAction("Hospitals");
            }
            catch
            {
                return View();
            }
        }

        // --------------------------------------

        public ActionResult BloodBanks()
        {
            var bloodBanks = unitOfWork.BloodBankRepository.Get();
            return View(bloodBanks);
        }

        public ActionResult CreateBloodBank()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBloodBank(BloodBankViewModel bloodBankViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BloodBank hospital = new BloodBank()
                    {
                        Name = bloodBankViewModel.Name,
                        County_Id = bloodBankViewModel.County_Id
                    };
                    unitOfWork.BloodBankRepository.Insert(hospital);
                    unitOfWork.Save();

                    return RedirectToAction("BloodBanks");
                }
                catch
                {
                    return View();
                }
            }
            else
                return View();
        }

        public ActionResult EditBloodBank(int id)
        {
            var h = unitOfWork.BloodBankRepository.GetByID(id);
            return View(new BloodBankViewModel()
            {
                Id = id,
                County_Id = h.County_Id,
                Name = h.Name
            });
        }

        [HttpPost]
        public ActionResult EditBloodBank(BloodBankViewModel hospitalViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BloodBank toUpdate = unitOfWork.BloodBankRepository.GetByID(hospitalViewModel.Id);
                    toUpdate.Name = hospitalViewModel.Name;
                    toUpdate.County_Id = hospitalViewModel.County_Id;
                    unitOfWork.BloodBankRepository.Update(toUpdate);
                    unitOfWork.Save();
                    return RedirectToAction("BloodBanks");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteBloodBank(int id)
        {
            var h = unitOfWork.BloodBankRepository.GetByID(id);
            return View(new BloodBankViewModel()
            {
                Id = id,
                County_Id = h.County_Id,
                Name = h.Name
            });
        }

        [HttpPost]
        public ActionResult ConfirmDeleteBloodBank(int id)
        {
            try
            {
                unitOfWork.BloodBankRepository.Delete(id);
                unitOfWork.Save();
                return RedirectToAction("BloodBanks");
            }
            catch
            {
                return View();
            }
        }

        //------------------------------------------------

        public ActionResult Doctors()
        {
            var users = unitOfWork.UserRepository.Get();
            List<User> allDoctors = new List<User>();
            foreach(var user in users)
            {
                if(UserManager.IsInRole(user.Id,"Doctor") || UserManager.IsInRole(user.Id, "BloodBankDoctor"))
                    allDoctors.Add(user);
            }
            return View(allDoctors);
        }

        public ActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDoctor(DoctorViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        UserName = userViewModel.Email,
                        Email = userViewModel.Email,
                        BirthDate = userViewModel.BirthDate,
                        CNP = userViewModel.CNP,
                        County_Id = userViewModel.County_Id,
                        LastName = userViewModel.LastName,
                        FirstName = userViewModel.FirstName,
                        Locality_Id = userViewModel.Locality_Id,
                        Hospital_Id = userViewModel.Hospital_Id
                    };
                        var result = UserManager.Create(user, userViewModel.Password);

                    

                        if(result != null)
                        {
                            if(userViewModel.Doctor)
                                UserManager.AddToRole(user.Id, "Doctor");
                            if (userViewModel.BloodBankDoctor)
                                UserManager.AddToRole(user.Id, "BloodBankDoctor");
                        }
                        
                        return RedirectToAction("Doctors");
                }
                catch(Exception ex)
                {
                    return View();
                }
            }
            else
                return View();
        }

        public ActionResult EditDoctor(string cnp)
        {
            var doctor = unitOfWork.UserRepository.Get(x => x.CNP == cnp)[0];
            var model = new DoctorViewModel
            {
                Email = doctor.Email,
                BirthDate = doctor.BirthDate,
                CNP = doctor.CNP,
                County_Id = doctor.County_Id,
                LastName = doctor.LastName,
                FirstName = doctor.FirstName,
                Locality_Id = doctor.Locality_Id
            };
            if (UserManager.IsInRole(doctor.Id, "Doctor"))
                model.Doctor = true;
            if (UserManager.IsInRole(doctor.Id, "BloodBankDoctor"))
                model.BloodBankDoctor = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditDoctor(DoctorViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User toUpdate = unitOfWork.UserRepository.Get(x => x.CNP == userViewModel.CNP)[0];
                    toUpdate.Email = userViewModel.Email;
                    toUpdate.BirthDate = userViewModel.BirthDate;
                    toUpdate.CNP = userViewModel.CNP;
                    toUpdate.County_Id = userViewModel.County_Id;
                    toUpdate.LastName = userViewModel.LastName;
                    toUpdate.FirstName = userViewModel.FirstName;
                    toUpdate.Locality_Id = userViewModel.Locality_Id;
                    toUpdate.Hospital_Id = userViewModel.Hospital_Id;
                    if (userViewModel.Doctor == true)
                        UserManager.AddToRole(toUpdate.Id, "Doctor");
                    if (userViewModel.BloodBankDoctor == true)
                        UserManager.AddToRole(toUpdate.Id, "BloodBankDoctor");
                    unitOfWork.UserRepository.Update(toUpdate);
                    unitOfWork.Save();
                    return RedirectToAction("Doctors");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteDoctor(string cnp)
        {
            User doctor = unitOfWork.UserRepository.Get(x => x.CNP == cnp)[0];
            var model = new DoctorViewModel
            {
                Email = doctor.Email,
                BirthDate = doctor.BirthDate,
                CNP = doctor.CNP,
                County_Id = doctor.County_Id,
                LastName = doctor.LastName,
                FirstName = doctor.FirstName,
                Locality_Id = doctor.Locality_Id
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult ConfirmDeleteDoctor(string cnp)
        {
            try
            {
                User doctor = unitOfWork.UserRepository.Get(x => x.CNP == cnp)[0];
                unitOfWork.UserRepository.Delete(doctor);
                unitOfWork.Save();
                return RedirectToAction("Doctors");
            }
            catch
            {
                return View();
            }
        }

        private void CreateViewBag()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            var all = unitOfWork.BloodBankRepository.Get();
            foreach (var h in all)
            {
                listItems.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            List<SelectListItem> BandD = new List<SelectListItem>();
            var badnd = unitOfWork.BloodBankRepository.Get();
            foreach (var h in badnd)
            {
                BandD.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            var badnd1 = unitOfWork.HospitalRepository.Get();
            foreach (var h in badnd1)
            {
                BandD.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            List<SelectListItem> Hostpitals = new List<SelectListItem>();
            var hostpitals = unitOfWork.HospitalRepository.Get();
            foreach (var h in hostpitals)
            {
                Hostpitals.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            List<SelectListItem> Counties = new List<SelectListItem>();
            var counties = unitOfWork.CountyRepository.Get();
            foreach (var h in counties)
            {
                Counties.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            List<SelectListItem> Localities = new List<SelectListItem>();
            var localities = unitOfWork.LocalityRepository.Get();
            foreach (var h in localities)
            {
                Localities.Add(new SelectListItem
                {
                    Text = h.Name,
                    Value = h.Id.ToString()
                });
            }

            ViewBag.BloodBanksAndHospitals = BandD;
            ViewBag.Counties = Counties;
            ViewBag.Localities = Localities;
            ViewBag.Hospitals = Hostpitals;
            ViewBag.BloodBanks = listItems;
        }

    }
}
