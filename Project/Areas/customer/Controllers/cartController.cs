using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using NPOI.SS.Formula.Eval;
using Project_Data_Access.Repository;
using Project_Model;
using Project_Model.VIewModel;
using Project_utility;
using Stripe;
using System.ComponentModel.Design;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace Project.Areas.customer.Controllers
{
    [Area("Customer")]

    public class cartController : Controller
    {
        private readonly Iunitofwork _unitofwork;
        private bool isEmailConfirm;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;
       // private readonly Iproduct _product;


        public cartController(Iunitofwork unitofwork,IEmailSender emailSender, UserManager<IdentityUser> userManager/*, Iproduct product*/)
        {
            _unitofwork = unitofwork;
            _emailSender = emailSender;
            _userManager = userManager;
           // _product = product;
        }
        [BindProperty]
     //  public shopingcartVM ShopingcartVM { get; set; }
        
        public CatVm CatVm { get; set; }
        public IActionResult Index()
        {
var claimsIdentiy = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentiy.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                CatVm = new CatVm()
                {
                    IsblogActive= false,
                    isblogactivecheck="Is blog active",
                    ListCart = new List<shopingcart>()
            };
                return View(CatVm);
            }
         CatVm= new CatVm()
            {
                ListCart = _unitofwork.Shopingcart.Getall(sc => sc.ApplicationUserId == claim.Value, includeProperties: "Product"),
                Orders = new Order()
            };
            CatVm.Orders.orderTotal= 0;
            CatVm.Orders.ApplicationUser=_unitofwork.ApplicationUser.FirstOrDefoult(u=>u.Id==claim.Value,includeProprties:"Company");
            foreach(var list in CatVm.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                CatVm.Orders.orderTotal += (list.Count * list.Price);
               
                if(list.Product.Description.Length > 100)
                {
                    list.Product.Description = list.Product.Description.Substring(0, 99) + "...";
                }
            }
            if(!isEmailConfirm)
            {
                ViewBag.EmailMassage = "Email HAs been";
                ViewBag.emailCss = "text-success";
                ViewBag.isEmailConfirm = false;

            }
            else
            {
                ViewBag.EmailMassage = "Email HAs been";
                ViewBag.emailCss = "text-success";
            }
            return View(CatVm);
           
       
        }
        public IActionResult Pluse(int id)
        {
            var cart = _unitofwork.Shopingcart.FirstOrDefoult(sc => sc.Id == id);
           
                cart.Count += 1;
            
           
            _unitofwork.save();
            return RedirectToAction(nameof (Index));

        }

        public IActionResult Delete(int id)
        {
            var cart = _unitofwork.Shopingcart.FirstOrDefoult(sd => sd.Id == id);
            _unitofwork.Shopingcart.Remove(cart);
            _unitofwork.save();
            //session
            var claims = (ClaimsIdentity)User.Identity;
            var claim = claims.FindFirst(ClaimTypes.NameIdentifier);
            if (claim!=null)
            {
                var count = _unitofwork.Shopingcart.Getall(sd => sd.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_cartSessionCount, count);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int id)
        {
            var cart = _unitofwork.Shopingcart.FirstOrDefoult(sc => sc.Id == id);
            if (cart.Count == 1)
                cart.Count = 1;
            else
                cart.Count -= 1;
            _unitofwork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary()
        {
            var claim = (ClaimsIdentity)User.Identity;
            var claims = claim.FindFirst(ClaimTypes.NameIdentifier);
            CatVm = new CatVm()
            {
                ListCart = _unitofwork.Shopingcart.Getall(sd => sd.ApplicationUserId == claims.Value, includeProperties: "Product"),
                Orders = new Order()
            };
            CatVm.Orders.ApplicationUser = _unitofwork.ApplicationUser.FirstOrDefoult(u => u.Id == claims.Value);
            foreach (var list in CatVm.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                CatVm.Orders.orderTotal += (list.Count * list.Price);
                list.Product.Description = SD.ConvertToRawHtml(list.Product.Description);
            }
                CatVm.Orders.Name = CatVm.Orders.ApplicationUser.Name;
                CatVm.Orders.StreetAddress = CatVm.Orders.ApplicationUser.streetAddress;
            
                CatVm.Orders.state = CatVm.Orders.ApplicationUser.state;
                CatVm.Orders.City = CatVm.Orders.ApplicationUser.city;
                CatVm.Orders.PostalCode = CatVm.Orders.ApplicationUser.Postalcode;
                CatVm.Orders.PhoneNumber = CatVm.Orders.ApplicationUser.PhoneNumber;
               
            
            return View(CatVm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost(string StripeToken)
        {
          
           
            var claim = (ClaimsIdentity)User.Identity;
            var claims = claim.FindFirst(ClaimTypes.NameIdentifier);
            CatVm.Orders.ApplicationUser = _unitofwork.ApplicationUser.FirstOrDefoult(u => u.Id == claims.Value);

            CatVm.ListCart = _unitofwork.Shopingcart.Getall(sc => sc.ApplicationUserId == claims.Value, includeProperties: "Product");
            CatVm.Orders.Carrier = "";
            CatVm.Orders.TrackIngName = "";
            CatVm.Orders.TransactionId = "";

            CatVm.Orders.PaymentStatus = SD.PeymentStatusPending;
            CatVm.Orders.OrderStatus = SD.orderStatusPending;
            CatVm.Orders.OrderData = DateTime.Now;
            CatVm.Orders.ApplicationUserId = claims.Value;
            _unitofwork.Order.Add(CatVm.Orders);
            _unitofwork.save();
            foreach (var list in CatVm.ListCart)
            {
                list.Price = SD.GetPriceBasedOnQuantity(list.Count, list.Product.Price, list.Product.Price50, list.Product.Price100);
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = list.ProductId,
                    OrderId = CatVm.Orders.Id,
                    Price = list.Price,
                    Count = list.Count,
                };
                
                CatVm.Orders.orderTotal += (list.Price * list.Count);
               
                _unitofwork.OrderDetail.Add(orderDetail);
                _unitofwork.save();
            }
            _unitofwork.Shopingcart.RemoveRange(CatVm.ListCart);
            _unitofwork.save();


            //session

            HttpContext.Session.SetInt32(SD.Ss_cartSessionCount, 0);
            #region Stripe Process
            if (StripeToken == null)
            {
                CatVm.Orders.PaymentDUeDate = DateTime.Today.AddDays(30);
                CatVm.Orders.PaymentStatus = SD.PeymentStatusDelayPayment;
                CatVm.Orders.OrderStatus = SD.orderStatusApproved;
            }
            else
            {
                //paymrnt proces
                var options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(CatVm.Orders.orderTotal),
                    Currency = "usd",
                    Description = "ordeerId" + CatVm.Orders.Id,
                    Source = StripeToken
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                if(charge.BalanceTransactionId== null)
                {
                    CatVm.Orders.PaymentStatus = SD.PeymentStatusRejected;
                }
                else
                
                {
                    CatVm.Orders.TransactionId=charge.BalanceTransactionId;
                    if(charge.Status.ToLower()== "succeeded")
                    {
                        CatVm.Orders.PaymentStatus = SD.PeymentStatusApproved;
                        CatVm.Orders.OrderStatus= SD.orderStatusApproved;
                        CatVm.Orders.OrderData = DateTime.Now;

                    }
                    _unitofwork.save();
                }
            }
            return RedirectToAction("OrderConfirmation", "cart", new { id = CatVm.Orders.Id });


            #endregion

        }
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var Claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitofwork.ApplicationUser.FirstOrDefoult(u => u.Id == Claim.Value);

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
            pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                user.Email,
                "Your Order is Canl",
                $" Order Number is = {id}");

            return View(id);
        }
    }
    //public async Task<IActionResult> OrderConfirmation(int id)
    //{

    //    var claimIdentity = (ClaimsIdentity)User.Identity;
    //    var Claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
    //    var user = _unitOfwork.ApplicationUser.FirstOrDefault(u => u.Id == Claim.Value);

    //    var userId = await _userManager.GetUserIdAsync(user);
    //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
    //    var callbackUrl = Url.Page(
    //        "/Account/ConfirmEmail",
    //        pageHandler: null,
    //        values: new { userId = userId, code = code },
    //        protocol: Request.Scheme);
    //    await _emailSender.SendEmailAsync(
    //        user.Email,
    //        "Your Order is Confirmed",
    //        $" Order Number is = {id}");
    //    return View(id);

   // }
}
