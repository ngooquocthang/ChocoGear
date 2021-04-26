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
        public ActionResult Product()
        {
            Models.IRepository<Models.ModelView.CategoryView> repository = Models.Dao.CategoryDao.Instance;
            Session["listCate"] = repository.Gets();

            Models.IRepository<Models.ModelView.Brand> repositoryBrand = Models.Dao.BrandDao.Instance;
            Session["listBrand"] = repositoryBrand.Gets();

            Models.IRepository<Models.ModelView.ProductView> repositoryProduct = Models.Dao.ProductDao.Instance;
            Session["listProduct"] = repositoryProduct.Gets();
            var q= repositoryProduct.Gets();
            ViewBag.data = q;

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateProduct(HttpPostedFileBase Img)
        {
            var name = Request.Form["Name"];
            var price = float.Parse(Request.Form["Price"]);
            var active = Request.Form["Status"].Equals("1") ? true : false;
            var image_name = Img.FileName;
            var id_cate = int.Parse(Request.Form["Category"]);
            var discount = float.Parse(Request.Form["Discount"]);
            var id_brand = int.Parse(Request.Form["Brand"]);
            var decription = Request.Form["Decription"];
            Models.ModelView.ProductView pro = new Models.ModelView.ProductView();
            pro.name_product = name;
            pro.name_image = image_name;
            pro.id_brand = id_brand;
            pro.id_category = id_cate;
            pro.price = price;
            pro.discount = discount;
            pro.status = active;
            pro.description = decription;
            pro.created = DateTime.Parse(DateTime.Now.ToString("d"));
            Models.IRepository<Models.ModelView.ProductView> Product = Models.Dao.ProductDao.Instance;
            Product.Create(pro);
            string pathUpload = Server.MapPath("~/Areas/Admin/Upload/") + image_name;
            Img.SaveAs(pathUpload);

            return RedirectToAction("Product");
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
            Product.Delete(id);
            return Json("success");
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
            /*var listCate = Category.Gets();
            var a = "";
            var b = "";
            foreach(var item in listCate)
            {
                if(item.status == true)
                {
                    b += "< input type = 'checkbox' name = 'status' id = 'status' checked />";
                }
                else
                {
                    b += "< input type = 'checkbox' name = 'status' id = 'status' />";
                }
                a += "<table class='tablerow'style='width: 100 % '><tr><td style='max-width:40px;' >"+ item.name_category+"</td>"+ b +"< td style = 'max-width:40px;' >< button type = 'button' class='btn-Del'><img width = '20' src='~/Areas/Admin/Upload/Icon/icondelete.ico' alt='Alternate Text' /></button><button class='btn-Edit'><img width = '20' src='~/Areas/Admin/Upload/Icon/iconedit.ico' alt='Alternate Text' /></button></td></tr></table>";
            }
            *//*if (cate.status == true)
            {
                b += "< input type = 'checkbox' name = 'status' id = 'status' checked />";
            }
            else
            {
                b += "< input type = 'checkbox' name = 'status' id = 'status' />";
            }
            var c = "<table class='tablerow'style='width: 100 % '><tr><td style='max-width:40px;' >" + name + "</td>" + b + "< td style = 'max-width:40px;' >< button type = 'button' class='btn-Del'><img width = '20' src='~/Areas/Admin/Upload/Icon/icondelete.ico' alt='Alternate Text' /></button><button class='btn-Edit'><img width = '20' src='~/Areas/Admin/Upload/Icon/iconedit.ico' alt='Alternate Text' /></button></td></tr></table>";*//*

            return Json(a);*/
        }

        //FeedBack
        public ActionResult FeedBack()
        {
            return View();
        }

        //Customer
        public ActionResult Customer()
        {
            Models.IRepository<Models.ModelView.CustomerView> customer = Models.Dao.CustomerDao.Instance;
            Session["listCus"] = customer.Gets();
            return View();
        }

        //Order
        public ActionResult Order()
        {
            return View();
        }
    }
}