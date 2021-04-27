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
            if (Session["Cart"] == null)
            {
                List<Models.ModelView.CartView> Cart = new List<Models.ModelView.CartView>();
                Session["Cart"] = Cart;
            }
            return View();
        }
        public ActionResult CountCart()
        {
            if (Session["Cart"] != null)
            {
                var cart = (List<ChocoGear.Models.ModelView.CartView>)Session["Cart"];
                var count = cart.Count().ToString();
                return Json(count);
            }
            else
            {
                return Json("0");
            }
        }
        public ActionResult Product(string cate)
        {
            Models.Dao.ProductDao product = Models.Dao.ProductDao.Instance;

            if (Session["Cart"] == null)
            {
                List<Models.ModelView.CartView> Cart = new List<Models.ModelView.CartView>();
                Session["Cart"] = Cart;
            }
            Session["listProduct"] = product.Search_characters_Category(cate);
            Session["NameCate"] = cate;
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
            if (Session["inforCus"] == null)
            {
                return RedirectToAction("LoginAndRegister");
            }
            Models.IRepository<Models.ModelView.CustomerView> repository = Models.Dao.CustomerDao.Instance;
            var q = repository.Gets();
            ViewBag.list_product = q;
            return View();
        }

        public ActionResult Create_ChangePassword()
        {
            var id = int.Parse(Request.Form["id_cus"]);
            var username = Request.Form["username"];
            var password_old = Request.Form["pass_old"];
            var password_new = Request.Form["pass_new"];
            var password_new_cf = Request.Form["pass_new_cf"];
            Models.Dao.Security s = new Models.Dao.Security();
            var tmp_old = username + password_old;
            var tmp_old1 = s.Base64(tmp_old);
            var tmp_old2 = s.MD5Hash(tmp_old1);

            var tmp_new = username + password_new;
            var tmp_new1 = s.Base64(tmp_new);
            var tmp_new2 = s.MD5Hash(tmp_new1);

            Models.Entity.ChocoGearEntities db = new Models.Entity.ChocoGearEntities();
            if (password_old == "" || password_new == "" || password_new_cf == "")
            {
                var a = "Please fill in your PassWord";
                return Json(a);
            }
            else
            {
                if (tmp_old2 == tmp_new2)
                {
                    return Json("Password already exists");
                }
                else
                {
                    if (s.CheckPass(tmp_old2))
                    {
                        var q = db.Customers.Single(d => d.id == id);
                        q.password = tmp_new2;
                        db.SaveChanges();
                        return Json("Success");
                    }
                    else
                    {
                        return Json("PassWord Old Wrong");
                    }
                }

            }
        }

        [HttpPost]
        public ActionResult Create_Login()
        {
            Models.Dao.Security s = new Models.Dao.Security();
            var user = Request.Form["user"];
            var pass = Request.Form["pass"];
            var tmp = user + pass;
            var tmp1 = s.Base64(tmp);
            var tmp2 = s.MD5Hash(tmp1);
            var check = s.CheckLogin(user, tmp2);
            if (check == true)
            {
                Models.Dao.CustomerDao cus = Models.Dao.CustomerDao.Instance;
                Session["inforCus"] = cus.GetCus(user);
                Session["login"] = user;
                return Json("Login Success");
            }
            else
            {
                return Json("Login Fail");
            }
        }

        [HttpPost]

        public ActionResult Create_Register()
        {
            Models.Dao.Security s = new Models.Dao.Security();
            var firstname = Request.Form["first_name"];
            var lastname = Request.Form["last_name"];
            var phone = Request.Form["phone"];
            var email = Request.Form["email"];
            var address = Request.Form["address"];
            var user = Request.Form["username"];
            var pass = Request.Form["password"];
            var status = true;
            var tmp = user + pass;
            var tmp1 = s.Base64(tmp);
            var tmp2 = s.MD5Hash(tmp1);
            if (s.Check_email(email))
            {
                return Json("Email was availabled");
            }
            else if (s.Check_username(user))
            {
                return Json("Username was availabled");
            }
            else
            {
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
                return Json("Success");
            }
        }

        public ActionResult create_update_customer()
        {
            var id = int.Parse(Request.Form["id_customer"]);
            var first_name = Request.Form["first_name"];
            var last_name = Request.Form["last_name"];
            var phone = Request.Form["phone"];
            var email = Request.Form["email"];
            var address = Request.Form["address"];
            var password = Request.Form["password"];
            Models.ModelView.CustomerView cv = new Models.ModelView.CustomerView();
            cv.id = id;
            cv.first_name = first_name;
            cv.last_name = last_name;
            cv.phone = phone;
            cv.email = email;
            cv.address = address;
            cv.password = password;
            Models.IRepository<Models.ModelView.CustomerView> repository = Models.Dao.CustomerDao.Instance;
            repository.Update(cv);
            var q = repository.GetId(id);
            Session["inforCus"] = q;
            return Json(q);
        }


        public ActionResult Forget_Password()
        {
            Models.Dao.Security s = new Models.Dao.Security();
            Models.Entity.ChocoGearEntities db = new Models.Entity.ChocoGearEntities();

            var username = Request.Form["user"];
            var email = Request.Form["email"];
            var string_default = "aptechloveindia";
            var tmp = username + string_default;
            var tmp1 = s.Base64(tmp);
            var tmp2 = s.MD5Hash(tmp1);
            if (s.Check_email_username(email, username))
            {
                var q = db.Customers.Single(d => d.email == email && d.username == username);
                q.password = tmp2;
                db.SaveChanges();

                //Send Mail
                var senderEmail = new MailAddress("tranphuoc4511@gmail.com", "ChocoGear");
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Cf123456789";
                var sub = "Order Gear";
                var body = string_default;
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
            else
            {
                return Json("Email, Username are not available");
            }
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
            if (Session["inforCus"] == null)
            {
                return RedirectToAction("LoginAndRegister");
            }
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
        public ActionResult Delete_Sesstion()
        {
            Session["inforCus"] = null;
            return Json("Success");
        }
    }
}