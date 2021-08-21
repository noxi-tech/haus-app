using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary.FieldsFormat
{
    public class OutOf
    {
        public int Index { get; set; }
        public int Total { get; set; }

        public override string ToString()
        {
            return $"{Index}/{Total}";
        }
    }
}
