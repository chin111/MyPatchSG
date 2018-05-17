using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class vwOutletList
    {
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public double FIS_TARGET { get; set; }
        public double FIS_SALES { get; set; }
        public double FIS_BAL { get; set; }
        public double FIS_PER { get; set; }
        public double FIS_MR { get; set; }
        public double FIS_MR_PER { get; set; }
        public double HP_TARGET { get; set; }
        public double HP_SALES { get; set; }
        public double HP_BAL { get; set; }
        public double HP_PER { get; set; }
        public double HP_MR { get; set; }
        public double HP_MR_PER { get; set; }
        public string P_01_CODE { get; set; }
        public string P_02_CODE { get; set; }
        public string P_03_CODE { get; set; }
        public string P_04_CODE { get; set; }
        public string P_05_CODE { get; set; }
        public string P_01_COLOR { get; set; }
        public string P_02_COLOR { get; set; }
        public string P_03_COLOR { get; set; }
        public string P_04_COLOR { get; set; }
        public string P_05_COLOR { get; set; }
        public string OUTLET_REMARK { get; set; }
        public string OUTLET_REMARK_DATE { get; set; }

        public vwOutletList()
        {
            
        }

        public string getCustomerID() { return this.CUSTOMER_ID; }
        public void setCustomerID(string CustomerID) { this.CUSTOMER_ID = CustomerID; }

        public string getCustomerName() { return this.CUSTOMER_NAME; }
        public void setCustomerName(string CustomerName) { this.CUSTOMER_NAME = CustomerName; }

        public string getFISTarget() { return this.FIS_TARGET.ToString(); }
        public void setFISTarget(double Target) { this.FIS_TARGET = Target; }

        public string getFISSales() { return this.FIS_SALES.ToString(); }
        public void setFISSales(double Sales) { this.FIS_SALES = Sales; }

        public string getFISPer() { return this.FIS_PER.ToString(); }
        public void setFISPer(double Per) { this.FIS_PER = Per; }

        public string getFISBal() { return this.FIS_BAL.ToString(); }
        public void setFISBal(double Bal) { this.FIS_BAL = Bal; }

        public string getFISMr() { return this.FIS_MR.ToString(); }
        public void setFISMr(double Mr) { this.FIS_MR = Mr; }

        public string getFISMrPer() { return this.FIS_MR_PER.ToString(); }
        public void setFISMrPer(double MrPer) { this.FIS_MR_PER = MrPer; }

        public string getHPTarget() { return this.HP_TARGET.ToString(); }
        public void setHPTarget(double Target) { this.HP_TARGET = Target; }

        public string getHPSales() { return this.HP_SALES.ToString(); }
        public void setHPSales(double Sales) { this.HP_SALES = Sales; }

        public string getHPPer() { return this.HP_PER.ToString(); }
        public void setHPPer(double Per) { this.HP_PER = Per; }

        public string getHPBal() { return this.HP_BAL.ToString(); }
        public void setHPBal(double Bal) { this.HP_BAL = Bal; }

        public string getHPMr() { return this.HP_MR.ToString(); }
        public void setHPMr(double Mr) { this.HP_MR = Mr; }

        public string getHPMrPer() { return this.HP_MR_PER.ToString(); }
        public void setHPMrPer(double MrPer) { this.HP_MR_PER = MrPer; }

        public string getRemark() { return this.OUTLET_REMARK; }
        public void setRemark(string remark) { this.OUTLET_REMARK = remark; }
        public string getRemarkDate() { return this.OUTLET_REMARK_DATE; }
        public void setRemarkDate(string remarkDate) { this.OUTLET_REMARK_DATE = remarkDate; }

        public string getP01Code()
        {
            if (this.P_01_CODE == null)
            {
                return "";
            }

            return this.P_01_CODE;
        }

        public void setP01Code(string P01Code)
        {
            this.P_01_CODE = P01Code;
        }

        public string getP02Code()
        {
            if (this.P_02_CODE == null)
            {
                return "";
            }

            return this.P_02_CODE;
        }

        public void setP02Code(string P02Code)
        {
            this.P_02_CODE = P02Code;
        }

        public string getP03Code()
        {
            if (this.P_03_CODE == null)
            {
                return "";
            }

            return this.P_03_CODE;
        }

        public void setP03Code(string P03Code)
        {
            this.P_03_CODE = P03Code;
        }

        public string getP04Code()
        {
            if (this.P_04_CODE == null)
            {
                return "";
            }

            return this.P_04_CODE;
        }

        public void setP04Code(string P04Code)
        {
            this.P_04_CODE = P04Code;
        }

        public string getP05Code()
        {
            if (this.P_05_CODE == null)
            {
                return "";
            }

            return this.P_05_CODE;
        }

        public void setP05Code(string P05Code)
        {
            this.P_05_CODE = P05Code;
        }

        public string getP01Color()
        {
            if (this.P_01_COLOR == null)
            {
                return "";
            }

            return this.P_01_COLOR;
        }

        public void setP01Color(string P01Color)
        {
            this.P_01_COLOR = P01Color;
        }

        public string getP02Color()
        {
            if (this.P_02_COLOR == null)
            {
                return "";
            }

            return this.P_02_COLOR;
        }

        public void setP02Color(string P02Color)
        {
            this.P_02_COLOR = P02Color;
        }

        public string getP03Color()
        {
            if (this.P_03_COLOR == null)
            {
                return "";
            }

            return this.P_03_COLOR;
        }

        public void setP03Color(string P03Color)
        {
            this.P_03_COLOR = P03Color;
        }

        public string getP04Color()
        {
            if (this.P_04_COLOR == null)
            {
                return "";
            }

            return this.P_04_COLOR;
        }

        public void setP04Color(string P04Color)
        {
            this.P_04_COLOR = P04Color;
        }

        public string getP05Color()
        {
            if (this.P_05_COLOR == null)
            {
                return "";
            }

            return this.P_05_COLOR;
        }

        public void setP05Color(string P05Color)
        {
            this.P_05_COLOR = P05Color;
        }

    }
}
