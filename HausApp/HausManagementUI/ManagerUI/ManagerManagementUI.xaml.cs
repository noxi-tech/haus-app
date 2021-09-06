using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManagerManagementUI.xaml
    /// </summary>
    public partial class ManagerManagementUI : Window
    {
        private HomeView homeView;
        private ItemsView itemsView;
        private EmployeesView employeesView;
        private SettingsView settingsView;

        public ManagerManagementUI()
        {
            InitializeComponent();
            homeView = new HomeView();
            itemsView = new ItemsView();
            employeesView = new EmployeesView();
            settingsView = new SettingsView();
        }
        private void lbNavigationMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChooseTab(lbNavigationMenu.SelectedIndex);
        }
        private void ChooseTab(int index)
        {
            switch (index)
            {
                case 0:
                    grdContentArea.Children.Clear();
                    grdContentArea.Children.Add(homeView);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, null);
                    break;
                case 1:
                    grdContentArea.Children.Clear();
                    grdContentArea.Children.Add(itemsView);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, null);
                    break;
                case 2:
                    grdContentArea.Children.Clear();
                    grdContentArea.Children.Add(employeesView);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, null);
                    break;
                case 3:
                    grdContentArea.Children.Clear();
                    grdContentArea.Children.Add(settingsView);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, null);
                    break;

            }
        }
    }
}
