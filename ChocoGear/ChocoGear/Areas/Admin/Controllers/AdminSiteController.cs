using System;
using System.Collections.Generic;
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

            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateProduct(HttpPostedFileBase Img)
        {
            var name = Request.Form["Name"];
            var price = float.Parse(Request.Form["Price"]);
            var active = Request.Form["Active"].Equals("on") ? true : false;
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
    }
}