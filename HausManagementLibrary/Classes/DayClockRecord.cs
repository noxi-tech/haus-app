using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class DayClockRecord
    {
        public DateTime DateTime { get; set; }
        public List<GroupedClockRecord> GroupedClockRecords{ get; set; }
        public double TotalWorkingHours
        {
            get
            {
                double total = 0;
                foreach (var record in GroupedClockRecords)
                {
                    total += record.TotalHoursWorked;
                }
                return total;
            }
        }

        public DayClockRecord() { }

        public DayClockRecord(DateTime date_time, List<GroupedClockRecord> grouped_clock_records)
        {
            DateTime = date_time;
            GroupedClockRecords = grouped_clock_records;
        }

        public DayClockRecord(DateTime date_time, List<ClockRecord> clock_records)
        {
            DateTime = date_time;
            GroupedClockRecords = new List<GroupedClockRecord>();
            var orderedClockRecord = clock_records.OrderBy(e => e.DateTime).ToList();
            GroupedClockRecord groupedClockRecord = new GroupedClockRecord();
            var lastClock = "";

            for (int i = 0; i < orderedClockRecord.Count; i++)
            {
                if (orderedClockRecord[i].Clocked == "in")
                {
                    if (lastClock == "in")
                    {
                        // in -> missing "out"
                        groupedClockRecord.ClockedOut = new DateTime();
                        GroupedClockRecords.Add(groupedClockRecord);
                        groupedClockRecord = new GroupedClockRecord();
                        groupedClockRecord.ClockedIn = orderedClockRecord[i].DateTime;
                    }
                    else
                    {
                        // empty or out -> correct scenario
                        groupedClockRecord.ClockedIn = orderedClockRecord[i].DateTime;
                    }
                    if (i == orderedClockRecord.Count - 1)
                    {
                        groupedClockRecord.ClockedOut = new DateTime();
                        GroupedClockRecords.Add(groupedClockRecord);
                    }
                }
                else
                {
                    if (lastClock == "in")
                    {
                        // in -> correct scenario
                        groupedClockRecord.ClockedOut = orderedClockRecord[i].DateTime;
                        GroupedClockRecords.Add(groupedClockRecord);
                        groupedClockRecord = new GroupedClockRecord();
                    }
                    else
                    {
                        // empty or out -> missing "in"
                        groupedClockRecord.ClockedIn = new DateTime();
                        groupedClockRecord.ClockedOut = orderedClockRecord[i].DateTime;
                        GroupedClockRecords.Add(groupedClockRecord);
                        groupedClockRecord = new GroupedClockRecord();
                    }

                }
                lastClock = orderedClockRecord[i].Clocked;
            }
        }
    }
}
