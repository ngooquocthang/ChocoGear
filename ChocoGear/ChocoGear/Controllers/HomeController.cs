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
        [HttpPost]
        public ActionResult Create_Login()
        {
            Models.Dao.Security s = new Models.Dao.Security();
            var user = Request.Form["usrname"];
            var pass = Request.Form["password"];
            var tmp = user + pass;
            var tmp1 = s.Base64(tmp);
            var tmp2 = s.MD5Hash(tmp1);
            var check = s.CheckLogin(user,tmp2);
            if (check==true)
            {
                Session["login"] = user;
                return RedirectToAction("Product");
            }
            return View();
        }

        [HttpPost]

        public ActionResult Create_Register()
        {
            Models.Dao.Security s = new Models.Dao.Security();
            var firstname = Request.Form["firstname"];
            var lastname = Request.Form["lastname"];
            var phone = Request.Form["phone"];
            var email = Request.Form["email"];
            var address = Request.Form["address"];
            var user = Request.Form["usrname"];
            var pass = Request.Form["password"];
            var cfpass = Request.Form["cfpassword"];
            if (pass!=cfpass)
            {
                return RedirectToAction("LoginAndRegister");
            }
            var status = true;
            var tmp = user + pass;
            var tmp1 = s.Base64(tmp);
            var tmp2 = s.MD5Hash(tmp1);
            Models.ModelView.CustomerView cv = new Models.ModelView.CustomerView();
            Models.IRepository<Models.ModelView.CustomerView> repository = Models.Dao.CustomerDao.Instance;
            cv.first_name = firstname;
            cv.last_name = lastname;
            cv.phone = phone;
            cv.email = email;
            cv.address = address;
            cv.username = user;
            cv.password = tmp2;
            cv.status = status;
            repository.Create(cv);
                return RedirectToAction("LoginAndRegister");

        }
    }
}