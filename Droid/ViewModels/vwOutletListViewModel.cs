using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MyPatchSG.DL.Models;

namespace MyPatchSG.Droid.ViewModels
{
    public class vwOutletListViewModel
    {
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string FIS_TARGET { get; set; }
        public string FIS_SALES { get; set; }
        public string FIS_BAL { get; set; }
        public string FIS_PER { get; set; }
        public string FIS_MR { get; set; }
        public string FIS_MR_PER { get; set; }
        public string HP_TARGET { get; set; }
        public string HP_SALES { get; set; }
        public string HP_BAL { get; set; }
        public string HP_PER { get; set; }
        public string HP_MR { get; set; }
        public string HP_MR_PER { get; set; }
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

        public bool Selected { get; set; }

        public vwOutletListViewModel()
        {

        }

        public vwOutletListViewModel(vwOutletList item, bool Selected = false)
        {
            this.CUSTOMER_ID = item.getCustomerID();
            this.CUSTOMER_NAME = item.getCustomerName();
            this.FIS_TARGET = item.getFISTarget();
            this.FIS_SALES = item.getFISSales();
            this.FIS_PER = item.getFISPer();
            this.FIS_BAL = item.getFISBal();
            this.FIS_MR = item.getFISMr();
            this.FIS_MR_PER = item.getFISMrPer();
            this.HP_TARGET = item.getHPTarget();
            this.HP_SALES = item.getHPSales();
            this.HP_PER = item.getHPPer();
            this.HP_BAL = item.getHPBal();
            this.HP_MR = item.getHPMr();
            this.HP_MR_PER = item.getHPMrPer();
            this.OUTLET_REMARK = item.getRemark();
            this.OUTLET_REMARK_DATE = item.getRemarkDate();
            this.P_01_CODE = item.getP01Code();
            this.P_02_CODE = item.getP02Code();
            this.P_03_CODE = item.getP03Code();
            this.P_04_CODE = item.getP04Code();
            this.P_05_CODE = item.getP05Code();
            this.P_01_COLOR = item.getP01Color();
            this.P_02_COLOR = item.getP02Color();
            this.P_03_COLOR = item.getP03Color();
            this.P_04_COLOR = item.getP04Color();
            this.P_05_COLOR = item.getP05Color();

            this.Selected = Selected;
        }

        public string getCustomerID() { return this.CUSTOMER_ID; }
        public string getCustomerName() { return this.CUSTOMER_NAME; }
        public string getFISTarget() { return this.FIS_TARGET.ToString(); }
        public string getFISSales() { return this.FIS_SALES.ToString(); }
        public string getFISPer() { return this.FIS_PER.ToString(); }
        public string getFISBal() { return this.FIS_BAL.ToString(); }
        public string getFISMr() { return this.FIS_MR.ToString(); }
        public string getFISMrPer() { return this.FIS_MR_PER.ToString(); }
        public string getHPTarget() { return this.HP_TARGET.ToString(); }
        public string getHPSales() { return this.HP_SALES.ToString(); }
        public string getHPPer() { return this.HP_PER.ToString(); }
        public string getHPBal() { return this.HP_BAL.ToString(); }
        public string getHPMr() { return this.HP_MR.ToString(); }
        public string getHPMrPer() { return this.HP_MR_PER.ToString(); }
        public string getRemark() { return this.OUTLET_REMARK; }
        public string getP01Code() { return this.P_01_CODE; }
        public string getP02Code() { return this.P_02_CODE; }
        public string getP03Code() { return this.P_03_CODE; }
        public string getP04Code() { return this.P_04_CODE; }
        public string getP05Code() { return this.P_05_CODE; }
        public string getP01Color() { return this.P_01_COLOR; }
        public string getP02Color() { return this.P_02_COLOR; }
        public string getP03Color() { return this.P_03_COLOR; }
        public string getP04Color() { return this.P_04_COLOR; }
        public string getP05Color() { return this.P_05_COLOR; }
    }
}