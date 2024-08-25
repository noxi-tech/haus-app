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
        static public string GetOrdersURL(string company,string customer, string status, int skip, int limit)
        {
            string extention = $"?company={company}&customer={customer}&state={status}&skip={skip}&limit={limit}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }
        static public string GetOrderURL(int orderId)
        {
            string extention = $"/{orderId}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }        
        static public string SetOrderDeliveryURL(int orderId)
        {
            string extention = $"/{orderId}/deliver";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }
        static public string SetOrderBillURL(int orderId)
        {
            string extention = $"/{orderId}/bill";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["OrdersApi"] + extention;
        }
        #endregion

        #region Company Region
        static public string GetCompaniesURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["CompaniesApi"];
        }
        #endregion

        #region Timekeeper region
        static public string TimekeeperCheckinURL(long employeeId)
        {
            string extention = $"/checkin/{employeeId}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["TimekeeperApi"] + extention;
        }
        static public string TimekeeperCheckoutURL(long employeeId)
        {
            string extention = $"/checkout/{employeeId}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["TimekeeperApi"] + extention;
        }
        static public string TimekeeperReportURL(long employeeId, int month, int year)
        {
            string extention = $"/report?employee_id={employeeId}&month={month}&year={year}";
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["TimekeeperApi"] + extention;
        }
        #endregion

        #region Connection Info
        static public string Ping(string url)
        {
            return url + "/ping";
        }
        static public string Server()
        {
            return ConfigurationManager.AppSettings["Root"] + "/server";
        }
        #endregion
    }
}
