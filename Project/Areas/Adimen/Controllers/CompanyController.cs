using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project_Data_Access.Repository;
using Project_Model;
using Project_utility;

namespace Project.Areas.Adimen.Controllers
{
    [Area("Adimen")]
   // [Authorize(Roles =SD.Role_Admin +","+SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly Iunitofwork _unitofwork;
        public CompanyController(Iunitofwork unitofwork)
        {
               _unitofwork= unitofwork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult upsert(int? id)
        {
            Company company = new Company();
            if (id == null) return View(company);
            company=_unitofwork.Company.get(id.GetValueOrDefault());
            if (company == null) return NotFound();


            return View(company);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult upsert(Company company)
        {
            if (company == null) return NotFound();
            if (!ModelState.IsValid) return View(company);
            if (company.Id == 0)
                _unitofwork.Company.Add(company);
            else
                _unitofwork.Company.update(company);
            _unitofwork.save();
            return RedirectToAction("Index");
        }
            #region APIs
            [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data=_unitofwork.Company.Getall() }); 
        }
        #endregion
    }
}
