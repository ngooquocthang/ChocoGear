using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            Models.IRepository<Models.ModelView.ProductView> repository = Models.Dao.ProductDao.Instance;
            Models.IRepository<Models.ModelView.CategoryView> repositorys = Models.Dao.CategoryDao.Instance;

            if (Session["Cart"] == null)
            {
                List<Models.ModelView.CartView> Cart = new List<Models.ModelView.CartView>();
                Session["Cart"] = Cart;
            }

            var s = repositorys.Gets();
            ViewBag.listcate = s;
            var q = repository.Gets();
            ViewBag.listproduct = q;
            return View();
        }

        public ActionResult FeedBack()
        {
            Models.IRepository<Models.ModelView.CustomerView> repository = Models.Dao.CustomerDao.Instance;
            var q=repository.Gets();
            ViewBag.list_product = q;
            return View();
        }
        public ActionResult CreateFeedBack()
        {
            var email = Request.Form["email"];
            var content = Request.Form["content"];
            var date = DateTime.Parse(DateTime.Now.ToString("d"));
            var status = true;
            Models.ModelView.FeedBackView fv = new Models.ModelView.FeedBackView();
            fv.email = email;
            fv.content = content;
            fv.created = date;
            fv.status = status;
            Models.IRepository<Models.ModelView.FeedBackView> repository = Models.Dao.FeedBackDao.Instance;
            repository.Create(fv);

            //Send Mail
            var senderEmail = new MailAddress("tranphuoc4511@gmail.com", "ChocoGear");
            var receiverEmail = new MailAddress(fv.email, "Receiver");
            var password = "Cf123456789";
            var sub = "Order Gear";
            var body = "We will try to make our services better and better. Thanks for your feedback ^-^ \n (NO REPLY !!)";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }

            return Json("Success");
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
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            Models.IRepository<Models.ModelView.CustomerView> repository = Models.Dao.CustomerDao.Instance;
            var q = repository.Gets();
            ViewBag.list_product = q;
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
            var check = s.CheckLogin(user, tmp2);
            if (check == true)
            {
                Models.Dao.CustomerDao cus = Models.Dao.CustomerDao.Instance;
                Session["inforCus"] = cus.GetCus(user);
                Session["login"] = user;
                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("LoginAndRegister");
            }
            
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
            if (pass != cfpass)
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

        public ActionResult AddToCart(int id)
        {
            List<Models.ModelView.ProductView> pro = new List<Models.ModelView.ProductView>();
            Models.IRepository<Models.ModelView.ProductView> product = Models.Dao.ProductDao.Instance;
            var q = product.GetId(id);
            if (Session["Cart"] == null)
            {
                List<Models.ModelView.CartView> Cart = new List<Models.ModelView.CartView>();
                Session["Cart"] = Cart;
            }
            var cart = (List<Models.ModelView.CartView>)Session["Cart"];
            var count = 0;
            foreach(var item in cart)
            {

                if (q.name_product == item.product)
                {
                    item.quantity += 1;
                    item.subtotal = (item.quantity * item.price);
                    count++;
                    return RedirectToAction("checkout");
                }
            }
            if(count == 0)
            {
                cart.Add(new Models.ModelView.CartView { product = q.name_product, image = q.name_image, price = q.price, quantity = 1, subtotal = (q.price * 1) });
                Session["Cart"] = cart;
            }

            return RedirectToAction("Cart");
        }

        // INSCREASE QUANTITY
        public ActionResult InscreaseQuantity()
        {
            var name = Request.Form["name"];
            var cart = (List<Models.ModelView.CartView>)Session["Cart"];
            var q = cart.Where(d => d.product == name).FirstOrDefault();
            q.quantity += 1;
            q.subtotal = (q.quantity * q.price);
            Session["Cart"] = cart;
            var total = 0.0;
            foreach (var item in cart)
            {
                total += item.subtotal;
            }
            return Json(total);
        }

        public ActionResult DescreaseQuantity()
        {
            var name = Request.Form["name"];
            var cart = (List<Models.ModelView.CartView>)Session["Cart"];
            var q = cart.Where(d => d.product == name).FirstOrDefault();
            q.quantity -= 1;
            q.subtotal = (q.quantity * q.price);
            Session["Cart"] = cart;
            var total = 0.0;
            foreach (var item in cart)
            {
                total += item.subtotal;
            }
            return Json(total);
        }

        public ActionResult DeleteCart()
        {
            var name = Request.Form["name"];
            var cart = (List<Models.ModelView.CartView>)Session["Cart"];
            var q = cart.Where(d => d.product == name).FirstOrDefault();
            cart.Remove(q);
            var count = cart.Count;
            return Json(count);
        }

        public ActionResult ProductDetail(int id)
        {
            Models.IRepository<Models.ModelView.ProductView> repository = Models.Dao.ProductDao.Instance;
            var q = repository.GetId(id);
            var q1 = repository.Gets();
            ViewBag.listproduct = q;
            ViewBag.listproductall = q1;
            return View();
        }

        public ActionResult GetQuantityInCart()
        {
            var count = 0;
            List<string> nam = new List<string>();
            if (Session["Cart"] != null)
            {
                var cart = (List<Models.ModelView.CartView>)Session["Cart"];
                count += cart.Count;
                foreach (var item in cart)
                {
                    nam.Add(item.product);
                }
                nam.Add(count.ToString());
                return Json(nam);
            }
            return Json(count);
        }

        public ActionResult CustomerInfor()
        {
            return View();
        }

        public ActionResult DeleteAll()
        {
            Session["Cart"] = null;
            return Json("Success");
        }
        
        public ActionResult checkCart()
        {
            var count = 0;
            if(Session["Cart"] != null)
            {
                var cart = (List<Models.ModelView.CartView>)Session["Cart"];
                count += cart.Count;
            }
            return Json(count);
        }
        public ActionResult Order()
        {
            if (Session["inforCus"] != null)
            {
                if (Session["Cart"] != null)
                {
                    var listcart = (List<ChocoGear.Models.ModelView.CartView>)Session["Cart"];
                    var cus = (ChocoGear.Models.ModelView.CustomerView)Session["inforCus"];
                    Models.IRepository<Models.ModelView.OrderView> order = Models.Dao.OrderDao.Instance;
                    Models.IRepository<Models.ModelView.OrderDetail> orderD = Models.Dao.OrderDetailDao.Instance;
                    var email = Request.Form["email"];
                    var phone = Request.Form["phone"];
                    var address = Request.Form["address"];
                    var total = float.Parse(Request.Form["total"]);
                    if (email != "" && phone != "" && address != "")
                    {
                        Models.ModelView.OrderView orderview = new Models.ModelView.OrderView();
                        orderview.email_order = email;
                        orderview.address_order = address;
                        orderview.phone_order = phone;
                        orderview.id_customer = cus.id;
                        orderview.order_date = DateTime.Parse(DateTime.Now.ToString("d"));
                        orderview.total = total;
                        orderview.status = false;
                        var id = order.Create(orderview);

                        foreach (var cart in listcart)
                        {
                            Models.ModelView.OrderDetail orderDview = new Models.ModelView.OrderDetail();
                            orderDview.id_orders = id;
                            Models.Dao.ProductDao pro = Models.Dao.ProductDao.Instance;
                            orderDview.id_product = pro.GetIdName(cart.product);
                            orderDview.quantity = cart.quantity;
                            orderDview.sub_total = cart.subtotal;
                            orderDview.status = false;
                            orderD.Create(orderDview);
                        }
                        Session["Cart"] = null;
                        return Json("Order Success! Thank you so much <3 ");
                    }
                    return Json("Order Fail!");
                }
            }
            return Json("Order Fail!");
        }
        public ActionResult FogetPass()
        {
            return View();
        }
    }
}