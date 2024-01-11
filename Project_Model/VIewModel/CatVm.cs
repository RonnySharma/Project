using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model.VIewModel
{
   public class CatVm
    {
        public IEnumerable<shopingcart> ListCart { get; set; }
        public bool IsblogActive { get; set; }
        public string isblogactivecheck { get; set; }
        public Order Orders { get; set; }
    }
}
