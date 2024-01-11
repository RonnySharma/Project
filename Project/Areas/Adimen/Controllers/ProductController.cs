using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Project_Data_Access.Repository;
using Project_Model;
using Project_Model.VIewModel;
using Project_utility;
using System.Data;

namespace Project.Areas.Adimen.Controllers
{
    [Area("Adimen")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly Iunitofwork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(Iunitofwork unitowork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitowork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),

                catalist = _unitofwork.catagory.Getall().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString()
                }),
                covertypeList = _unitofwork.Covertype.Getall().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                }
             ),
              
            };
            if (id == null) return View(productVM);
            productVM.Product = _unitofwork.Product.get(id.GetValueOrDefault());
            return View(productVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult upsert(ProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0)
                {
                    var filename = Guid.NewGuid().ToString();

                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webRootPath, @"Images\Products");
                    if (productVM.Product.Id != 0)
                    {
                        var ImagerExists = _unitofwork.Product.get(productVM.Product.Id).ImgeUrl;
                        productVM.Product.ImgeUrl = ImagerExists;
                    }
                    if (productVM.Product.ImgeUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImgeUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    productVM.Product.ImgeUrl = @"\Images\Products\" + filename + extension;
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var imageexists = _unitofwork.Product.get(productVM.Product.Id).ImgeUrl;
                        productVM.Product.ImgeUrl = imageexists;
                    }
                }
                if (productVM.Product.Id == 0)
                   

                    _unitofwork.Product.Add(productVM.Product);
                else
                    _unitofwork.Product.update(productVM.Product);
                _unitofwork.save();
                return RedirectToAction("Index");

            }   
            else
            {
                productVM = new ProductVM()
                {
                    Product = new Product(),

                    catalist = _unitofwork.catagory.Getall().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    covertypeList = _unitofwork.Covertype.Getall().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    }
             )
                };
                if (productVM.Product.Id != 0)
                {
                    productVM.Product = _unitofwork.Product.get(productVM.Product.Id);
                }
                return View(productVM);

            }

        
    }
    #region APIs
    [HttpGet]
    public IActionResult getall()
    {
        var productlist = _unitofwork.Product.Getall();
            return Json(new { data = productlist });
    }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var cat = _unitofwork.Product.get(id);
            if (cat == null) return Json(new
            {
                success = false,
                message = "Something went !!!"
            });
            var wabRootPath = _webHostEnvironment.WebRootPath;
            var imgePath = Path.Combine(wabRootPath, cat.ImgeUrl.Trim('\\'));
            if(System.IO.File.Exists(imgePath))
            {
                System.IO.File.Exists(imgePath);
            }
            _unitofwork.Product.Remove(cat);
            _unitofwork.save();
            return Json(new
            {
                success = true,
                Message = "Finally DATA delete"
            });
        }

        
        }
    #endregion
}
