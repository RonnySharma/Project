﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Model
{
   public class OrderDetail
    {
        public int Id { get; set;}
        public int OrderId { get; set;}
        [ForeignKey("OrderId")]
        public Order Order { get; set;}
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

    }
}
