using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project_Data_Access.Data;
using Project_Data_Access.Repository.IRepositry;
using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
     public class Unitofwork:Iunitofwork
    {
        private readonly ApplicationDbContext _context;
      public Unitofwork(ApplicationDbContext context)
        {
            _context = context;
            catagory = new categoryRepostry(_context);
            Covertype = new CovertypeRepository(_context);
            spCall = new spcall(_context);
            Product= new ProductRepository(_context);
            Company= new CompanyRepository(_context);
            ApplicationUser= new ApplicationUserRepostry(_context);
            Order = new ORderRepostery(_context);
            OrderDetail = new orderDetailsRepostary(_context);
            Shopingcart = new Shopingcart(_context);
            //cot = new cotRepostery(_context);
            //city = new Cityrepository(_context);
            //ciety = new cietyRepostry(_context);
            //country = new countryRepostry(_context);

        }
        public IcategoryRepostry catagory { get; private set; }
        public IcoverTypeRepostory Covertype { get; private set; }
       
        public IcompanyRepostry Company { get; private set; }
        public IspCall spCall { get; private set; }

        public Iproduct Product { get; private set; }
        public IApplicationUserRepostry ApplicationUser { get; private set; }

        public IOrder Order { get; private set; }
        public IOrderDetail OrderDetail { get; private set; }
        public IShopingcart Shopingcart { get; private set; }

        //public Iciety ciety { get; private set; }

        //public Icountry country { get; private set; }

        //public Icot cot { get; private set; }

        //public Icity city { get; private set; }



        public void save()
        {
            _context.SaveChanges();
        }
    }
}
