using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class FeedBackDao : IRepository<ModelView.FeedBackView>
    {
        private static FeedBackDao instance = null;
        private FeedBackDao()
        {

        }
        public static FeedBackDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedBackDao();
                }
                return instance;
            }
        }

        public int Create(FeedBackView item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public FeedBackView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<FeedBackView> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(FeedBackView item)
        {
            throw new NotImplementedException();
        }
    }
}