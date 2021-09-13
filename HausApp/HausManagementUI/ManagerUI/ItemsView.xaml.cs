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
    /// Interaction logic for ItemsView.xaml
    /// </summary>
    public partial class ItemsView : UserControl , INotifyPropertyChanged
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<string> companies = new ObservableCollection<string>();

        #region New Items Fields
        ObservableCollection<PendingOrder> pendingOrders = new ObservableCollection<PendingOrder>();
        List<PendingOrder> selectedPendingOrders = new List<PendingOrder>();
        PendingOrder currentPendingOrder { get; set; }
        #endregion

        #region In Progress Items Fields
        List<Item> itemsInProgress = new List<Item>();
        List<string> filteredStages = new List<string>();
        #endregion

        #region Orders Fields
        ObservableCollection<Order> orders = new ObservableCollection<Order>();
        ObservableCollection<Order> deliveredOrders = new ObservableCollection<Order>();
        ObservableCollection<Order> billedOrders = new ObservableCollection<Order>();
        #endregion

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

        public ItemsView()
        {
            InitializeComponent();
            InitializeSources();
            InitializeOptions();
            this.DataContext = this;
        }

        #region Initializations region
        private void InitializeSources()
        {
            icNewItems.ItemsSource = pendingOrders;
            icOrders.ItemsSource = orders;
            grdInProgressItems.ItemsSource = itemsInProgress;
            icOrdersInBilling.ItemsSource = billedOrders;
            icOrdersInDelivery.ItemsSource = deliveredOrders;
            cbCompanyName.ItemsSource = companies;
            cbCompanyOrderSearch.ItemsSource = companies;
        }
        private void InitializeOptions()
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
                GetCompanies();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region Create Order/Item region
        private void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            tbItems.SelectedIndex = 1;
            ResetOrder();
        }
        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = CreateItem();

                cbCompanyName.IsEnabled = false;
                txtCustomerName.IsEnabled = false;
                currentPendingOrder.Items.Add(newItem);
                txtOutOf.Text = $"Total: {currentPendingOrder.Items.Count}";
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
                GetCompanies();
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
            if (!(selectedPendingOrders == null))
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected orders ?", "Delete Orders", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var pendingOrder in selectedPendingOrders)
                    {
                        pendingOrders.Remove(pendingOrder);
                    }
                    selectedPendingOrders.Clear();
                }
            }
        }
        private async void GetCompanies()
        {
            List<string> fetchedCompanies = await data.GetCompanies();
            companies.Clear();
            foreach (var company in fetchedCompanies)
            {
                companies.Add(company);
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

        #region ItemsInProgress region
        private void grdInProgressItems_Sorting(object sender, DataGridSortingEventArgs e)
        {
            //MessageBox.Show("Is Working? ");
        }
        private async void RefreshItemsInProgress()
        {
            try
            {
                itemsInProgress = await data.GetItems(filteredStages);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            grdInProgressItems.ItemsSource = itemsInProgress;
        }
        private void btnRefreshInProgressItems_Click(object sender, RoutedEventArgs e)
        {
            RefreshItemsInProgress();
        }
        private void Filter_Checked(object sender, RoutedEventArgs e)
        {
            filteredStages.Add((string)((CheckBox)sender).Tag);
        }
        private void Filter_Unchecked(object sender, RoutedEventArgs e)
        {
            filteredStages.Remove((string)((CheckBox)sender).Tag);
        }
        #endregion

        #region Orders region
        private async void RefreshOrders(string companyName,string customerName,string status)
        {
            if (!isLoading)
            {
                List<Order> fetchedOrders = new List<Order>();
                orders.Clear();
                IsLoading = true;
                try
                {
                    fetchedOrders = await data.GetOrders(companyName, customerName, status);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                foreach (var order in fetchedOrders)
                {
                    orders.Add(order);
                    await Task.Run(() =>
                    {
                        Thread.Sleep(200);
                    });
                }
                IsLoading = false;
            }
        }
        private void btnRefreshOrders_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrders(cbCompanyOrderSearch.Text, txtOrderCustomerSearch.Text, ((ComboBoxItem)cbOrdersStatusFilter.SelectedItem).Tag.ToString());
        }
        private void btnOpenListsTab_Click(object sender, RoutedEventArgs e)
        {
            tbItems.SelectedIndex = 4;
        }
        private void btnAddOrderToDelivered_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)((Button)sender).DataContext;
            if (!deliveredOrders.Contains(order))
            {
                deliveredOrders.Add(order);
            }
            else
            {
                MessageBox.Show("This order is already in the list.");
            }
        }
        private void btnAddOrderToBilled_Click(object sender, RoutedEventArgs e)
        {
            var order = (Order)((Button)sender).DataContext;
            if (!billedOrders.Contains(order)) 
            {
                billedOrders.Add(order);
            }
            else
            {
                MessageBox.Show("This order is already in the list.");
            }
        }
        private void btnClearDeliveryList_Click(object sender, RoutedEventArgs e)
        {
            deliveredOrders.Clear();
        }
        private void btnClearBillList_Click(object sender, RoutedEventArgs e)
        {
            billedOrders.Clear();
        }
        private void btnAcceptDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (deliveredOrders.Count != 0)
            {
                foreach (var deliveredOrder in deliveredOrders)
                {
                    data.SetOrderDelivery(deliveredOrder.Id, txtDeliveryPerson.Text);
                }
                deliveredOrders.Clear();
            }
            else
            {
                MessageBox.Show("List must have orders.");
            }

            txtDeliveryPerson.Text = "";
            dhDelivery.IsOpen = false;
        }
        private void btnCloseDelivery_Click(object sender, RoutedEventArgs e)
        {
            dhDelivery.IsOpen = false;
        }
        private void btnAcceptBill_Click(object sender, RoutedEventArgs e)
        {
            if (billedOrders.Count != 0)
            {
                foreach (var billedOrder in billedOrders)
                {
                    data.SetOrderBill(billedOrder.Id, txtBillId.Text);
                }
                billedOrders.Clear();
            }
            else
            {
                MessageBox.Show("List must have orders.");
            }
            txtBillId.Text = "";
            dhBill.IsOpen = false;
        }
        private void btnCloseBill_Click(object sender, RoutedEventArgs e)
        {
            dhBill.IsOpen = false;
        }
        #endregion

        #region RefreshAll
        private void tbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((TabControl)sender).SelectedIndex)
            {
                case 2:
                    RefreshItemsInProgress();
                    break;
                case 3:
                    if (cbCompanyOrderSearch.SelectedItem is null)
                    {
                        RefreshOrders(cbCompanyOrderSearch.Text, txtOrderCustomerSearch.Text, ((ComboBoxItem)cbOrdersStatusFilter.SelectedItem).Tag.ToString());
                    }
                    else
                    {
                        RefreshOrders(cbCompanyOrderSearch.SelectedItem.ToString(), txtOrderCustomerSearch.Text, ((ComboBoxItem)cbOrdersStatusFilter.SelectedItem).Tag.ToString());
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
