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
            try
            {
                Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
                Entity.Feedback fb = new Entity.Feedback() { id = item.id, email = item.email, content = item.content, created = item.created, status = item.status };
                db.Feedbacks.Add(fb);
                db.SaveChanges();
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