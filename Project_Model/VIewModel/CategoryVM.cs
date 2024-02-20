using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model.VIewModel
{
   public class CategoryVM
    {
        public IEnumerable<SelectListItem> CatagoriesList { get; set; }
        public catagory Catagory { get; set;}
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public string Term { get; set; }
        public string Orderby { get; set; }

        
    }
}
