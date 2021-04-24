using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class BrandDao : IRepository<ModelView.Brand>
    {
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
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Brand GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Brand> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(Brand item)
        {
            throw new NotImplementedException();
        }
    }
}