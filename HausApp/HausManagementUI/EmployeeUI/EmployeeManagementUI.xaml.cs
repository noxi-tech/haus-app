using HausManagementLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace HausManagementUI
{
    /// <summary>
    /// Interaction logic for EmployeeManagementUI.xaml
    /// </summary>
    public partial class EmployeeManagementUI : Window
    {

        DataAccessor data = new DataAccessor();

        public EmployeeManagementUI()
        {
            InitializeComponent();
            InitializeEmployees();
            this.DataContext = this;
        }

        private async void InitializeEmployees()
        {
            spLoading.Visibility = Visibility.Visible;
            try
            {
                lbEmployeeChoose.ItemsSource = await data.GetEmployees();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                spLoading.Visibility = Visibility.Collapsed;
            }
        }
        private void btnChooseEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee)((Button)sender).DataContext;
            BarcodeDialog barcodeDialog = new BarcodeDialog(emp);
            barcodeDialog.ShowDialog();
        }
    }
}
