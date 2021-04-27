using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class OrderDao : IRepository<ModelView.OrderView>
    {
        Models.Entity.ChocoGearEntities database = new Entity.ChocoGearEntities();
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
            Models.Entity.Order order = new Entity.Order() { id = item.id,email_order = item.email_order, address_order = item.address_order, phone_order = item.phone_order, id_customer = item.id_customer, order_date = item.order_date, total = item.total, status = item.status };
            database.Orders.Add(order);
            database.SaveChanges();
            return order.id;
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
            var q = database.Orders.Select(d => new Models.ModelView.OrderView { id = d.id, email_order = d.email_order, address_order = d.address_order, phone_order = d.phone_order, id_customer = (int)d.id_customer, order_date = (DateTime)d.order_date, total = (float)d.total, status = (bool)d.status }).ToList();
            var a = (from cus in database.Customers
                     join order in database.Orders on cus.id equals order.id_customer
                     select new Models.ModelView.OrderView
                     {
                         id = order.id,
                         email_order = order.email_order,
                         address_order = order.address_order,
                         phone_order = order.phone_order,
                         id_customer = (int)order.id_customer,
                         order_date = (DateTime)order.order_date,
                         total = (float)order.total,
                         status = (bool)order.status,
                         namecus_order = cus.first_name +" "+cus.last_name
                     }).ToList();
            return a;
        }

        public int Update(OrderView item)
        {
            throw new NotImplementedException();
        }
    }
}