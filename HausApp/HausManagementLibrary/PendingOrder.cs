using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class PendingOrder
    {
        private DataAccessor data = new DataAccessor();
        public OrderCreate Order { get; set; }
        public List<ItemCreate> Items { get; set; } = new List<ItemCreate>();

        public PendingOrder(OrderCreate order, ItemCreate item)
        {
            Order = order;
            Items.Add(item);
        }
        public PendingOrder() { }
        public async Task<Order> Commit()
        {
            var newOrder = await data.CreateOrder(Order);
            foreach (var item in Items)
            {
                item.OrderId = newOrder.Id;
                newOrder.Items.Add(await data.CreateItem(item));
            }
            return newOrder;
        }
    }
}
