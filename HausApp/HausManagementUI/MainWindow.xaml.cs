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
        DataAccessor accessor = new DataAccessor();
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
            InitializeSettings();
            this.DataContext = this;
        }

        #region Events Region
        public void InitializeSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Uri))
            {
                ConfigurationManager.AppSettings["Root"] = Properties.Settings.Default.Uri;
                CurrentSettings.CsvPath = Properties.Settings.Default.CsvPath;
                CurrentSettings.Root = Properties.Settings.Default.Uri;
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
                IsPending = false;
            }
        }
        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            var managerWindow = new ManagerLoginUI();
            managerWindow.Show();
            Properties.Settings.Default.IsManager = true;
            this.Close();
        }
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            var employeeWindow = new EmployeeManagementUI();
            employeeWindow.Show();
            Properties.Settings.Default.IsEmployee = true;
            this.Close();
        }
        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsPending = true;
                //if (txtUri.Text == "1")
                //{
                //    MessageBox.Show("Connected Successfully.");
                //    IsConnected = true;
                //    btnEmployee.IsEnabled = true;
                //    btnManager.IsEnabled = true;
                //}
                if (await accessor.Ping(txtUri.Text) == "pong")
                {
                    MessageBox.Show("Connected Successfully.");
                    IsConnected = true;
                    Properties.Settings.Default.Uri = txtUri.Text;
                    ConfigurationManager.AppSettings["Root"] = txtUri.Text;
                    btnEmployee.IsEnabled = true;
                    btnManager.IsEnabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't Connect.");
                IsPending = false;
            }
        }
        private void txtReconnectionCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtReconnectionCode.Text == "1234")
                {
                    Properties.Settings.Default.Reset();
                    this.Close();
                }
            }
        }
        private void icReconnect_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtReconnectionCode.Visibility = Visibility.Visible;
        }
        protected override void OnClosed(EventArgs e)
        {
            Properties.Settings.Default.Save();
            base.OnClosed(e);
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
