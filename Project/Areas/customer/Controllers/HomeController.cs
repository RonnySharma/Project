using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NPOI.SS.Formula.Functions;
using Project_Data_Access.Data;
using Project_Data_Access.Repository;
using Project_Model;
using Project_Model.VIewModel;
using Project_utility;
using Stripe.Treasury;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using static NPOI.POIFS.Crypt.CryptoFunctions;

namespace Project.Areas.customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Iunitofwork _unitofwork;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, Iunitofwork unitofwork,IEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _unitofwork = unitofwork;
            _context = context;
            _emailSender = emailSender;
        }
        //public ActionResult Index()
        //{

        //    IEnumerable<HotelAvailability> hotels = db.HotelAvailabilities.Include(c => c.Hotel);

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        hotels = db.HotelAvailabilities.Include(c => c.Hotel).Where(
        //                   s => s.RoomType.Contains(searchString)
        //                       || s.AvailabilityDate.Equals(searchString)
        //                       || s.Hotel.HotelName.Contains(searchString));

        //        return View(hotels);
        //    }
        //}
        //[HttpGet]
        //public IActionResult DropDowen(DateTime startDate, DateTime endDate)
        //{
        //    //var filteredOrders = _dbContext.Orders
        //    //    .Where(order => order.OrderDate >= startDate && order.OrderDate <= endDate)
        //    //    .ToList();
        //    return View();

        //}
        //[HttpGet]
        //public IActionResult dropdowen()
        //{
        //var filteredOrders = _dbContext.Orders
        //    .Where(order => order.OrderDate >= startDate && order.OrderDate <= endDate)
        //    .ToList();
        // Get a list of countries and cities
        //ProductVM productVM = new ProductVM()
        //{
        //    Product = new Product(),


        //    cityList = _unitofwork.ciety.Getall().Select(d => new SelectListItem()
        //    {
        //                Text = d.Name,
        //                Value = d.Id.ToString()
        //            }
        //             ),
        //            CountryList = _unitofwork.country.Getall().Select(d => new SelectListItem()
        //            {
        //                Text = d.Name,
        //                Value = d.Id.ToString()
        //            }
        //             ),
        //        };

        //        return View(productVM);


        //}

        //var fdd= _unitofwork.Product.FirstOrDefoult(c=>c.Country.Id==aa && c.City.Id==aa);
        //var filteredOrders = _dbContext.Orders
        //    .Where(order => order.OrderDate >= startDate && order.OrderDate <= endDate)
        //    .ToList();
        // Get a list of countries and cities






        public IActionResult FilterOrders(DateTime startDate, DateTime endDate)
        {
            //var filteredOrders = _dbContext.Orders
            //    .Where(order => order.OrderDate >= startDate && order.OrderDate <= endDate)
            //    .ToList();

            var mohit=_unitofwork.Order.Getall(o=>o.OrderData>=startDate && o.OrderData<=endDate).ToList();
            return View(mohit);
        }


        [HttpGet]
        public IActionResult  Allorder(DateTime? searchdate)
        {
            var the = _unitofwork.Order.Getall();
          
            if (searchdate.HasValue)
            {

                var thet = _unitofwork.Order.Getall(p => p.OrderData.Date == searchdate.Value.Date).ToList();
                return View(thet);
            }

           
            return View(the);
        }

        // [HttpGet]
        //public IActionResult Cancel(int Id)
        //{
        //  var  Order  = _unitofwork.Order.Getall();
         [HttpPost]
        public IActionResult Rodo(int year ,int month)
        {
            //var topSellingProducts = _context.Shopingcarts.OrderByDescending(p => p.Count).Take(10).ToList();
            //     return View(topSellingProducts);
            if (year < 1 || month < 1 || month > 12)
            {
                throw new ArgumentException("Invalid year or month values.");
            }

            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            
            var popularProducts = _context.Orders
                .Where(p => p.OrderData >= firstDayOfMonth && p.OrderData <= lastDayOfMonth)
                .OrderByDescending(p => p.orderTotal)
                .Take(5)
                .ToList();

            ViewBag.SelectedMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            ViewBag.SelectedYear = year;
            return View(popularProducts);

            //var topSellingProducts = _context.Orders
            //.Where(p => p.OrderData.Date == orderDate.Date)
            //.OrderByDescending(p => p.orderTotal)
            //.Take(3)
            //.ToList();

            //ViewBag.SelectedDate = orderDate.Date; // Pass the selected date to the view
            //return View(topSellingProducts);
            //var a = _unitofwork.Shopingcart.Getall(a=>a.ApplicationUserId==id);
            //// Consider orders from the last month
            //var startDate = DateTime.Now.AddMonths(-1);

            //// Retrieve orders for the specified user within the last month
            //var orders = _unitofwork.Order
            //    .Getall(o => o.ApplicationUserId == id && o.OrderData >= startDate)
            //    .ToList();

            //// Pass the orders to the view
            //return View(orders);
        }
        //[HttpGet]
        //public IActionResult Rodo(int id)
        //{

        //    return View();
        //}





        //    return View(Order);
        //}
        // [HttpPost]
        public IActionResult Cancel(int id)
        {
            var order = _unitofwork.Order.Getall();

            if (order != null)
            {
                var ee = _unitofwork.Order.FirstOrDefoult(o=>o.Id==id);
                var user = _unitofwork.ApplicationUser.FirstOrDefoult(u => u.Id == ee.ApplicationUserId);
                ee.OrderStatus = "Cancelled";
                _unitofwork.save();
              //  _emailSender.SendEmailAsync()
                //var emailService = new EmailSender();
                    var emailContent = "Your order has been cancelled.";
                 _emailSender.SendEmailAsync(user.Email,"Order Canceled",emailContent);
                return View(new List<Project_Model.Order> { ee });
            }
            return View(order);
        }
        public IActionResult Allcencel()
        {
            var order = _unitofwork.Order.Getall();
            return View(order);
        }
        //[HttpPost]
        //public IActionResult Cancel(Order order)
        //{
        // var   or = _unitofwork.Order.Getall();

        //    if (or != null)
        //    {
        //        var hh = _unitofwork.Order.FirstOrDefoult(o => o.OrderStatus == "pending");
        //      hh.OrderStatus = "Cancelled";
        //        _unitofwork.save();

        //        return View(hh);
        //    }
        //    return View(order);
        //}
        public IActionResult Details(int id)
        { 
            var dft=_unitofwork.Order.FirstOrDefoult(i=>i.Id==id);
            if (dft == null) return NotFound();
            return View(dft);
        }
        public IActionResult Index(string serchChanger)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitofwork.Shopingcart.Getall(sc => sc.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_cartSessionCount, count);
            }
            var parduct = _unitofwork.Product.Getall(includeProperties: "Catagory,Covertype");

            if (serchChanger != null)
            {

                var prir = _unitofwork.Product.Getall(X => X.Author.Contains(serchChanger)).ToList();
                //  var prir =_unitofwork.Product.Getall(r=>r.Author.Contains(serchChanger)).ToList();
                if (prir.Count == 0)
                {
                    ViewBag.Msg = "data not found";
                    return View(prir);
                }
                else if (prir.Count > 0)
                {
                    // if(serchChanger.ToLower())
                    ViewBag.Msg = "data not found";
                    return View(prir);


                }


                else
                {
                    return View(prir);
                }

            }

            var productlist = _unitofwork.Product.Getall();
            //if (parduct.All(x => x != null))
           // {
                var p = _unitofwork.Product.Getall(t => t.Active == false).ToList();
                return View(p);


                
           // }
           // return View(productlist);
        }
        public IActionResult Detail(int id)
        {
            var productit = _unitofwork.Product.FirstOrDefoult(p => p.Id == id, includeProprties: "Catagory,Covertype");
            if (productit == null) return NotFound();
            var shoppingCart = new shopingcart()
            {
                Product = productit,
                ProductId = productit.Id
            };

            return View(shoppingCart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Detail(shopingcart shopingcart)
        {
            
            shopingcart.Id = 0;
            if (!ModelState.IsValid)
            {
                var clamsid = (ClaimsIdentity)User.Identity;
                var claim = clamsid.FindFirst(ClaimTypes.NameIdentifier);

                shopingcart.ApplicationUserId = claim.Value;
                var shoopingcartdb = _unitofwork.Shopingcart.FirstOrDefoult
                    (sc => sc.ApplicationUserId == claim.Value && sc.ProductId == shopingcart.ProductId);
                if (shoopingcartdb == null) _unitofwork.Shopingcart.Add(shopingcart);
                else
                    shoopingcartdb.Count += shopingcart.Count;
                _unitofwork.save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productit = _unitofwork.Product.FirstOrDefoult(p => p.Id == shopingcart.Product.Id, includeProprties: "Catagory,Covertype");
                if (productit == null) return NotFound();
                var shoppingCart = new shopingcart()
                {
                    Product = productit,
                    ProductId = productit.Id
                };

                return View(shoppingCart);

            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}