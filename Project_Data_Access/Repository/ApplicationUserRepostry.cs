﻿using Project_Data_Access.Data;
using Project_Data_Access.Repository.IRepositry;
using Project_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
   public class ApplicationUserRepostry:Repository<ApplicationUser>,IApplicationUserRepostry
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepostry(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
