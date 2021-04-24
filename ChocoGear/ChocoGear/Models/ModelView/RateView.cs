using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class RateView
    {
        public int id { get; set; }
        public int id_Customer { get; set; }
        public int star { get; set; }
        public string content { get; set; }
        public DateTime created { get; set; }
        public bool status { get; set; }
    }
}