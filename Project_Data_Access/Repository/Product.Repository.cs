//using Project_Data_Access.Data;
using Project_Data_Access.Data;

using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
public class ProductRepository : Repository<Product>,Iproduct
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
