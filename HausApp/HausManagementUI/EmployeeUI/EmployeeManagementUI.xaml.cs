using HausManagementLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    public partial class EmployeeManagementUI : Window
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        bool isLoading = false;

        public EmployeeManagementUI()
        {
            InitializeComponent();
            LoadEmployees();
            icEmployeeChoose.ItemsSource = employees;
            this.DataContext = this;
        }

        private async void LoadEmployees()
        {
            if (!isLoading)
            {
                List<Employee> fetchedEmployees = new List<Employee>();
                employees.Clear();
                isLoading = true;
                try
                {
                    fetchedEmployees = await data.GetEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                foreach (var order in fetchedEmployees)
                {
                    employees.Add(order);
                    //await Task.Run(() =>
                    //{
                    //    Thread.Sleep(200);
                    //});
                }
                isLoading = false;
            }
        }
        private void btnChooseEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee)((Button)sender).DataContext;
            BarcodeDialog barcodeDialog = new BarcodeDialog(emp);
            barcodeDialog.ShowDialog();
        }
        private void btnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
    }
}
