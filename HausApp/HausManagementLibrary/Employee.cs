using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private long id;
        private string name { get; set; }
        private string job { get; set; }
        private long? assignedItem { get; set; }

        public Employee(long id, string name, string job, long? assigned_item)
        {
            Id = id;
            Name = name;
            Job = job;
            AssignedItem = assigned_item;
        }


        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }         
        public string Job
        {
            get { return job; }
            set { job = value; OnPropertyChanged("Job"); }
        }        
        public long? AssignedItem
        {
            get { return assignedItem; }
            set { assignedItem = value; OnPropertyChanged("AssignedItem"); }
        }
 
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
