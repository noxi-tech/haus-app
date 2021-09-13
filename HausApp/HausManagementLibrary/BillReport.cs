using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    [Serializable]
    public class BillReport
    {
        private string bill_id;
        public string BillId
        {
            get { return bill_id; }
            set { bill_id = value; }
        }
        
        public BillReport(string bill_id)
        {
            BillId = bill_id;
        }
    }
}
