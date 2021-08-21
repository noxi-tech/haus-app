using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary.FieldsFormat
{
    public class P
    {
        public float Value { get; set; }

        public override string ToString()
        {
            return $"P* {Value}";
        }
    }
}
