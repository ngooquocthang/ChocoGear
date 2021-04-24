using ChocoGear.Models.ModelView;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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