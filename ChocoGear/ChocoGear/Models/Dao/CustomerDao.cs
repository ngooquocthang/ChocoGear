﻿using ChocoGear.Models.ModelView;
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
                Models.Entity.ChocoGearEntities db = new Entity.ChocoGearEntities();
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
            throw new NotImplementedException();
        }

        public CustomerView GetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerView> Gets()
        {
            throw new NotImplementedException();
        }

        public int Update(CustomerView item)
        {
            throw new NotImplementedException();
        }
    }
}