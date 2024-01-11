﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_utility
{
  public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String SecondayDomain { get; set; }
        public int SecondaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
        public String FromEmail { get; set; }
        public String ToEmail { get; set; }
        public String ccEmail { get; set; }
    }
}
