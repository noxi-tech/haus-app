using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class GroupedClockRecord
    {
        public DateTime ClockedIn { get; set; }
        public DateTime ClockedOut { get; set; }
        public string ClockedInDisplay
        {
            get
            {
                if (ClockedIn == default)
                {
                    return "missing";
                }
                else
                {
                    return ClockedIn.ToString("HH:mm:ss");
                }
            }
        }
        public string ClockedOutDisplay
        {
            get
            {
                if (ClockedOut == default)
                {
                    return "missing";
                }
                else
                {
                    return ClockedOut.ToString("HH:mm:ss");
                }
            }
        }
        public double TotalHoursWorked
        {
            get
            {
                if(ClockedIn != default && ClockedOut != default)
                {
                    TimeSpan diff = ClockedOut - ClockedIn;
                    return diff.TotalHours;
                }
                else
                {
                    return 0;
                }
            }
        }

        public GroupedClockRecord()
        {
            ClockedIn = new DateTime();
            ClockedOut = new DateTime();
        }
    }
}
