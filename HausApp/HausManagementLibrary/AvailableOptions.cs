using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public static class AvailableOptions
    {
        public static List<string> SwOptions = new List<string>() {
            "IIII",
            "XXXX",
            "====",
            "Bf",
            "PP",
            "3",
            "KR",
            "Gldr",
            "Rngs",
            "Skch",
            "Pnlm",
            "Plstk",
            "RC",
            "RR",
            "AustR",
            "AustM",
            "RGly"
            };
        public static List<string> SkOptions = new List<string>()
        {
            "Mi",
            "A",
            "Look",
            "EC",
            "ECS",
            "ECD",
            "F",
            "Skch",
            "DT",
            "AT",
            "Pnlm",
            "Hsptl",
            "C300",
            "RTf"
        };
        public static List<string> TOptions = new List<string>()
        {
            "Satin",
            "Crep",
            "BO80",
            "BO100",
            "Voile"
        };
        public static List<string> POptions = new List<string>()
        {
           "1.2",
           "1.5",
           "2",
           "2.5",
           "3",
           "3.2",
           "3.3",
           "3.5"
        };
        public static List<int> ShOptions = new List<int>()
        {
            3,
            6,
            9,
            10,
            12,
            15,
            18,
            20
        };
        public static List<string> MtOptions = new List<string>()
        {
            "min",
            "5",
            "8",
            "10",
            "15"
        };
        public static List<string> SgOptions = new List<string>()
        {
            "",
            "L",
            "R",
            "L+R"
        };
        public static List<string> PiecesOptions = new List<string>()
        {
            "",
            ">",
            "<>"
        };
    }
}
