using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "street Address")]
        public string streetaddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        [Display(Name = "Postal code")]
        public string Postalcode { get; set; }
        [Display (Name ="Phonenumber")]
        public string phoneno { get; set;  }
        [Display (Name ="Is Authorized Company")]
        public bool IsAuthorizedCompany { get; set;  }

    }
}
