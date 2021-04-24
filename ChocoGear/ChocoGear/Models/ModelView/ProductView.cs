using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class ProductView
    {
        public int id { get; set; }
        public string name_product { get; set; }
        public string name_image { get; set; }
        public float price { get; set; }
        public DateTime created { get; set; }
        public float discount { get; set; }
        public string description { get; set; }
        public int id_brand { get; set; }
        public int id_category { get; set; }
        public bool status { get; set; }
    }
}