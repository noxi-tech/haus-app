using HausManagementLibrary;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HausManagementUI
{
    /// <summary>
    /// Interaction logic for BarcodeDialog.xaml
    /// </summary>
    public partial class BarcodeDialog : Window
    {
        DataAccessor data = new DataAccessor();
        Employee employee;
        public static int barcode;
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));

        public BarcodeDialog(Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            txtBarcode.DataContext = barcode;
        }

        private async void ProcessBarcode()
        {
            txtBarcode.IsEnabled = false;
            cardLoading.Visibility = Visibility.Visible;

            long itemId;
            if (!long.TryParse(txtBarcode.Text, out itemId))
            {
                IncorrectInput();
                ShowMessage("Barcode can include numbers only.");
                return;
            };

            try
            {
                employee = await data.AssignItem(itemId, employee);
                CorrectInput();
            }
            catch (Exception e)
            {
                IncorrectInput();
                ShowMessage(e.Message);
            }
        }
        private void txtBarcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessBarcode();
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public async void IncorrectInput()
        {
            pgLoading.Visibility = Visibility.Collapsed;
            icoScanSatus.Kind = PackIconKind.WindowClose;
            icoScanSatus.Foreground = Brushes.DarkRed;
            await Task.Run(() => Thread.Sleep(1000));
            cardLoading.Visibility = Visibility.Collapsed;
            txtBarcode.Visibility = Visibility.Visible;
            icoScanSatus.Kind = PackIconKind.Upload;
            icoScanSatus.Foreground = Brushes.Black;
            txtBarcode.Text = string.Empty;
            txtBarcode.IsEnabled = true;
            txtBarcode.Focus();
        }
        public async void CorrectInput()
        {
            pgLoading.Visibility = Visibility.Collapsed;
            icoScanSatus.Kind = PackIconKind.Check;
            icoScanSatus.Foreground = Brushes.Green;
            await Task.Run(() => Thread.Sleep(1000));
            this.Close();
        }
        private void ShowMessage(string msg)
        {
            snbMessage.MessageQueue = messageQueue;
            snbMessage.MessageQueue.Enqueue(msg, true);
        }
    }
}
