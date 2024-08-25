using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class ServerInfo
    {
        public string System { get; set; }
        public string Hostname { get; set; }
        public string PrivateIp { get; set; }
        public DateTime DatabaseCreationTime { get; set; }
        DateTime StartUpTime { get; set; }
        public int RunningTime { get; set; }

        public ServerInfo(string system, string hostname, string private_ip, DateTime database_creation_time, DateTime start_up_time, int running_time)
        {
            System = system;
            Hostname = hostname;
            PrivateIp = private_ip;
            DatabaseCreationTime = database_creation_time;
            StartUpTime = start_up_time;
            RunningTime = running_time;
        }
    }
}
