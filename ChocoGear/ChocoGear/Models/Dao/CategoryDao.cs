using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class CategoryDao : IRepository<ModelView.CategoryView>
    {
        Models.Entity.ChocoGearEntities database = new Entity.ChocoGearEntities();
        private static CategoryDao instance = null;
        private CategoryDao()
        {

        }
        public static CategoryDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDao();
                }
                return instance;
            }
        }

        public int Create(CategoryView item)
        {
            Entity.Category cate = new Entity.Category() { id = item.id, name_category = item.name_category, status = item.status };
            database.Categories.Add(cate);
            database.SaveChanges();
            return cate.id;
        }

        public int Delete(int id)
        {
            var q = database.Categories.Where(d => d.id == id).FirstOrDefault();
            database.Categories.Remove(q);
            database.SaveChanges();
            return 1;
        }

        public CategoryView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryView> Gets()
        {
            var q = database.Categories.Select(d=>new CategoryView { id = d.id, name_category = d.name_category, status = (bool)d.status}).ToList();
            return q;
        }

        public int Update(CategoryView item)
        {
            if(CheckNameExist(item.name_category, item.id) == false)
            {
                var q = database.Categories.Where(d => d.id == item.id).FirstOrDefault();
                q.name_category = item.name_category;
                q.status = item.status;
                database.SaveChanges();
                return 1;
            }
            return 0;
        }

        public bool CheckNameExist(string name, int id)
        {
            var q = database.Categories.Where(d => d.id != id && d.name_category == name).Count();
            if(q > 0)
            {
                return true;
            }
            return false;
        }
    }
}