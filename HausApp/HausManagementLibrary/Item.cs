using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Item
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Stage { get; set; }
        public string CreatedAt { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public Item(int id, string customer, string stage, string created_at, int width, int height)
        {
            Id = id;
            Customer = customer;
            Stage = stage;
            CreatedAt = created_at;
            Width = width;
            Height = height;
        }
    }
}
