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
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl, INotifyPropertyChanged
    {
        DataAccessor data = new DataAccessor();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ObservableCollection<DayClockRecord> clockRecords = new ObservableCollection<DayClockRecord>();
        List<long> selectedEmployees = new List<long>();

        #region Notify Property Fields
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


        double totalHoursWorkedInMonth = 0; 
        public double TotalHoursWorkedInMonth
        {
            get { return totalHoursWorkedInMonth; }
            set { totalHoursWorkedInMonth = value; OnPropertyChanged("TotalHoursWorkedInMonth"); }
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

        public EmployeesView()
        {
            InitializeComponent();
            InitializeOptions();
            InitializeSources();

            LoadEmployees();
            this.DataContext = this;
        }

        private void InitializeOptions()
        {
            cbEmployeeSearch.ItemsSource = employees;
            cbMonthSearch.ItemsSource = AvailableOptions.MonthsOptions;
        }
        private void InitializeSources()
        {
            grdEmployees.ItemsSource = employees;
            icTimeKeeper.ItemsSource = clockRecords;
            
        }

        private void btnRefreshClocks_Click(object sender, RoutedEventArgs e)
        {
            LoadClockRecords();
        }
        private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee newEmployee = await data.CreateEmployee(txtEmployeeName.Text, cbJob.Text);
                employees.Add(newEmployee);
                txtEmployeeName.Text = string.Empty;
                cbJob.SelectedItem = null;
                tbEmployees.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSwitchToAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            tbEmployees.SelectedIndex = 2;
        }
        private void btnRefreshEmployees_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
        private void cbEmployeeSelect_Checked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Add(((Employee)((CheckBox)sender).DataContext).Id);
        }
        private void cbEmployeeSelect_Unchecked(object sender, RoutedEventArgs e)
        {
            selectedEmployees.Remove(((Employee)((CheckBox)sender).DataContext).Id);
        }
        private async void btnDeleteEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployees.Count > 0)
            {
                foreach (var employeeId in selectedEmployees)
                {
                    await data.DeleteEmployee(employeeId);
                }
                LoadEmployees();
            }
        }

        private async void LoadEmployees()
        {
            if (!isLoading)
            {
                selectedEmployees.Clear();
                List<Employee> fetchedEmployees = new List<Employee>();
                employees.Clear();
                IsLoading = true;
                try
                {
                    fetchedEmployees = await data.GetEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                foreach (var employee in fetchedEmployees)
                {
                    employees.Add(employee);
                }
                IsLoading = false;
            }
        }
        private async void LoadClockRecords()
        {
            if (!isLoading)
            {
                List<ClockRecord> fetchedClockRecord = new List<ClockRecord>();
                clockRecords.Clear();
                IsLoading = true;
                try
                {
                    long employeeId = 0;
                    var month = 0;
                    var year = 0;
                    if (cbEmployeeSearch.SelectedValue !=  null)
                    {
                        employeeId = (long)cbEmployeeSearch.SelectedValue;
                        if (cbMonthSearch.SelectedValue != null)
                        {
                            month = (int)cbMonthSearch.SelectedValue;
                            //if (cbYearSearch.SelectedValue != null)
                            //{
                            //    year = (int)cbYearSearch.SelectedValue;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Select a year");
                            //    return;
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Select a month");
                            IsLoading = false;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select an employee");
                        IsLoading = false;
                        return;
                    }
                    
                    fetchedClockRecord = await data.TimekeeperReport(employeeId, month, 2024);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                var groupedRecords = GroupDailyRecords(fetchedClockRecord);
                var groupedClockRecords = ConvertRecords(groupedRecords);
                double total = 0;
                foreach (var record in groupedClockRecords)
                {
                    clockRecords.Add(record);
                    total += record.TotalWorkingHours;
                }
                TotalHoursWorkedInMonth = total;

            }
            IsLoading = false;
        } 
        private Dictionary<DateTime, List<ClockRecord>> GroupDailyRecords(List<ClockRecord> fetchedClockRecord)
        {
            Dictionary<DateTime, List<ClockRecord>> result = new Dictionary<DateTime, List<ClockRecord>>();
            foreach (var record in fetchedClockRecord)
            {
                if (result.ContainsKey(record.DateTime.Date))
                {
                    result[record.DateTime.Date].Add(record);
                }
                else
                {
                    List<ClockRecord> newList = new List<ClockRecord>();
                    newList.Add(record);
                    result.Add(record.DateTime.Date, newList);
                }
            }

            return result;
        }
        private List<DayClockRecord> ConvertRecords(Dictionary<DateTime, List<ClockRecord>> groupedRecords)
        {
            List<DayClockRecord> records = new List<DayClockRecord>();
            foreach (var pair in groupedRecords)
            {
                records.Add(new DayClockRecord(pair.Key, pair.Value));
            }
            return records;
        }
    }
}
