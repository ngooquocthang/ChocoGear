using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class CartView
    {
        public int id { get; set; }
        public string product { get; set; }
        public string image { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
        public float subtotal { get; set; }

    }
}