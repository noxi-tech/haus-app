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
                    await Task.Run(() =>
                    {
                        Thread.Sleep(200);
                    });
                }
                IsLoading = false;
            }
        }
        //private void RefreshData()
        //{
        //    LoadEmployees();
        //    //grdEmployees.ItemsSource = employees;
        //}
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
        //private void btnDeleteEmployees_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedEmployees = grdEmployees.SelectedItems;
        //    foreach (Employee employee in selectedEmployees)
        //    {
        //        employees.Remove(employee);
        //        MessageBox.Show($"{employee.Id}");
        //    }
        //    grdEmployees.ItemsSource = employees;
        //}
        //private void cbEmployeeSelect_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var chk = (CheckBox)sender;
        //    var row = VisualTreeHelpers.FindAncestor<DataGridRow>(chk);
        //    var newValue = !chk.IsChecked.GetValueOrDefault();

        //    row.IsSelected = newValue;
        //    //chk.IsChecked = newValue;

        //    // Mark event as handled so that the default 
        //    // DataGridPreviewMouseDown doesn't handle the event
        //    e.Handled = true;
        //}
        //private void grdEmployees_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var chk = VisualTreeHelpers.FindAncestor<CheckBox>((DependencyObject)e.OriginalSource, "cbEmployeeSelect");
        //    if (chk == null)
        //        e.Handled = true;
        //}
    }
}
