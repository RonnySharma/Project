using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderData { get; set; }
        [Required]
        public DateTime ShippingDAte { get; set; }
        [Required]
        public double orderTotal { get; set; }
        public string TrackIngName { get; set; }
        public string Carrier { get; set; }
        public string OrderStatus { get; set; }
      //  public string Cancelled { get; set; }
    
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDUeDate { get; set; }
        public string TransactionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City {get;set;}
        public string state{get;set;}
        [Display(Name ="Postal code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
