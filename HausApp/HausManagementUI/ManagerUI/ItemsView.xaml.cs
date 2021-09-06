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
        ObservableCollection<PendingOrder> pendingOrders = new ObservableCollection<PendingOrder>();
        List<PendingOrder> selectedPendingOrders = new List<PendingOrder>();
        PendingOrder currentPendingOrder { get; set; }
        #endregion

        #region In Progress Items Fields
        List<Item> itemsInProgress = new List<Item>();
        #endregion
        
        public ItemsView()
        {
            InitializeComponent();
            InitializeOptions();
            InitializeSources();
            RefreshData();
            this.DataContext = this;
        }

        #region Initializations region
        private void InitializeSources()
        {
            icNewItems.ItemsSource = pendingOrders;
        }
        private async void UpdateItems()
        {
            try
            {
                itemsInProgress = await data.GetItems();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private async void InitializeOptions()
        {
            try
            {
                cbSw.ItemsSource = AvailableOptions.SwOptions;
                cbSk.ItemsSource = AvailableOptions.SkOptions;
                cbT.ItemsSource = AvailableOptions.TOptions;
                cbP.ItemsSource = AvailableOptions.POptions;
                cbSh.ItemsSource = AvailableOptions.ShOptions;
                cbMt.ItemsSource = AvailableOptions.MtOptions;
                cbSgType.ItemsSource = AvailableOptions.SgOptions;
                cbParts.ItemsSource = AvailableOptions.PiecesOptions;
                cbNotes.ItemsSource = AvailableOptions.NotesOptions;
                cbCompanyName.ItemsSource = await data.GetCompanies();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
            grdInProgressItems.ItemsSource = itemsInProgress;
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
            ResetOrder();
        }
        #endregion

        #region Create Order/Item region
        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = CreateItem();

                cbCompanyName.IsEnabled = false;
                txtCustomerName.IsEnabled = false;
                currentPendingOrder.Items.Add(newItem);
                txtOutOf.Text = $"{currentPendingOrder.Items.Count}/{currentPendingOrder.Items.Count}";
                ResetNewFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newOrder = CreateOrder();
                var newItem = CreateItem();

                currentPendingOrder.Order = newOrder;
                currentPendingOrder.Items.Add(newItem);
                pendingOrders.Add(currentPendingOrder);
                tbItems.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void btnSaveAndExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Order> commitedOrders = new List<Order>();
                foreach (var pendingOrder in selectedPendingOrders)
                {
                    commitedOrders.Add(await pendingOrder.Commit());
                    pendingOrders.Remove(pendingOrder);
                }
                CSVConvertor.SavePendingOrders(commitedOrders);
                selectedPendingOrders.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnRefreshNewOrders_Click(object sender, RoutedEventArgs e)
        {
            icNewItems.ItemsSource = null;
            icNewItems.ItemsSource = pendingOrders;
            selectedPendingOrders.Clear();
        }
        private void cbPendingOrders_Checked(object sender, RoutedEventArgs e)
        {
            selectedPendingOrders.Add((PendingOrder)((CheckBox)sender).DataContext);
        }
        private void cbPendingOrders_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedPendingOrders.Remove((PendingOrder)((CheckBox)sender).DataContext);
        }
        private void btnDeleteNewOrder_Click(object sender, RoutedEventArgs e)
        {
            var result =  MessageBox.Show("Are you sure you want to delete the selected orders ?", "Delete Orders", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                foreach (var pendingOrder in selectedPendingOrders)
                {
                    pendingOrders.Remove(pendingOrder);
                }
                selectedPendingOrders.Clear();
            }
        }

        private void ResetOrder()
        {
            cbCompanyName.IsEnabled = true;
            txtCustomerName.IsEnabled = true;
            cbCompanyName.Text = "";
            cbCompanyName.SelectedItem = null;
            txtCustomerName.Text = "";
            txtOutOf.Text = "";
            ResetNewFields();
            currentPendingOrder = new PendingOrder();
        }
        private void ResetNewFields()
        {
            txtHLength.Text = "";
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtHAmount.Text = "";
            txtSgValue.Text = "";
            cbMt.Text = "";
            cbMt.SelectedItem = null;
            cbP.SelectedItem = null;
            cbParts.Text = "";
            cbParts.SelectedItem = null;
            cbSgType.Text = "";
            cbSgType.SelectedItem = null;
            cbSh.Text = "";
            cbSh.SelectedItem = null;
            cbSk.Text = "";
            cbSk.SelectedItem = null;
            cbSw.Text = "";
            cbSw.SelectedItem = null;
            cbT.Text = "";
            cbT.SelectedItem = null;
            cbNotes.Text = "";
            cbNotes.SelectedItem = null;
        }
        private OrderCreate CreateOrder()
        {
            return new OrderCreate(cbCompanyName.Text, txtCustomerName.Text);
        }
        private ItemCreate CreateItem()
        {
                return new ItemCreate(txtWidth.Text, txtHeight.Text, cbSw.Text, cbSk.Text,
                cbT.Text, cbP.Text, cbSh.Text, cbMt.Text, txtSgValue.Text, cbSgType.Text, txtHLength.Text, int.Parse(txtHAmount.Text),
                cbParts.Text, cbNotes.Text);
        }
        #endregion
    }
}
