using HausManagementLibrary;
using HausManagementLibrary.FieldsFormat;
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
        List<Item> items = new List<Item>();

        public ItemsView()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                items.Add(new Item(i, $"Test{i}", $"{DateTime.Now}", $"AssadTech{i}", $"Customer{i}", 10 * i, 10 * i, $"SW{i}", $"Sk{i}", $"T{i}", new P() { Value = 123.01f }, i, $"MT{i}", new SG() { Number = i, Direction = "R+L" }, new OutOf() { Index = 1, Total = 5 }, new HT() { H = i, T = i }, $"Notes {i}", $"Pieces {i}"));
            }

            InitializeItems();
            this.DataContext = this;
        }
        private async void InitializeItems()
        {
            try
            {
                //grdItems.ItemsSource = await data.GetItems();
                grdItems.ItemsSource = items;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
