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

        //TO DO: change Hospital with HospitalViewModel
        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(HospitalViewModel hospitalViewModel)
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
