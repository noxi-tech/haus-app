using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class ClockRecord
    {
        public DateTime DateTime { get; set; }
        public long EmployeeId { get; set; }
        public string Clocked { get; set; }
        
        public ClockRecord(string date_time, long employee_id, string clocked) {
            DateTime utcDateTime = DateTime.Parse(date_time, null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime = utcDateTime.ToLocalTime();
            EmployeeId = employee_id;
            Clocked = clocked;
        }
    }
}
