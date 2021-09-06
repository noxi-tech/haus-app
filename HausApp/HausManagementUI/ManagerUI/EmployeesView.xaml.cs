using HausManagementLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    public partial class EmployeesView : UserControl
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public EmployeesView()
        {
            InitializeComponent();
            this.DataContext = this;
            RefreshData();
        }

        private async void UpdateEmployees()
        {
            //spLoading.Visibility = Visibility.Visible;
            try
            {
                employees = await data.GetEmployees();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                //spLoading.Visibility = Visibility.Collapsed;
            }
        }
        private void RefreshData()
        {
            UpdateEmployees();
            grdEmployees.ItemsSource = employees;
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
            RefreshData();
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
