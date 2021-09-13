using HausManagementLibrary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            txtCsvPath.Text = CurrentSettings.CsvPath;
            txtDueDateDays.Text = $"{CurrentSettings.LastOrderWarrning}";
            slOrderWarningDays.Value = CurrentSettings.OrderWarrning;
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            txtAppInfo.Text = $"All Rights Reserved To {versionInfo.CompanyName} {versionInfo.LegalCopyright}.\n{versionInfo.ProductName} Version {versionInfo.ProductVersion}";
            txtServerInfo.Text = "";//Get Server Info
        }
        private void btnChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.DefaultExt = "*.CSV";
            saveFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog.FileName))
            {
                Properties.Settings.Default.CsvPath = saveFileDialog.FileName;
                CurrentSettings.CsvPath = saveFileDialog.FileName;
                Properties.Settings.Default.Save();
                txtCsvPath.Text = CurrentSettings.CsvPath;
            }
        }
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            CurrentSettings.OrderWarrning = Convert.ToInt32(slOrderWarningDays.Value);
            CurrentSettings.LastOrderWarrning = Convert.ToInt32(txtDueDateDays.Text);
            Properties.Settings.Default.OrderWarrning = CurrentSettings.OrderWarrning;
            Properties.Settings.Default.LastOrderWarrning = CurrentSettings.LastOrderWarrning;
            Properties.Settings.Default.Save();
        }
        private void txtDueDateDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            slOrderWarningDays.Value = CurrentSettings.OrderWarrning;
            e.Handled = InputValidator.NumRegex.IsMatch(e.Text);
        }
        private void btnResetChanges_Click(object sender, RoutedEventArgs e)
        {
            slOrderWarningDays.Value = CurrentSettings.OrderWarrning;
            txtDueDateDays.Text = $"{CurrentSettings.LastOrderWarrning}";
        }
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (txtOldCode.Password == Properties.Settings.Default.PinCode)
            {
                Properties.Settings.Default.PinCode = txtNewCode.Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Incorrect Code.");
            }
            txtOldCode.Password = "";
            txtNewCode.Password = "";
            dhDelivery.IsOpen = false;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            txtOldCode.Password = "";
            txtNewCode.Password = "";
            dhDelivery.IsOpen = false;
        }
    }
}
