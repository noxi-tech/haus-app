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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HausManagementUI
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl, INotifyPropertyChanged
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        List<long> selectedEmployees = new List<long>();

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

        public EmployeesView()
        {
            InitializeComponent();
            LoadEmployees();
            grdEmployees.ItemsSource = employees;
            this.DataContext = this;
        }

        private async void LoadEmployees()
        {
            if (!isLoading)
            {
                selectedEmployees.Clear();
                List<Employee> fetchedEmployees = new List<Employee>();
                employees.Clear();
                IsLoading = true;
                try
                {
                    fetchedEmployees = await data.GetEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                foreach (var employee in fetchedEmployees)
                {
                    employees.Add(employee);
                    //await Task.Run(() =>
                    //{
                    //    Thread.Sleep(200);
                    //});
                }
                IsLoading = false;
            }
        }
        private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee newEmployee = await data.CreateEmployee(txtEmployeeName.Text, cbJob.Text);
                employees.Add(newEmployee);
                txtEmployeeName.Text = string.Empty;
                cbJob.SelectedItem = null;
                tbEmployees.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSwitchToAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            tbEmployees.SelectedIndex = 1;
        }
        private void btnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
        private void cbEmployeeSelect_Checked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Add(((Employee)((CheckBox)sender).DataContext).Id);
        }
        private void cbEmployeeSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Remove(((Employee)((CheckBox)sender).DataContext).Id);
        }
        private async void btnDeleteEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployees.Count > 0)
            {
                foreach (var employeeId in selectedEmployees)
                {
                    await data.DeleteEmployee(employeeId);
                }
                LoadEmployees();
            }
        }
    }
}
