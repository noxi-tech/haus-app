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
        public List<Item> Items { get; set; }
        public bool isCompleted { get; set; }

        public Order(int id, string company, string customer,List<Item> items, bool is_completed, string created_at)
            :base(company,customer)
        {
            Id = id;
            CreatedAt = created_at;
            Items = items;
            isCompleted = is_completed;
        }
    }
}
