using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace HausManagementLibrary
{
    public class DataAccessor
    {
        #region Employees Region
        //Get all the employees from the DB.
        public async Task<ObservableCollection<Employee>> GetEmployees()
        {
            return await Task.Run( async () =>  {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(UrlFunctions.GetEmployeesURL(0,100));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<ObservableCollection<Employee>>();
                    }
                    return null;
                }
            });
        }

        //Assigns an item to a specified employee taking into consideration if the employee's job is QC (sends twice).
        public async Task<Employee> AssignItem(long itemId, Employee employee)
        {
            return await Task.Run(async () => {

                using (HttpClient client = new HttpClient())
                {
                    var response1 = await client.PostAsJsonAsync<long?>(UrlFunctions.AssignEmployeeURL(employee.Id,itemId), null);
                    if (response1.IsSuccessStatusCode)
                    {
                        if (employee.Job.ToUpper() == "QC")
                        {
                            var response2 = await client.PostAsJsonAsync<long?>(UrlFunctions.AssignEmployeeURL(employee.Id,itemId), null);
                            HttpContent content2 = response2.Content;
                            return await content2.ReadAsAsync<Employee>();
                        }
                        HttpContent content = response1.Content;
                        return await content.ReadAsAsync<Employee>();
                    }
                    else
                    {
                        var dic = await response1.Content.ReadAsAsync<Dictionary<string, string>>();
                        throw new HttpRequestException(dic["detail"] + ".");
                    }
                }
            });
        }

        //Create a new employee.
        public async Task<Employee> CreateEmployee(string name, string job)
        {
            return await Task.Run(async () => {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(UrlFunctions.CreateEmployeeURL(), new EmployeeCreate(name, job));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<Employee>();
                    }
                    return null;
                }
            });
        }
        #endregion

        #region Items Region
        public async Task<List<Item>> GetItems(List<string> stages)
        {
            return await Task.Run(async () => {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(UrlFunctions.GetItemsURL(stages, 0, 100));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<Item>>();
                    }
                    return null;
                }
            });
        }
        public async Task<Item> CreateItem(ItemCreate itemCreate)
        {
            return await Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(UrlFunctions.CreateItemURL(), itemCreate);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<Item>();
                    }
                    return null;
                }
            });
        }
        #endregion

        #region Orders Region
        public async Task<Order> CreateOrder(OrderCreate order)
        {
            return await Task.Run(async () => {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(UrlFunctions.CreateOrderURL(), order);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<Order>();
                    }
                    return null;
                }
            });
        }
        public async Task<List<Order>> GetOrders(string customer)
        {
            return await Task.Run(async () => {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(UrlFunctions.GetOrdersURL(customer, 0, 100));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<Order>>();
                    }
                    return null;
                }
            });
        }
        public async Task<Order> GetOrder(int orderId)
        {
            return await Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(UrlFunctions.GetOrderURL(orderId));
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<Order>();
                    }
                    return null;
                }
            });
        }
        public async void SetOrderDelivery(int orderId, string deliveredBy)
        {
            await Task.Run(async () =>
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync<long?>(UrlFunctions.SetOrderDeliveryURL(orderId, deliveredBy), null);
                }
            });
        }


        #endregion

        #region Companies Region
        public async Task<List<string>> GetCompanies()
        {
            return await Task.Run(async () => {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(UrlFunctions.GetCompaniesURL());
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsAsync<List<string>>();
                    }
                    return null;
                }
            });
        }
        #endregion

        #region Connection Info Region
        public async Task<string> Ping(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(UrlFunctions.Ping(url));
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<string>();
                }
                return null;
            }
        }
        #endregion
    }
}
