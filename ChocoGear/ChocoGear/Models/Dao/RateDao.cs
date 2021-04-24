using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class RateDao : IRepository<ModelView.RateView>
    {
        private static RateDao instance = null;
        private RateDao()
        {

        }
        public static RateDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RateDao();
                }
                return instance;
            }
        }

        public int Create(RateView item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public RateView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<RateView> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(RateView item)
        {
            throw new NotImplementedException();
        }
    }
}