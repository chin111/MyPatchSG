using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class LKWk
    {
        public string FIELD_ID { get; set; }
        public string WK { get; set; }

        public LKWk()
        {

        }

        public string getFieldID()
        {
            return this.FIELD_ID != null ? this.FIELD_ID : "";
        }

        public void setFieldID(string fieldID)
        {
            this.FIELD_ID = fieldID;
        }

        public string getWK()
        {
            return this.WK != null ? this.WK : "";
        }

        public void setWK(string Wk)
        {
            this.WK = Wk;
        }
    }
}
