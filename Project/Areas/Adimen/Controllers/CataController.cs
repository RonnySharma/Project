using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Data_Access.Repository;
using Project_Model;
using Project_utility;

namespace Project.Areas.Adimen.Controllers
{
    [Area("Adimen")]
   // [Authorize(Roles =SD.Role_Admin)]
    public class CataController : Controller
    {
        private readonly Iunitofwork _unitofwork;
        public CataController(Iunitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult upsert(int? id)

        {
            catagory catagory = new catagory();
            if(id==null)return View(catagory);//Creat Code
            //Edit
            catagory=_unitofwork.catagory.get(id.GetValueOrDefault());
            if (catagory == null) return NotFound();
            return View(catagory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult upsert(catagory catagory)
        {
            if(catagory == null) return NotFound(); 
            if(!ModelState.IsValid) return View(catagory);
            if (catagory.Id==0)
                _unitofwork.catagory.Add(catagory);
            else
                _unitofwork.catagory.update(catagory);
            _unitofwork.save();
            return RedirectToAction("Index");

        }
        
       
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitofwork.catagory.Getall();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var cat = _unitofwork.catagory.get(id);
            if (cat == null) return Json(new
            {success = false,message = "Something went !!!"
            });
            _unitofwork.catagory.Remove(cat);
            _unitofwork.save();
            return Json(new { success=true, message="Data is Delete suceess!!!" });
        }
         
        #endregion

    }
}


