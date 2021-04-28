using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChocoGear.Areas.Admin.Controllers
{
    public class AdminSiteController : Controller
    {
        // GET: Admin/AdminSite
        public ActionResult Index()
        {
            return View();
        }
        
        // PRODUCT
        public ActionResult Product(string category_name)
        {
            if (Session["login_admin"] != null)
            {
                Models.IRepository<Models.ModelView.CategoryView> category = Models.Dao.CategoryDao.Instance;
                Session["listCate"] = category.Gets();

                Models.IRepository<Models.ModelView.Brand> brand = Models.Dao.BrandDao.Instance;
                Session["listBrand"] = brand.Gets();

                Models.IRepository<Models.ModelView.ProductView> product = Models.Dao.ProductDao.Instance;
                Session["listProduct"] = product.Gets();

                ViewBag.data = Session["listProduct"];

                return View();
            }
            else
            {

            }
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateProduct(HttpPostedFileBase Img)
        {
            var name = Request.Form["Name"];
            var price = 0.0;
            if(Request.Form["Price"] != "")
            {
                price = float.Parse(Request.Form["Price"]);
            }

            var active = false;
            if(Request.Form["Status"] != null)
            {
                if(int.Parse(Request.Form["Status"]) == 1)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
            var image_name = "";
            if (Img != null)
            {
                image_name = Img.FileName;
            }
            

            var id_cate = 0;
            if (Request.Form["Category"] != null)
            {
                id_cate = int.Parse(Request.Form["Category"]);
            }

            var id_brand = 0.0;
            if(Request.Form["Brand"] != null)
            {
                id_brand = int.Parse(Request.Form["Brand"]);
            }
            var description = "";
            if (Session["description"] != null)
            {
                description = Session["description"].ToString();
                Session["description"] = null;
            }
            if (name != "" && price != 0  && image_name != "" && id_cate != 0 && id_brand != 0 && description != "")
            {
                Models.ModelView.ProductView pro = new Models.ModelView.ProductView();
                pro.name_product = name;
                pro.name_image = image_name;
                pro.id_brand = (int)id_brand;
                pro.id_category = id_cate;
                pro.price = (float)price;
                pro.discount = 0;
                pro.status = active;
                pro.description = description;
                pro.created = DateTime.Parse(DateTime.Now.ToString("d"));
                Models.IRepository<Models.ModelView.ProductView> Product = Models.Dao.ProductDao.Instance;
                Product.Create(pro);
                string pathUpload = Server.MapPath("~/Areas/Admin/Upload/") + image_name;
                Img.SaveAs(pathUpload);

                return RedirectToAction("Product");
            }
            else
            {
                return RedirectToAction("Product");
            }
            
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpDescription()
        {
            var description = Request.Form["description"];
            Session["description"] = description;
            return Json("Success");
        }

        public ActionResult Edit()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.IRepository<Models.ModelView.ProductView> Product = Models.Dao.ProductDao.Instance;
            var q = Product.GetId(id);
            Session["EditProduct"] = q;
            return RedirectToAction("Product");
        }

        public ActionResult DeleteProduct()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.IRepository<Models.ModelView.ProductView> Product = Models.Dao.ProductDao.Instance;
            var q = Product.GetId(id);
            string fullPath = Request.MapPath("~/Areas/Admin/Upload/" + q.name_image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            var result = Product.Delete(id);
            if(result == 1)
            {
                return Json("success");
            }
            else
            {
                return Json("fail");
            }
        }


        public ActionResult EditProduct(int id)
        {
            /*var id = int.Parse(Request.Form["id"]);*/
            Models.IRepository<Models.ModelView.ProductView> Product = Models.Dao.ProductDao.Instance;
            Session["inforProduct"] = Product.GetId(id);

            Models.IRepository<Models.ModelView.CategoryView> Category = Models.Dao.CategoryDao.Instance;
            Session["listCate"] = Category.Gets();

            Models.IRepository<Models.ModelView.Brand> Brand = Models.Dao.BrandDao.Instance;
            Session["listBrand"] = Brand.Gets();
            return View();
        }
        [HttpPost]
        public ActionResult Create_Edit()
        {
            if (Request.Files.Count != 0)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    var fileName = Path.GetFileName(file.FileName);
                    var id = int.Parse(Request.Form["id_pro"]);
                    var name = Request.Form["name"];
                    var image_name = Request.Form["Img"];
                    var id_cate = int.Parse(Request.Form["Category"]);
                    var id_brand = int.Parse(Request.Form["Brand"]);
                    var price = float.Parse(Request.Form["Price"]);
                    var decription = Request.Form["Decription"];
                    var discount = float.Parse(Request.Form["Discount"]);
                    var active = Request.Form["Status"].Equals("1") ? true : false;
                    Models.IRepository<Models.ModelView.ProductView> repository = Models.Dao.ProductDao.Instance;
                    Models.ModelView.ProductView pv = new Models.ModelView.ProductView();
                    pv.id = id;
                    pv.name_product = name;
                    pv.name_image = fileName;
                    pv.id_brand = id_brand;
                    pv.id_category = id_cate;
                    pv.price = price;
                    pv.discount = discount;
                    pv.status = active;
                    pv.description = decription;
                    repository.Update(pv);
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Upload/"), fileName);
                    file.SaveAs(path);
                }

            }

            return RedirectToAction("Product");
        }
        //Category
        public ActionResult Category()
        {
            Models.IRepository<Models.ModelView.CategoryView> Category = Models.Dao.CategoryDao.Instance;
            Session["listCate"] = Category.Gets();
            return View();
        }

        public ActionResult CreateCategory()
        {
            var name = Request.Form["Name"];
            var status = Request.Form["Status"];
            Models.IRepository<Models.ModelView.CategoryView> Category = Models.Dao.CategoryDao.Instance;
            Models.ModelView.CategoryView cate = new Models.ModelView.CategoryView();
            cate.name_category = name;
            cate.status = bool.Parse(status);
            Category.Create(cate);
            return RedirectToAction("Category");
        }

        public ActionResult UpdateCategory()
        {
            var id = int.Parse(Request.Form["id"]);
            var name = Request.Form["name"];
            var status = bool.Parse(Request.Form["status"]);
            var nameBefore = Request.Form["nameBefore"];
            Models.ModelView.CategoryView cateV = new Models.ModelView.CategoryView();
            cateV.id = id;
            cateV.name_category = name;
            cateV.status = status;
            Models.IRepository<Models.ModelView.CategoryView> cate = Models.Dao.CategoryDao.Instance;
            var result = cate.Update(cateV);
            if(result == 0)
            {
                return Json("Category exits!!");
            }
            return Json("success");
        }
        public ActionResult DeleteCate()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.IRepository<Models.ModelView.CategoryView> cate = Models.Dao.CategoryDao.Instance;
            cate.Delete(id);
            return Json("Success");
        }

        //FeedBack
        public ActionResult FeedBack()
        {
            Models.IRepository<Models.ModelView.FeedBackView> feedback = Models.Dao.FeedBackDao.Instance;
            Session["listFeedback"] = feedback.Gets();
            return View();
        }

        //Customer
        public ActionResult Customer()
        {
            Models.IRepository<Models.ModelView.CustomerView> customer = Models.Dao.CustomerDao.Instance;
            Session["listCus"] = customer.Gets();
            return View();
        }

        public ActionResult deleteCustomer()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.IRepository<Models.ModelView.CustomerView> customer = Models.Dao.CustomerDao.Instance;
            var result = customer.Delete(id);
            if(result == 1)
            {
                return Json("Delete Success");
            }
            return Json("Delete Fail!");
        }
        //Order
        public ActionResult Order()
        {
            Models.IRepository<Models.ModelView.OrderView> order = Models.Dao.OrderDao.Instance;
            Session["listOrder"] = order.Gets();
            return View();
        }

        public ActionResult ViewOrderDetail()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.Dao.OrderDetailDao orderDetail = Models.Dao.OrderDetailDao.Instance;
            var list = orderDetail.getList(id);
            var table = "";
            foreach(var item in list)
            {
                table += "<table style='width:100%'><tr><th style='width: 30%;'>" + item.name_product+ "</th><th style='width: 10%;'>" + item.price+ "$</th><th style='width:33 %;'>" + item.quantity+"</th><th>"+item.sub_total+"$</th></tr></table>";
            }
            return Json(table);
        }

        //Brand
        public ActionResult Brand()
        {
            Models.IRepository<Models.ModelView.Brand> brand = Models.Dao.BrandDao.Instance;
            Session["listBrands"] = brand.Gets();
            return View();
        }
        public ActionResult CreateBrand()
        {
            var name = Request.Form["name"];
            Models.IRepository<Models.ModelView.Brand> brand = Models.Dao.BrandDao.Instance;
            Models.ModelView.Brand br = new Models.ModelView.Brand();
            br.name = name;
            brand.Create(br);
            return RedirectToAction("Brand");
        }
        public ActionResult UpdateBrand()
        {
            var id = int.Parse(Request.Form["id"]);
            var name = Request.Form["name"];
            Models.ModelView.Brand brandV = new Models.ModelView.Brand();
            brandV.id = id;
            brandV.name = name;
            Models.IRepository<Models.ModelView.Brand> brand = Models.Dao.BrandDao.Instance;
            var result = brand.Update(brandV);
            if (result == 0)
            {
                return Json("Category exits!!");
            }
            return Json("success");
        }
        public ActionResult DeleteBrand()
        {
            var id = int.Parse(Request.Form["id"]);
            Models.IRepository<Models.ModelView.Brand> brand = Models.Dao.BrandDao.Instance;
            var res = brand.Delete(id);
            if(res == 1)
            {
                return Json("Delete Success");
            }
            else
            {
                return Json("Delete Fail");
            }
        }
    }
}