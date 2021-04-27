using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public class OrderDetailDao : IRepository<Models.ModelView.OrderDetail>
    {
        Models.Entity.ChocoGearEntities database = new Entity.ChocoGearEntities();
        private static OrderDetailDao instance = null;
        private OrderDetailDao()
        {

        }
        public static OrderDetailDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDetailDao();
                }
                return instance;
            }
        }
        public int Create(OrderDetail item)
        {
            Entity.OrderDetail orderD = new Entity.OrderDetail() { id = item.id, id_orders = item.id_orders, id_product = item.id_product, quantity = item.quantity, sub_total = item.sub_total, status = item.status };
            database.OrderDetails.Add(orderD);
            database.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(OrderDetail item)
        {
            throw new NotImplementedException();
        }
    }
}