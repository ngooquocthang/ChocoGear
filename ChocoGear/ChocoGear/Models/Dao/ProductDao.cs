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
                Entity.Product pro = new Entity.Product() { id = item.id, name_product = item.name_product, name_image = item.name_image, id_brand = item.id_brand, id_category = item.id_category, description = item.description, discount = item.discount, price = item.price, created = item.created, status = item.status };
                database.Products.Add(pro);
                database.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProductView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductView> Gets()
        {
            /*var q = database.Products.Select(d=>new ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created =(DateTime)d.created, description =d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price =(float)d.price, status =(bool)d.status}).ToList();
            return q;*/
            var q = (from pr in database.Products
                     join br in database.Brands on pr.id_brand equals br.id
                     join ct in database.Categories on pr.id_category equals ct.id
                     select new ProductView
                     {
                         id = pr.id,
                         name_product = pr.name_product,
                         name_image = pr.name_image,
                         created = (DateTime)pr.created,
                         description = pr.description,
                         discount = (float)pr.discount,
                         id_brand = (int)pr.id_brand,
                         id_category = (int)pr.id_category,
                         price = (float)pr.price,
                         status = (bool)pr.status,
                         name_brand = br.name,
                         name_category = ct.name_category,
                     }).ToList();
            return q;
        }

        public int Update(ProductView item)
        {
            throw new NotImplementedException();
        }
    }
}