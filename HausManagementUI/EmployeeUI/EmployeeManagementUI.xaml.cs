using HausManagementLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HausManagementUI
{
    /// <summary>
    /// Interaction logic for EmployeeManagementUI.xaml
    /// </summary>
    public partial class EmployeeManagementUI : Window, INotifyPropertyChanged
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        #region Loading Data Fields
        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; OnPropertyChanged("IsLoading"); OnPropertyChanged("IsNotLoading"); }
        }
        public bool IsNotLoading
        {
            get { return !isLoading; }
        }
        #endregion

        #region PropertyChange Region
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public EmployeeManagementUI()
        {
            IsLoading = true;
            InitializeComponent();
            LoadEmployees();
            icEmployeeChoose.ItemsSource = employees;
            this.DataContext = this;
            IsLoading = false;
        }

        private void btnChooseEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee)((Button)sender).DataContext;
            BarcodeDialog barcodeDialog = new BarcodeDialog(emp);
            barcodeDialog.ShowDialog();
        }
        private async void btnCheckinEmployee(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            Employee emp = (Employee)((Button)sender).DataContext;
            if (await CheckinEmployee(emp.Id))
            {
                MessageBox.Show("Welcome, " + emp.Name);
            }

            button.IsEnabled = true;
        }
        private async void btnCheckoutEmployee(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            Employee emp = (Employee)((Button)sender).DataContext;
            if (await CheckoutEmployee(emp.Id))
            {
                MessageBox.Show("Goodbye, " + emp.Name);
            }

            button.IsEnabled = true;
        }
        private async void btnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;
                btnRefreshEmployees.IsEnabled = false;

                await LoadEmployees();
                
                btnRefreshEmployees.IsEnabled = true;
                IsLoading = false;
            }
        }

        private async Task LoadEmployees()
        {
            //List<Employee> fetchedEmployees = new List<Employee>();
            //employees.Clear();
            //try
            //{
            //    fetchedEmployees = await data.GetEmployees();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //foreach (var order in fetchedEmployees)
            //{
            //    employees.Add(order);
            //}

            List<Employee> fetchedEmployees = null;

            try
            {
                fetchedEmployees = await data.GetEmployees();
                if (fetchedEmployees == null || !fetchedEmployees.Any())
                {
                    MessageBox.Show("No employees found.");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}");
                return;
            }

            UpdateEmployeeList(fetchedEmployees);
        }
        private async Task<bool> CheckinEmployee(long employeeId)
        {
            try
            {
                ClockRecord respond = await data.TimekeeperCheckin(employeeId);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private async Task<bool> CheckoutEmployee(long employeeId)
        {
            try
            {
                ClockRecord respond = await data.TimekeeperCheckout(employeeId);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void UpdateEmployeeList(List<Employee> fetchedEmployees)
        {
            employees.Clear();
            foreach (var employee in fetchedEmployees)
            {
                employees.Add(employee);
            }
        }
    }
}
