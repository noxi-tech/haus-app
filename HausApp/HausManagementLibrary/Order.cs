using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Order
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }

        public Order() { }
        public Order(int id, string companyName, string customerName)
        {
            Id = id;
            CompanyName = companyName;
            CustomerName = customerName;
        }
    }
}
