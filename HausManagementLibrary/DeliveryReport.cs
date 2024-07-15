using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    [Serializable]
    public class DeliveryReport
    {
        private string delivered_by;
        public string DeliveredBy
        {
            get { return delivered_by; }
            set { delivered_by = value; }
        }

        public DeliveryReport(string delivered_by)
        {
            DeliveredBy = delivered_by;
        }
    }
}
