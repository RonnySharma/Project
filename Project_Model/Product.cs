using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string publisher { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }

        [Required]
        [Range(0, 1000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(0, 1000)]
        public double Price { get; set; }
        [Required]
        [Range(0, 1000)]
        public double Price50 { get; set; }
        [Required]
        [Range(0, 1000)]
        public double Price100 { get; set; }
        [Display(Name = "ImgeUrl")]
        public string ImgeUrl { get; set; }
        [Required]
        [Display(Name = "catagory")]
        public int catagoryId { get; set; }
        public catagory Catagory { get; set; }
        [Required]
        [Display(Name = "covertype")]
        public int CovertypeId { get; set; }
        public Covertype Covertype { get; set; }
        public int Prdqty { get; set; }
        public bool Active { get; set; }
      
      
     
    }
}
