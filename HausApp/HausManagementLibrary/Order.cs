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
                return DateTime.Parse(CreatedAt).Date.AddDays(CurrentSettings.OrderWarrning) <= DateTime.Today;
            }
        }
        public bool IsLate
        {
            get
            {
                return DateTime.Parse(CreatedAt).Date.AddDays(CurrentSettings.LastOrderWarrning) <= DateTime.Today;
            }
        }
        public int DaysToDueDate
        {
            get
            {
                return (DateTime.Parse(CreatedAt).Date.AddDays(CurrentSettings.LastOrderWarrning) - DateTime.Today).Days;
            }
        }
        public Brush StatusBrush
        {
            get
            {
                if (IsCompleted)
                {
                    return Brushes.Green;
                }
                else if (IsLate)
                {
                    return Brushes.OrangeRed;
                }
                else if (IsGettingLate)
                {
                    return Brushes.DarkOrange;
                }
                return null;
            }
        }
        public bool CanDeliver
        {
            get { return IsCompleted && string.IsNullOrEmpty(DeliveredBy); }
        }
        public bool CanBill
        {
            get { return string.IsNullOrEmpty(BillId); }
        }

        public Order(int id, string company, string customer, List<Item> items, bool is_completed, string delivered_by, string bill_id, string created_at)
            : base(company, customer)
        {
            Id = id;
            CreatedAt = created_at;
            Items = items;
            IsCompleted = is_completed;
            DeliveredBy = delivered_by;
            BillId = bill_id;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order order = (Order)obj;
                return order.Id == Id;
            }
        }
    }
}
