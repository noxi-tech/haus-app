using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary.FieldsFormat
{
    public class HT
    {
        public int H { get; set; }
        public int T { get; set; }
        public override string ToString()
        {
            return $"{H} cm * {T}";
        }
    }
}
