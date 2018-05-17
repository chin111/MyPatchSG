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

using MyPatchSG.Droid.ViewModels;

using System.Globalization;

namespace MyPatchSG.Droid.Adapters
{
    class OutletItemFISHolder : RecyclerView.ViewHolder
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
        public OutletItemFISHolder(View itemView) : base(itemView)
        {
            FISLabel = ItemView.FindViewById<TextView>(Resource.Id.item_fis_fis);
            TotalLabel = ItemView.FindViewById<TextView>(Resource.Id.item_fis_total);
            FISSalesTarget = itemView.FindViewById<TextView>(Resource.Id.item_fis_fis_sales_vs_target);
            FISPer = itemView.FindViewById<TextView>(Resource.Id.item_fis_fis_per);
            FISBal = itemView.FindViewById<TextView>(Resource.Id.item_fis_fis_bal);
            FISMr = itemView.FindViewById<TextView>(Resource.Id.item_fis_fis_mr);
            FISMrPer = itemView.FindViewById<TextView>(Resource.Id.item_fis_fis_mr_per);
            HPLabel = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp);
            HPSalesTarget = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp_sales_vs_target);
            HPPer = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp_per);
            HPBal = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp_bal);
            HPMr = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp_mr);
            HPMrPer = itemView.FindViewById<TextView>(Resource.Id.item_fis_hp_mr_per);
        }

        public void BindItem(vwOutletListViewModel item)
        {
            FISLabel.Text = "FIS";
            TotalLabel.Text = "Total";
            HPLabel.Text = "( HP )";

            FISSalesTarget.Text = item.getFISSales() + "/" + item.getFISTarget();
            FISPer.Text = item.getFISPer() + "%";
            FISBal.Text = "B: " + item.getFISBal();
            FISMr.Text = "MR: " + item.getFISMr();
            FISMrPer.Text = item.getFISMr() + "%";
            HPSalesTarget.Text = item.getHPSales() + "/" + item.getHPTarget();
            HPPer.Text = item.getHPPer() + "%";
            HPBal.Text = "B: " + item.getHPBal();
            HPMr.Text = "MR: " + item.getHPMr();
            HPMrPer.Text = item.getHPMr() + "%";
        }
    }
}