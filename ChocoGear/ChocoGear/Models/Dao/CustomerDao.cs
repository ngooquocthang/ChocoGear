using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ChocoGear.Models.Dao
{
    public sealed class CustomerDao : IRepository<ModelView.CustomerView>
    {

        Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
        private static CustomerDao instance = null;
        private CustomerDao()
        {

        }
        public static CustomerDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDao();
                }
                return instance;
            }
        }

        public int Create(CustomerView item)
        {
            try
            {
                Models.Entity.Customer c = new Entity.Customer() {first_name=item.first_name,last_name=item.last_name,phone=item.phone,email=item.email,address=item.address,username=item.username,password=item.password,status=item.status};
                db.Customers.Add(c);
                db.SaveChanges();
                return 1;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
                return 0;
            }
        }

        public int Delete(int id)
        {
            var q = db.Customers.Where(d => d.id == id).FirstOrDefault();
            db.Customers.Remove(q);
            db.SaveChanges();
            return 1;
        }

        public CustomerView GetId(int id)
        {
            Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
            var q = db.Customers.Where(d => d.id == id).Select(d => new ModelView.CustomerView()
            {
                id = d.id,
                first_name = d.first_name,
                last_name = d.last_name,
                phone = d.phone,
                email = d.email,
                address = d.address,
                username = d.username,
                password = d.password,
            }).FirstOrDefault();
            return q;
        }

        public List<CustomerView> Gets()
        {
            var q = db.Customers.Select(d => new Models.ModelView.CustomerView { id = d.id, username = d.username, password = d.password, address = d.address, email = d.email, first_name = d.first_name, last_name = d.last_name, phone = d.phone, status = (bool)d.status }).ToList();
            return q;
        }

        public int Update(CustomerView item)
        {
            try
            {
                Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
                var q = db.Customers.Where(d => d.id == item.id).FirstOrDefault();
                q.id = item.id;
                q.first_name = item.first_name;
                q.last_name = item.last_name;
                q.phone = item.phone;
                q.email = item.email;
                q.address = item.address;
                q.password = item.password;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Models.ModelView.CustomerView GetCus(string username) {
            var q = db.Customers.Where(d => d.username == username).Select(d => new Models.ModelView.CustomerView { id = d.id, username = d.username, password = d.password, address = d.address, email = d.email, first_name = d.first_name, last_name = d.last_name, phone = d.phone, status = (bool)d.status }).FirstOrDefault();
            return q;
        }
    }
}