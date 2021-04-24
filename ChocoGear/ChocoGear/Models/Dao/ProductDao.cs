using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class ProductDao : IRepository<Models.ModelView.ProductView>
    {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int Update(ProductView item)
        {
            throw new NotImplementedException();
        }
    }
}