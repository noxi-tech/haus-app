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
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        DataAccessor data = new DataAccessor();

        #region New Items Fields
        List<ItemCreate> createItems = new List<ItemCreate>();
        ObservableCollection<Order> newOrders = new ObservableCollection<Order>();
        #endregion

        List<Item> items = new List<Item>();

        public ItemsView()
        {
            InitializeComponent();
            InitializeOptions();
            RefreshData();
            this.DataContext = this;
        }

        #region Initializations region
        private async void UpdateItems()
        {
            try
            {
                items = await data.GetItems();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async void InitializeOptions()
        {
            cbSw.ItemsSource = AvailableOptions.SwOptions;
            cbSk.ItemsSource = AvailableOptions.SkOptions;
            cbT.ItemsSource = AvailableOptions.TOptions;
            cbP.ItemsSource = AvailableOptions.POptions;
            cbSh.ItemsSource = AvailableOptions.ShOptions;
            cbMt.ItemsSource = AvailableOptions.MtOptions;
            cbSg.ItemsSource = AvailableOptions.SgOptions;
            cbPieces.ItemsSource = AvailableOptions.PiecesOptions;
            cbCompanyName.ItemsSource = await data.GetCompanies();
        }
        #endregion

        #region ItemsInProgress region
        private void grdInProgressItems_Sorting(object sender, DataGridSortingEventArgs e)
        {
            MessageBox.Show("Is Working? ");
        }
        private void RefreshData()
        {
            UpdateItems();
            grdInProgressItems.ItemsSource = items;
        }
        private void btnRefreshInProgressItems_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
        #endregion

        #region Navigation region
        private void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            tbItems.SelectedIndex = 1;
        }
        #endregion

        #region Create Order/Item region
        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            cbCompanyName.IsEnabled = false;
            txtCustomerName.IsEnabled = false;
            ResetNewFields();
        }
        private async void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = await data.CreateOrder(cbCompanyName.Text, txtCustomerName.Text);
            createItems.Add(CreateItem(order.Id));

            foreach (var item in createItems)
            {
                await data.CreateItem(item);
            }
            MessageBox.Show($"{order.Id}");
            tbItems.SelectedIndex = 0;
            cbCompanyName.IsEnabled = true;
            txtCustomerName.IsEnabled = true;
            cbCompanyName.Text = "";
            cbCompanyName.SelectedItem = null;
            txtCustomerName.Text = "";
            ResetNewFields();
        }
        private void ResetNewFields()
        {
            txtH.Text = "";
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtNotes.Text = "";
            txtT.Text = "";
            cbMt.Text = "";
            cbMt.SelectedItem = null;
            cbP.SelectedItem = null;
            cbPieces.Text = "";
            cbPieces.SelectedItem = null;
            cbSg.Text = "";
            cbSg.SelectedItem = null;
            cbSh.Text = "";
            cbSh.SelectedItem = null;
            cbSk.Text = "";
            cbSk.SelectedItem = null;
            cbSw.Text = "";
            cbSw.SelectedItem = null;
            cbT.Text = "";
            cbT.SelectedItem = null;
        }
        private ItemCreate CreateItem(int orderId)
        {
            return new ItemCreate(int.Parse(txtWidth.Text), int.Parse(txtHeight.Text), cbSw.Text, cbSk.Text,
                cbT.Text, cbP.Text, int.Parse(cbSh.Text), cbMt.Text, 0, cbSg.Text, int.Parse(txtT.Text), int.Parse(txtH.Text),
                cbPieces.Text, txtNotes.Text, orderId);
        }
        #endregion
    }
}
