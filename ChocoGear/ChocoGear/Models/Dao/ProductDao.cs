using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class ProductDao : IRepository<Models.ModelView.ProductView>
    {
        Models.Entity.ChocoGearEntities database = new Entity.ChocoGearEntities();
        private static ProductDao instance = null;
        private ProductDao()
        {

        }
        public static ProductDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDao();
                }
                return instance;
            }
        }

        public int Create(ProductView item)
        {
            try
            {
                if(checkNameProduct(item.name_product) == false)
                {
                    Entity.Product pro = new Entity.Product() { id = item.id, name_product = item.name_product, name_image = item.name_image, id_brand = item.id_brand, id_category = item.id_category, description = item.description, discount = item.discount, price = item.price, created = item.created, status = item.status };
                    database.Products.Add(pro);
                    database.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(int id)
        {

            var q = database.Products.Where(d => d.id == id).FirstOrDefault();
            database.Products.Remove(q);
            database.SaveChanges();

            return 1;
        }

        public ProductView GetId(int id)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Products.Where(d => d.id == id).Select(d => new ModelView.ProductView()
            {
                id = d.id,
                name_product = d.name_product,
                name_image = d.name_image,
                created = (DateTime)d.created,
                price = (float)d.price,
                description = d.description,
                discount = (float)d.discount,
                name_category = d.Category.name_category,
                name_brand = d.Brand.name,
                status = (bool)d.status
            }).FirstOrDefault();
            return q;
        }

        public List<ProductView> Gets()
        {
            var q = database.Products.Where(d => d.status == true).Select(d => new ProductView
            {
                id = d.id,
                name_product = d.name_product,
                name_image = d.name_image,
                created = (DateTime)d.created,
                price = (float)d.price,
                description = d.description,
                discount = (float)d.discount,
                name_category = d.Category.name_category,
                name_brand = d.Brand.name,
                status = (bool)d.status
            }).ToList();
            return q;
        }

        public int Update(ProductView item)
        {
            throw new NotImplementedException();
        }

        public bool checkNameProduct(string name)
        {
            var q = database.Products.Where(d => d.name_product == name).FirstOrDefault();
            if(q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ProductView> Skip(int Page, int Row)
        {
            var q = database.Products.Select(d => new ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).OrderBy(d => d.id).Skip((Page - 1) * Row).Take(Row).ToList();
            return q;
        }
    }
}