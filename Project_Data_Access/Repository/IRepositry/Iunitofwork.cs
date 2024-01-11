using Project_Data_Access.Repository.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public interface Iunitofwork
    {
        IcategoryRepostry catagory { get; }

        IcoverTypeRepostory Covertype { get; }
        IspCall spCall { get; }
        Iproduct Product { get; }
        IcompanyRepostry Company { get; }
        IApplicationUserRepostry ApplicationUser { get; }
        IOrder Order { get; }
        IOrderDetail OrderDetail { get; }
        IShopingcart Shopingcart { get; }   
        //Icot cot { get; }
        //Icity city { get; }
        //Iciety ciety { get; }
        //Icountry country { get; }
        void save();
    }
}
