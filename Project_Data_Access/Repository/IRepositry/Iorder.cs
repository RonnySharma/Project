using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository.IRepositry
{
    public interface IOrder : IRepository<Order>
    {
        // IEnumerable<Order> Include();
       // IEnumerable<Order> where(Func<object, object> value);
    }
}
