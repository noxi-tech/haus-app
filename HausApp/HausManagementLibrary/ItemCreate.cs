using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausManagementLibrary
{
    [Serializable]
    public class ItemCreate
    {
        #region Private Fields
        private int width;
        private int height;
        private string sw;
        private string sk;
        private string t;
        private string p;
        private int sh;
        private string mt;
        private int sg_value;
        private string sg_type;
        private int ht_length;
        private int ht_amount;
        private string notes;
        private string parts;
        private int? order_id;
        #endregion

        #region Properties
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public string Sw
        {
            get { return sw; }
            set { sw = value; }
        }
        public string Sk
        {
            get { return sk; }
            set { sk = value; }
        }
        public string T
        {
            get { return t; }
            set { t = value; }
        }
        public string P
        {
            get { return $"P* {p}"; }
        }
        public int Sh
        {
            get { return sh; }
            set { sh = value; }
        }
        public string Mt
        {
            get { return mt; }
            set { mt = value; }
        }
        public string Sg
        {
            get { return $"{sg_value} {sg_type}"; }
        }
        public string Ht
        {
            get { return $"{ht_length} cm * {ht_amount}"; }
        }
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        public string Parts
        {
            get { return parts; }
            set { parts = value; }
        }
        public int? OrderId
        {
            get { return order_id; }
            set { order_id = value; }
        }
        #endregion

        public ItemCreate(int width, int height, string sw, string sk, string t, string p, int sh, string mt, int sg_value, string sg_type, int ht_length, int ht_amount, string parts, string notes, int? order_id = null)
        {
            Width = width;
            Height = height;
            Sw = sw;
            Sk = sk;
            T = t;
            this.p = p;
            Sh = sh;
            Mt = mt;
            this.sg_value = sg_value;
            this.sg_type = sg_type;
            this.ht_length = ht_length;
            this.ht_amount = ht_amount;
            Notes = notes;
            Parts = parts;
            OrderId = order_id;
        }
    }
}
