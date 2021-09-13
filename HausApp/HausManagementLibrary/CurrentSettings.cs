using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public static class CurrentSettings
    {
        public static string Root { get; set; }
        public static string CsvPath { get; set; }
        public static int OrderWarrning { get; set; }
        public static int LastOrderWarrning { get; set; }
    }
}
