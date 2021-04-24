using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class OrderView
    {
        public int id { get; set; }
        public DateTime order_date { get; set; }
        public int id_customer { get; set; }
        public float total { get; set; }
        public bool status { get; set; }
    }
}