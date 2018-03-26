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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

       
        
        // POST: Admin/Create
        [HttpPost]
        public ActionResult CreateHospital(HospitalViewModel hospitalViewModel)
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

                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }


       

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult EditHospital(HospitalViewModel hospitalViewModel)
        {
            try
            {
                Hospital hospital = new Hospital()
                {
                    Name = hospitalViewModel.Name,
                    BloodBank_Id = hospitalViewModel.BloodBank_Id

                };
                unitOfWork.HospitalRepository.Update(hospital);
                unitOfWork.Save();
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete() //(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(HospitalViewModel hospitalViewModel)
        {
            try
            {
                Hospital hospital = new Hospital()
                {
                    Name = hospitalViewModel.Name,
                    BloodBank_Id = hospitalViewModel.BloodBank_Id

                };
                unitOfWork.HospitalRepository.Delete(hospital);
                unitOfWork.Save();
               

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*public ActionResult CreateBloodBank()
       {
           return View();
       }
       */

        /// <summary>
        /// //////////////////
        /// </summary>
        /// <param name="bloodBankViewModel"></param>
        /// <returns></returns>
        //Create  BloodBank
        /*  public ActionResult CreateBloodBank(BloodBankViewModel bloodBankViewModel)
          {
              try
              {
                  BloodBank bloodB = new BloodBank()
                  {
                      Name = bloodBankViewModel.Name,
                      County_Id = bloodBankViewModel.County_Id
                  };

                  unitOfWork.BloodBankRepository.Insert(bloodB);
                  unitOfWork.Save();

                  return RedirectToAction("CreateA");
              }
              catch
              {
                  return View();
              }
          }*/
    }
}
