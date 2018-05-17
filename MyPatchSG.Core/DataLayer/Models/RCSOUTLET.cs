using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class RCSOUTLET
    {
        public string RCS_TYPE { get; set; }
        public string FIELD_ID { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string FIELD_VALUE { get; set; }
        public string LAST_UPDATE { get; set; }
        public RCSOUTLET()
        {

        }

        public string getRCSTYPE()
        {
            return this.RCS_TYPE != null ? this.RCS_TYPE : "";
        }
        public void setRCSTYPE(string RCSTYPE)
        {
            this.RCS_TYPE = RCSTYPE;
        }
        public string getFIELDID()
        {
            return this.FIELD_ID != null ? this.FIELD_ID : "";
        }
        public void setFIELDID(string ID)
        {
            this.FIELD_ID = ID;
        }
        public string getCustomerID()
        {
            return this.CUSTOMER_ID != null ? this.CUSTOMER_ID : "";
        }
        public void setCustomerID(string ID)
        {
            this.CUSTOMER_ID = ID;
        }
        public string getFIELDVALUE()
        {
            return this.FIELD_VALUE != null ? this.FIELD_VALUE : "";
        }
        public void setFIELDVALUE(string Value)
        {
            this.FIELD_VALUE = Value;
        }
        public string getLASTUPDATE()
        {
            return this.LAST_UPDATE != null ? this.LAST_UPDATE : "";
        }
        public void setLASTUPDATE(string UpdateDate)
        {
            this.LAST_UPDATE = UpdateDate;
        }
    }
}
