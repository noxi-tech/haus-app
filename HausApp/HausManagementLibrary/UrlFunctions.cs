using System.Configuration;

namespace HausManagementLibrary
{
    public static class UrlFunctions
    {
        #region Employee Region
        static public string GetEmployeesURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["EmployeesApi"];
        }
        static public string CreateEmployeeURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["EmployeesApi"];
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
        static public string GetItemsURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"];
        }
        static public string CreateItemURL()
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"];
        }
        static public string GetItemURL(long itemId)
        {
            return ConfigurationManager.AppSettings["Root"] + ConfigurationManager.AppSettings["ItemsApi"]+ $"/{itemId}";
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
