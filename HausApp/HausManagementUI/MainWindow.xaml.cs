using HausManagementLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Fields Region
        private bool isConnected;
        private bool isPending;
        #endregion

        #region Properties Region
        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; OnPropertyChanged("IsConnected"); }
        }        
        public bool IsPending
        {
            get { return isPending; }
            set { isPending = value; OnPropertyChanged("isPending"); }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            IsConnected = false;
            IsPending = false;
        }

        #region Events Region
        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            var managerWindow = new ManagerLoginUI();
            managerWindow.Show();
            this.Close();
        }
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = new EmployeeManagementUI();
            employeeWindow.Show();
            this.Close();
        }
        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            DataAccessor accessor = new DataAccessor();
            try
            {
                IsPending = true;
                //if (txtUrl.Text == "1")
                //{
                //    MessageBox.Show("Connected Successfully.");
                //    IsConnected = true;
                //}
                if (await accessor.Ping(txtUrl.Text) == "pong")
                {
                    MessageBox.Show("Connected Successfully.");
                    IsConnected = true;
                    ConfigurationManager.AppSettings["Root"] = txtUrl.Text;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect.");
                IsPending = false;
            }
        }
        #endregion
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
