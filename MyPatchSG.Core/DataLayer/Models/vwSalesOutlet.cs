using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class vwSalesOutlet
    {
        public string SALES_TYPE { get; set; }
        public string SKU_CODE { get; set; }
        public string CUSTOMER_ID { get; set; }
        public double VOL_BASE { get; set; }
        public double VOL_AVG { get; set; }
        public double VOL_PER { get; set; }
        public double VOL_01 { get; set; }
        public double VOL_02 { get; set; }
        public double VOL_03 { get; set; }
        public double VOL_04 { get; set; }
        public double VOL_05 { get; set; }
        public double VOL_06 { get; set; }
        public double VOL_07 { get; set; }
        public double VOL_08 { get; set; }
        public double VOL_09 { get; set; }
        public double VOL_TOTAL { get; set; }

        public vwSalesOutlet()
        {

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
        public string getCustomerID()
        {
            return this.CUSTOMER_ID != null ? this.CUSTOMER_ID : "";
        }
        public void setCustomerID(string ID)
        {
            this.CUSTOMER_ID = ID;
        }
        public double getVolBase()
        {
            return this.VOL_BASE;
        }
        public string getVolBaseString()
        {
            return string.Format("{0:0.##}", this.VOL_BASE);
        }
        public void setVolBase(double value)
        {
            this.VOL_BASE = value;
        }
        public double getVolAvg()
        {
            return this.VOL_AVG;
        }
        public string getVolAvgString()
        {
            return string.Format("{0:0.##}", this.VOL_AVG);
        }
        public void setVolAvg(double value)
        {
            this.VOL_AVG = value;
        }
        public double getVolPer()
        {
            return this.VOL_PER;
        }
        public string getVolPercentString()
        {
            double per = this.VOL_PER * 100;
            return string.Format("{0:0.#}", per);
        }
        public void setVolPer(double value)
        {
            this.VOL_PER = value;
        }
        public double getVol01()
        {
            return this.VOL_01;
        }
        public string getVol01String()
        {
            return string.Format("{0:0.##}", this.VOL_01);
        }
        public void setVol01(double value)
        {
            this.VOL_01 = value;
        }
        public double getVol02()
        {
            return this.VOL_02;
        }
        public string getVol02String()
        {
            return string.Format("{0:0.##}", this.VOL_02);
        }
        public void setVol02(double value)
        {
            this.VOL_02 = value;
        }
        public double getVol03()
        {
            return this.VOL_03;
        }
        public string getVol03String()
        {
            return string.Format("{0:0.##}", this.VOL_03);
        }
        public void setVol03(double value)
        {
            this.VOL_03 = value;
        }
        public double getVol04()
        {
            return this.VOL_04;
        }
        public string getVol04String()
        {
            return string.Format("{0:0.##}", this.VOL_04);
        }
        public void setVol04(double value)
        {
            this.VOL_04 = value;
        }
        public double getVol05()
        {
            return this.VOL_05;
        }
        public string getVol05String()
        {
            return string.Format("{0:0.##}", this.VOL_05);
        }
        public void setVol05(double value)
        {
            this.VOL_05 = value;
        }
        public double getVol06()
        {
            return this.VOL_06;
        }
        public string getVol06String()
        {
            return string.Format("{0:0.##}", this.VOL_06);
        }
        public void setVol06(double value)
        {
            this.VOL_06 = value;
        }
        public double getVol07()
        {
            return this.VOL_07;
        }
        public string getVol07String()
        {
            return string.Format("{0:0.##}", this.VOL_07);
        }
        public void setVol07(double value)
        {
            this.VOL_07 = value;
        }
        public double getVol08()
        {
            return this.VOL_08;
        }
        public string getVol08String()
        {
            return string.Format("{0:0.##}", this.VOL_08);
        }
        public void setVol08(double value)
        {
            this.VOL_08 = value;
        }
        public double getVol09()
        {
            return this.VOL_09;
        }
        public string getVol09String()
        {
            return string.Format("{0:0.##}", this.VOL_09);
        }
        public void setVol09(double value)
        {
            this.VOL_09 = value;
        }
        public double getVolTotal()
        {
            return this.VOL_TOTAL;
        }
        public string getVolTotalString()
        {
            return string.Format("{0:0.##}", this.VOL_TOTAL);
        }
        public void setVolTotal(double value)
        {
            this.VOL_TOTAL = value;
        }
    }
}
