﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.ModelView
{
    public class CustomerView
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool status { get; set; }

    }
}