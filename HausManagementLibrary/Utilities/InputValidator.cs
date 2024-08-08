using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public static class InputValidator
    {
        static public readonly Regex NumRegex = new Regex("[^0-9]+");//Regex for numbers used to check numbers validation

    }
}
