using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class RCSTE
    {
        public string RCS_TYPE { get; set; }
        public string FIELD_ID { get; set; }
        public string TE_ID { get; set; }
        public string FIELD_VALUE { get; set; }
        public string LAST_UPDATE { get; set; }

        public RCSTE()
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
        public string getTEID()
        {
            return this.TE_ID != null ? this.TE_ID : "";
        }
        public void setTEID(string ID)
        {
            this.TE_ID = ID;
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
