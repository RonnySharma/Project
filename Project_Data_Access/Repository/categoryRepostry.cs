 using Project_Data_Access.Data;
using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public class categoryRepostry:Repository<catagory>,IcategoryRepostry
    {
        private readonly ApplicationDbContext _context;
public categoryRepostry(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

      

        public IEnumerable<catagory> Select(Func<object, catagory> selector)
        {
            return _context.Categries.Select(selector);
        }
    }
}
