using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public interface IcategoryRepostry : IRepository<catagory>
    {
        IEnumerable<catagory> Select(Func<object, catagory> selector);
    }
}
