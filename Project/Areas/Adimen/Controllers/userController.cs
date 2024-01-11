using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Project_Data_Access.Data;
using Project_Data_Access.Repository;
using Project_Model;
using Project_utility;
using System.Data;

namespace Project.Areas.Adimen.Controllers
{
    [Area("Adimen")]
 // [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class userController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Iunitofwork _unitofwork;
        public userController(ApplicationDbContext context, Iunitofwork unitofwork)
        {
            _context = context;
            _unitofwork= unitofwork;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpPost]
        public IActionResult LockUnLock([FromBody] string id)
        {
            var lockout = _context.ApplicationUsers.Find(id);
            bool islocked=false;
            if(lockout==null)
                return Json(new {success=false, Message="somethink went worng locking"});
            if (lockout!=null && lockout.LockoutEnd > DateTime.Now   )//unlock
            {
                lockout.LockoutEnd = DateTime.Now;
                islocked = false;
            }
            else {
                lockout.LockoutEnd = DateTime.Now.AddYears(100);
                islocked = true;
            }
            _context.SaveChanges();
            return Json(new { message=islocked==true?"Locked":"UnLocked",success=lockout });
        }

        [HttpGet]
        public IActionResult getall()
        {
            var userList = _context.ApplicationUsers.ToList();
            //aspNetUseers
            var roles = _context.Roles.ToList();
            //aspNetRoles
            var userRoles = _context.UserRoles.ToList();
            //aspnetuserRoles
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(r => r.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitofwork.Company.get(Convert.ToInt32(user.CompanyId)).Name
                    };
                }
                if (user.CompanyId == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);
            return Json(new { data = userList });
          
        }
        #endregion
    }

}
    
