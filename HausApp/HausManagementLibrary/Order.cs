using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HausManagementLibrary
{
    public class Order : OrderCreate
    {
        public int Id { get; set; }
        public string CreatedAt { get; set; }
        public List<Item> Items { get; set; }
        public bool IsCompleted { get; set; }
        public string DeliveredBy { get; set; }
        public string BillId { get; set; }
        public bool IsGettingLate
        {
            get
            {
                return DateTime.Parse(CreatedAt).Date.AddDays(0) <= DateTime.Today;
            }
        }
        public bool IsLate
        {
            get
            {
                return DateTime.Parse(CreatedAt).Date.AddDays(1) <= DateTime.Today;
            }
        }
        public int DaysToDueDate
        {
            get
            {
                return (DateTime.Parse(CreatedAt).Date.AddDays(1) - DateTime.Today).Days;
            }
        }
        public Brush StatusBrush
        {
            get
            {
                if (IsCompleted)
                {
                    return Brushes.LawnGreen;
                }
                else if (IsLate)
                {
                    return Brushes.OrangeRed;
                }
                else if (IsGettingLate)
                {
                    return Brushes.Orange;
                }
                return null;
            }
        }

        public Order(int id, string company, string customer,List<Item> items, bool is_completed, string delivered_to, string bill_id, string created_at)
            :base(company,customer)
        {
            Id = id;
            CreatedAt = created_at;
            Items = items;
            IsCompleted = is_completed;
        }
    }
}
