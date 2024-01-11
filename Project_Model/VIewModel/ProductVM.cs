using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model.VIewModel
{
  public class ProductVM
    {
       public Product Product { get; set; }
        public IEnumerable<SelectListItem> catalist { get; set; }
        public IEnumerable<SelectListItem> covertypeList { get; set; }
        public IEnumerable<SelectListItem> cityList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }

    }
}
