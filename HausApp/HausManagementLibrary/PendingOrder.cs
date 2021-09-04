using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class PendingOrder
    {
        public OrderCreate Order { get; set; }
        public List<ItemCreate> Items { get; set; } = new List<ItemCreate>();

        public PendingOrder(Order order, ItemCreate item)
        {
            Order = order;
            Items.Add(item);
        }
    }
}
