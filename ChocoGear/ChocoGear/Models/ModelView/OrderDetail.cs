using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int id_orders { get; set; }
        public int id_product { get; set; }
        public int quantity { get; set; }
        public double sub_total { get; set; }
        public bool status { get; set; }
    }
}