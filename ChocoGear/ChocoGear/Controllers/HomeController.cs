using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocoGear.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }

        public ActionResult FeedBack()
        {
            return View();

        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult LoginAndRegister()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            return View();
        }
    }
}