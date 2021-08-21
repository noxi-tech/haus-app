using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HausManagementLibrary.FieldsFormat;

namespace HausManagementLibrary
{
    public class Item
    {
        #region Fields
        public int Id { get; set; }
        public string Stage { get; set; }
        public string CreatedAt { get; set; }
        public string CompanyName { get; set; }
        public string CustomerName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Sw { get; set; }
        public string Sk { get; set; }
        public string T { get; set; }
        public P P { get; set; }
        public int Sh { get; set; }
        public string Mt { get; set; }
        public SG Sg { get; set; }
        public OutOf OutOf { get; set; }
        public HT Ht { get; set; }
        public string Notes { get; set; }
        public string Pieces { get; set; }
        #endregion

        public Item(int id, string stage, string created_at,string companyName, string customerName,
            int width, int height, string sw, string sk,string t, P p, int sH,string mT, SG sG, OutOf outOf, HT hT, string notes, string pieces)
        {
            Id = id;
            Stage = stage;
            CreatedAt = created_at;
            CompanyName = companyName;
            CustomerName = customerName;
            Width = width;
            Height = height;
            Sw = sw;
            Sk = sk;
            T = t;
            P = p;
            Sh = sH;
            Mt = mT;
            Sg = sG;
            OutOf = outOf;
            Ht = hT;
            Notes = notes;
            Pieces = pieces;
        }
    }
}
