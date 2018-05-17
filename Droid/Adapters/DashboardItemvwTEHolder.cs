using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;

using MyPatchSG.DL.Models;
using System.Globalization;

namespace MyPatchSG.Droid.Adapters
{
    class DashboardItemvwTEHolder : RecyclerView.ViewHolder
    {
        public TextView FISLabel { get; set; }
        public TextView TotalLabel { get; set; }
        public TextView FISSalesTarget { get; private set; }
        public TextView FISPer { get; private set; }
        public TextView FISBal { get; private set; }
        public TextView FISMr { get; private set; }
        public TextView FISMrPer { get; private set; }
        public TextView HPLabel { get; private set; }
        public TextView HPSalesTarget { get; private set; }
        public TextView HPPer { get; private set; }
        public TextView HPBal { get; private set; }
        public TextView HPMr { get; private set; }
        public TextView HPMrPer { get; private set; }
        public DashboardItemvwTEHolder(View itemView) : base(itemView)
        {
            FISLabel = ItemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis);
            TotalLabel = ItemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_total);
            FISSalesTarget = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis_sales_vs_target);
            FISPer = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis_per);
            FISBal = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis_bal);
            FISMr = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis_mr);
            FISMrPer = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_fis_mr_per);
            HPLabel = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp);
            HPSalesTarget = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp_sales_vs_target);
            HPPer = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp_per);
            HPBal = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp_bal);
            HPMr = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp_mr);
            HPMrPer = itemView.FindViewById<TextView>(Resource.Id.item_vwte_vwte_hp_mr_per);
        }

        public void BindItem(vwTE mVwTE)
        {
            FISLabel.Text = "FIS";
            TotalLabel.Text = "Total";
            HPLabel.Text = "( HP )";

            FISSalesTarget.Text = mVwTE.getFISSales() + "/" + mVwTE.getFISTarget();
            FISPer.Text = mVwTE.getFISPer() + "%";
            FISBal.Text = "B: " + mVwTE.getFISBal();
            FISMr.Text = "MR: " + mVwTE.getFISMr();
            FISMrPer.Text = mVwTE.getFISMr() + "%";
            HPSalesTarget.Text = mVwTE.getHPSales() + "/" + mVwTE.getHPTarget();
            HPPer.Text = mVwTE.getHPPer() + "%";
            HPBal.Text = "B: " + mVwTE.getHPBal();
            HPMr.Text = "MR: " + mVwTE.getHPMr();
            HPMrPer.Text = mVwTE.getHPMr() + "%";
        }
    }
}