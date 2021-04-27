using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class BrandDao : IRepository<ModelView.Brand>
    {
        Models.Entity.ChocoGearEntities database = new Entity.ChocoGearEntities();
        private static BrandDao instance = null;
        private BrandDao()
        {

        }
        public static BrandDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BrandDao();
                }
                return instance;
            }
        }

        public int Create(Brand item)
        {
            Entity.Brand brand = new Entity.Brand();
            brand.id = item.id;
            brand.name = item.name;
            database.Brands.Add(brand);
            database.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            var q = database.Brands.Where(d => d.id == id).FirstOrDefault();
            database.Brands.Remove(q);
            database.SaveChanges();
            return 1;
        }

        public Brand GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Brand> Gets()
        {
            var q = database.Brands.Select(d => new Brand { id = d.id, name = d.name }).ToList();
            return q;
        }

        public int Update(Brand item)
        {
            if(CheckNameExist(item.name, item.id) == false)
            {
                var q = database.Brands.Where(d => d.id == item.id).FirstOrDefault();
                q.name = item.name;
                database.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
            
        }
        public bool CheckNameExist(string name, int id)
        {
            var q = database.Brands.Where(d => d.id != id && d.name == name).Count();
            if (q > 0)
            {
                return true;
            }
            return false;
        }
    }
}