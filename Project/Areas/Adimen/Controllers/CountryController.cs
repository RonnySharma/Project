//using Microsoft.AspNetCore.Mvc;
//using Project_Data_Access.Repository;
//using Project_Model;

//namespace Project.Areas.Adimen.Controllers
//{
//    [Area("Adimen")]
//    public class CountryController : Controller
//    {
//        private readonly Iunitofwork _unitofwork;
//        public CountryController(Iunitofwork unitofwork)
//        {
//            _unitofwork = unitofwork;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult upsert(int? id)

//        {
//            Country catagory = new Country();
//            if (id == null) return View(catagory);//Creat Code
//            //Edit
//            catagory = _unitofwork.country.get(id.GetValueOrDefault());
//            if (catagory == null) return NotFound();
//            return View(catagory);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult upsert(Country catagory)
//        {
//            if (catagory == null) return NotFound();
//            if (!ModelState.IsValid) return View(catagory);
//            if (catagory.Id == 0)
//                _unitofwork.country.Add(catagory);
//            else
//                _unitofwork.country.update(catagory);
//            _unitofwork.save();
//            return RedirectToAction("Index");

//        }


//        #region APIs
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var categoryList = _unitofwork.country.Getall();
//            return Json(new { data = categoryList });
//        }

//        [HttpDelete]
//        public IActionResult Delete(int id)
//        {
//            var cat = _unitofwork.country.get(id);
//            if (cat == null) return Json(new
//            {
//                success = false,
//                message = "Something went !!!"
//            });
//            _unitofwork.country.Remove(cat);
//            _unitofwork.save();
//            return Json(new { success = true, message = "Data is Delete suceess!!!" });
//        }

//        #endregion

//    }
//}


