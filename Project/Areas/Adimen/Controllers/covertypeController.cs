using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Data_Access.Repository;
using Project_Model;
using Project_utility;
using System.Data;

namespace Project.Areas.Adimen.Controllers
{
    [Area("Adimen")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class covertypeController : Controller
    {
        private readonly Iunitofwork _unitfowork;
        public covertypeController(Iunitofwork unitfowork)
        {
            _unitfowork = unitfowork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Covertype covertype = new Covertype();
            if(id==null) return View(covertype);
            covertype = _unitfowork.Covertype.get(
                id.GetValueOrDefault());
            //var param.add("@id",id getvalu)
            if (covertype == null) return NotFound();
            return View(covertype); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
public IActionResult upsert(Covertype covertype)
        {
            if(covertype == null) return NotFound();
            if(!ModelState.IsValid) return View(covertype);
            var ms = new DynamicParameters();
            ms.Add("@Name", covertype.Name);
            if (covertype.Id == 0)

                //_unitfowork.Covertype.Add(covertype);
                _unitfowork.spCall.excecute(SD.proc_CreateCoverType, ms );
            else

            //  _unitfowork.Covertype.update(covertype);
            {
                ms.Add("@id", covertype.Id);
                _unitfowork.spCall.excecute(SD.proc_updateCoverType, ms);
                _unitfowork.save();
            }
                
                return RedirectToAction("Index");
            
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitfowork.spCall.List<Covertype>(SD.proc_getcovertype) });
          // return Json(new { data = _unitfowork.Covertype.Getall() });
        }
        [HttpDelete]
        public IActionResult Delete( int id)
        {
            var parm = new DynamicParameters();
            parm.Add("@id", id);
            var coer = _unitfowork.spCall.oneRecord<Covertype>(SD.proc_getcovertypes, parm);
          //  var cat = _unitfowork.Covertype.get(id);
            if (parm == null) return Json(new
            {
                 success = false,
                message = "Something went !!!"
            });
            //   _unitfowork.Covertype.Remove(cat);
            //_unitfowork.save();
            _unitfowork.spCall.excecute(SD.proc_DeleteCoverTypes, parm);

            return Json(new { success = true, message = "data is delete sucaee" });

        }
        #endregion
    }
}