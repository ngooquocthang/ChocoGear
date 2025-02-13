﻿using ChocoGear.Models.ModelView;
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
            Models.Dao.OrderDetailDao orderdetail = Models.Dao.OrderDetailDao.Instance;
            var check = orderdetail.CheckConstraintProduct(id);
            if(check == 1)
            {
                return 0;
            }
            else
            {
                database.SaveChanges();
                return 1;
            }
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
            try
            {
                Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
                var q = db.Products.Where(d => d.id == item.id).FirstOrDefault();
                q.id = item.id;
                q.name_product = item.name_product;
                q.name_image = item.name_image;
                q.price = item.price;
                q.description = item.description;
                q.status = item.status;
                q.id_category = item.id_category;
                q.id_brand = item.id_brand;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
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
        public int GetIdName(string name)
        {
            return database.Products.Where(d => d.name_product == name).Select(d => d.id).FirstOrDefault();
        }

        public List<ProductView> Skip(int Page, int Row)
        {
            var q = database.Products.Select(d => new ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).OrderBy(d => d.id).Skip((Page - 1) * Row).Take(Row).ToList();
            return q;
        }
        public ProductView Search_Category(string name_cate)
        {
            var q = database.Products.Where(d => d.Category.name_category == name_cate).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).FirstOrDefault();
            return q;
        }
        public ProductView Search_Product(string name_product)
        {
            var q = database.Products.Where(d => d.name_product == name_product).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).FirstOrDefault();
            return q;
        }
        public List<ProductView> Search_characters_product(string name_product)
        {
            var q = database.Products.Where(d => d.name_product.Contains(name_product)).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).ToList();
            return q;
        }
        public List<ProductView> Search_characters_Brand(string name_brand)
        {
            var q = database.Products.Where(d => d.Brand.name.Contains(name_brand)).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).ToList();
            return q;
        }
        public List<ProductView> Search_characters_Category(string name_cate)
        {
            var q = database.Products.Where(d => d.Category.name_category.Contains(name_cate)).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).ToList();
            return q;
        }
        public ProductView Search_Brand(string name_brand)
        {
            var q = database.Products.Where(d => d.Brand.name == name_brand).Select(d => new Models.ModelView.ProductView { id = d.id, name_product = d.name_product, name_image = d.name_image, created = (DateTime)d.created, description = d.description, discount = (float)d.discount, id_brand = (int)d.id_brand, id_category = (int)d.id_category, price = (float)d.price, status = (bool)d.status }).FirstOrDefault();
            return q;
        }
    }
}