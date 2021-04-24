using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class OrderDao : IRepository<ModelView.OrderView>
    {
        private static OrderDao instance = null;
        private OrderDao()
        {

        }
        public static OrderDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDao();
                }
                return instance;
            }
        }

        public int Create(OrderView item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderView> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(OrderView item)
        {
            throw new NotImplementedException();
        }
    }
}