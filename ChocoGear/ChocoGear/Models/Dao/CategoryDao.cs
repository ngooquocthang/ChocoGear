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
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}