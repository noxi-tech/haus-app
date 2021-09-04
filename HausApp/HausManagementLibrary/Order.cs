using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Order : OrderCreate
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public Order(int id, string company, string customer, string created_at)
            :base(company,customer)
        {
            Id = id;
            CreatedAt = created_at;
        }
    }
}
