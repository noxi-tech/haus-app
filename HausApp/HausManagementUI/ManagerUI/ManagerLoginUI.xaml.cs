using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for ManagerLoginUI.xaml
    /// </summary>
    public partial class ManagerLoginUI : Window
    {
        public ManagerLoginUI()
        {
            InitializeComponent();
        }

        private async void btnCode_Click(object sender, RoutedEventArgs e)
        {
            if(txtCode.Password == "1234")
            {
                ManagerManagementUI managerManagementUI = new ManagerManagementUI();
                iconKey.Foreground = Brushes.Green;
                await Task.Run(() => { Thread.Sleep(1000); });
                managerManagementUI.Show();
                this.Close();
            }
            else
            {
                iconKey.Foreground = Brushes.DarkRed;
                MessageBox.Show("Incorrect Code.");
            }
        }
    }
}
