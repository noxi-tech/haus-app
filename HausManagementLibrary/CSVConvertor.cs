using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public static class CSVConvertor
    {
        private static StreamWriter sw;
        public static void CreateWriter()
        {
            sw = new StreamWriter(CurrentSettings.CsvPath, false);
        }
        public static async void SavePendingOrders(List<Order> orders)
        {
            DataTable itemsTable = new DataTable();
            BuildItemsTable(itemsTable);
            FillItemsTable(itemsTable, orders);
            await Task.Run(() =>
            {
                for (int i = 0; i < itemsTable.Columns.Count; i++)
                {
                    sw.Write(itemsTable.Columns[i]);
                    if (i < itemsTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in itemsTable.Rows)
                {
                    for (int i = 0; i < itemsTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < itemsTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            });
        }
        private static void BuildItemsTable(DataTable itemsTable)
        {
            itemsTable.Columns.Add("Id", typeof(int));
            itemsTable.Columns.Add("CompanyName", typeof(string));
            itemsTable.Columns.Add("CustomerName", typeof(string));
            itemsTable.Columns.Add("Width", typeof(string));
            itemsTable.Columns.Add("Height", typeof(string));
            itemsTable.Columns.Add("Sw", typeof(string));
            itemsTable.Columns.Add("Sk", typeof(string));
            itemsTable.Columns.Add("T", typeof(string));
            itemsTable.Columns.Add("P", typeof(string));
            itemsTable.Columns.Add("Sh", typeof(string));
            itemsTable.Columns.Add("Mt", typeof(string));
            itemsTable.Columns.Add("Sg", typeof(string));
            itemsTable.Columns.Add("Ht", typeof(string));
            itemsTable.Columns.Add("Parts", typeof(string));
            itemsTable.Columns.Add("Notes", typeof(string));
            itemsTable.Columns.Add("OutOf", typeof(string));
        }
        private static void FillItemsTable(DataTable itemsTable, List<Order> orders)
        {
            foreach (var order in orders)
            {
                var itemsInOrder = order.Items;
                for (int i = 0; i < itemsInOrder.Count; i++)
                {
                    itemsTable.Rows.Add(itemsInOrder[i].Id, order.Company, order.Customer, itemsInOrder[i].Width, itemsInOrder[i].Height, itemsInOrder[i].Sw, itemsInOrder[i].Sk, itemsInOrder[i].T, itemsInOrder[i].P,
                        itemsInOrder[i].Sh, itemsInOrder[i].Mt, itemsInOrder[i].Sg, itemsInOrder[i].Ht, itemsInOrder[i].Parts, itemsInOrder[i].Notes, $"{i + 1} out of {itemsInOrder.Count}");
                }
            }
        }
    }
}
