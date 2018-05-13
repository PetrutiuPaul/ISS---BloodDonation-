using BloodDonation.Models;
using DAL.Models;
using DAL.UnitOfWork.Contract;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
        
    }
}
