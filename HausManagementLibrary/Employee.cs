using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Employee : EmployeeCreate
    {
        private long id;
        private long? assignedItem;
        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        public long? AssignedItem
        {
            get { return assignedItem; }
            set { assignedItem = value; OnPropertyChanged("AssignedItem"); }
        }

        public Employee(long id, string name, string job, long? assigned_item)
            : base(name, job)
        {
            Id = id;
            AssignedItem = assigned_item;
        }
    }
}
