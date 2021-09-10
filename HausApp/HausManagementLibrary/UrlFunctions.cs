using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace HausManagementLibrary
{
    public static class UrlFunctions
    {
        #region Employee Region
        static public string CreateEmployeeURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["EmployeesApi"];
        }
        static public string GetEmployeesURL(int skip, int limit)
        {
            string extention = $"?skip={skip}&limit={limit}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["EmployeesApi"] + extention;
        }
        static public string GetEmployeeURL(long employeeId)
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["EmployeesApi"] + $"/{employeeId}";
        }
        static public string DeleteEmployeeURL(long employeeId)
        {
            return GetEmployeeURL(employeeId) + "/delete";
        }
        static public string AssignEmployeeURL(long employeeId, long itemId)
        {
            return GetEmployeeURL(employeeId) + $"/assign?item_id={itemId}";
        }
        #endregion

        #region Item Region
        static public string CreateItemURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"];
        }
        static public string GetItemsURL(List<string> stages, int skip, int limit)
        {
            string extention = "?";
            for (int i = 0; i < stages.Count; i++)
            {
                extention += $"stages={stages[i]}&";
            }
            extention += $"skip={skip}&limit={limit}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"] + extention;
        }
        static public string GetItemURL(long itemId)
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"]+ $"/{itemId}";
        }
        #endregion

        #region Order Region
        static public string CreateOrderURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"];
        }
        static public string GetOrdersURL(string customer, int skip, int limit)
        {
            string extention = $"?customer={customer}&skip={skip}&limit={limit}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }
        static public string GetOrderURL(int orderId)
        {
            string extention = $"/{orderId}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }        
        static public string SetOrderDeliveryURL(int orderId, string deliveredBy)
        {
            string extention = $"/{orderId}/deliver?delivered_to={deliveredBy}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }
        static public string SetOrderBillURL(int orderId, string billId)
        {
            string extention = $"/{orderId}/bill?bill_id={billId}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }

        #endregion

        #region Company Region
        static public string GetCompaniesURL()
        {
            return ConfigurationManager.AppSettings["Root"] + "/haus/companies";
        }
        #endregion

        #region Connection Info
        static public string Ping(string url)
        {
            return url + "/ping";
        }
        #endregion
    }
}
