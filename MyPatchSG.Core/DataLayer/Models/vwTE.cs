using System;
using SQLite;

namespace MyPatchSG.DL.Models
{
    public class vwTE
    {
        public string TE_ID { get; set; }
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
        public string TE_REMARK { get; set; }
        public string TE_REMARK_DATE { get; set; }
        public vwTE()
        {

        }
        public string getTEID() { return this.TE_ID; }
        public void setTEID(string ID) { this.TE_ID = ID; }
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

        public string getRemark() { return this.TE_REMARK; }
        public void setRemark(string remark) { this.TE_REMARK = remark; }
        public string getTERemarkDate() { return this.TE_REMARK_DATE; }
        public void setTERemarkDate(string remarkDate) { this.TE_REMARK_DATE = remarkDate; }
    }
}
