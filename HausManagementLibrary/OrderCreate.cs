using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    [Serializable]
    public class OrderCreate
    {
        private string company;
        private string customer;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }
        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public OrderCreate(string company, string customer)
        {
            Company = company;
            Customer = customer;
        }

    }
}
