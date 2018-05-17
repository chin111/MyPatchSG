using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class vwSalesTEChart
    {
        public string WK { get; set; }
        public string SALES_TYPE { get; set; }
        public string SKU_CODE { get; set; }
        public double VOL_BASE { get; set; }
        public double VOL_WK { get; set; }

        public vwSalesTEChart()
        {

        }

        public string getWK()
        {
            return this.WK != null ? this.WK : "";
        }

        public void setWK(string wk)
        {
            this.WK = wk;
        }

        public string getSalesType()
        {
            return this.SALES_TYPE != null ? this.SALES_TYPE : "";
        }

        public void setSalesType(string salesType)
        {
            this.SALES_TYPE = salesType;
        }

        public string getSKUCode()
        {
            return this.SKU_CODE != null ? this.SKU_CODE : "";
        }

        public void setSKUCode(string code)
        {
            this.SKU_CODE = code;
        }

        public double getVolBase()
        {
            return this.VOL_BASE;
        }

        public void setVolBase(double volBase)
        {
            this.VOL_BASE = volBase;
        }

        public double getVolWK()
        {
            return this.VOL_WK;
        }

        public void setVolWK(double volWK)
        {
            this.VOL_WK = volWK;
        }
    }
}
