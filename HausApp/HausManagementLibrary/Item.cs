using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    public class Item : ItemCreate
    {
        private int id;
        private string stage;
        #region Fields
        public int Id
        { 
            get { return id; }
            set { id = value; }
        }
        public string Stage
        {
            get { return stage; }
            set { stage = value; }
        }
        #endregion

        public Item(int id, string stage, int width, int height, string sw,
            string sk, string t, string p, int sh, string mt, int sg_value, string sg_type, int ht_length, int ht_amount, string notes, string parts, int order_id)
            : base(width, height, sw, sk, t, p, sh, mt, sg_value, sg_type, ht_length, ht_amount, parts, notes, order_id)
        {
            Id = id;
            Stage = stage;
        }
    }
}
