using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary.FieldsFormat
{
    public class SG
    {
        public int Number { get; set; }
        public string Direction { get; set; }

        public override string ToString()
        {
            return $"{Number} {Direction}";
        }
    }
}
