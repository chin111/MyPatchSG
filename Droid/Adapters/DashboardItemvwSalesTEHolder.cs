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
    class DashboardItemvwSalesTEHolder : RecyclerView.ViewHolder
    {
        public TableLayout TETable { get; set; }
        private ViewGroup parentView;
        public DashboardItemvwSalesTEHolder(View itemView, ViewGroup parent) : base(itemView)
        {
            this.parentView = parent;

            TETable = itemView.FindViewById<TableLayout>(Resource.Id.item_vwsaleste_table);

            TETable.SetOnClickListener(null);
            TETable.Clickable = false;
        }

        public void BindItem(List<vwSalesTE> mVwSalesTe, List<LKWk> mLKWk)
        {
            View view = LayoutInflater.From(this.parentView.Context).Inflate(Resource.Layout.item_vwsaleste_header, this.parentView, false);
            TableRow rowHeader = (TableRow)view;

            TextView headerType = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_type);
            TextView headerSKU = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_sku);
            TextView headerBase = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_base);
            TextView headerAvg = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_avg);
            TextView headerPer = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_per);
            TextView headerWK1 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk1);
            TextView headerWK2 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk2);
            TextView headerWK3 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk3);
            TextView headerWK4 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk4);
            TextView headerWK5 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk5);
            TextView headerWK6 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk6);
            TextView headerWK7 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk7);
            TextView headerWK8 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk8);
            TextView headerWK9 = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_wk9);
            TextView headerTotal = (TextView)rowHeader.FindViewById<TextView>(Resource.Id.header_total);

            headerType.Text = "";
            headerSKU.Text = "SKU";
            headerBase.Text = "Base";
            headerAvg.Text = "Avg";
            headerPer.Text = "%";
            headerTotal.Text = "Total";

            LKWk wk = FindWK("VOL_01", mLKWk);
            headerWK1.Text = GetWeekName(wk, "1");
            wk = FindWK("VOL_02", mLKWk);
            headerWK2.Text = GetWeekName(wk, "2");
            wk = FindWK("VOL_03", mLKWk);
            headerWK3.Text = GetWeekName(wk, "3");
            wk = FindWK("VOL_04", mLKWk);
            headerWK4.Text = GetWeekName(wk, "4");
            wk = FindWK("VOL_05", mLKWk);
            headerWK5.Text = GetWeekName(wk, "5");
            wk = FindWK("VOL_06", mLKWk);
            headerWK6.Text = GetWeekName(wk, "6");
            wk = FindWK("VOL_07", mLKWk);
            headerWK7.Text = GetWeekName(wk, "7");
            wk = FindWK("VOL_08", mLKWk);
            headerWK8.Text = GetWeekName(wk, "8");
            wk = FindWK("VOL_09", mLKWk);
            headerWK9.Text = GetWeekName(wk, "9");

            TETable.AddView(rowHeader);

            AddEmptyRow();

            foreach(var item in mVwSalesTe)
            {
                AddRow(item);
            }
        }

        private LKWk FindWK(string ID, List<LKWk> mLKWk)
        {
            LKWk result = mLKWk.Find(delegate (LKWk item)
            {
                return item.getFieldID() == ID;
            });

            return result;
        }

        private string GetWeekName(LKWk wk, string placeHolder)
        {
            if (wk != null)
            {
                string wkFullTitle = wk.getWK();
                if (wkFullTitle != "")
                {
                    string wkNumString = wkFullTitle.Substring(wkFullTitle.Length - 2, 2);
                    return Int32.Parse(wkNumString).ToString();
                }
                else
                {
                    return placeHolder;
                }
            }

            return placeHolder;
        }
        private void AddEmptyRow()
        {
            View view = LayoutInflater.From(this.parentView.Context).Inflate(Resource.Layout.item_vwsaleste_item, this.parentView, false);
            TableRow rowItem = (TableRow)view;

            TextView itemType = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_type);
            TextView itemSKU = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_sku);
            TextView itemBase = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_base);
            TextView itemAvg = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_avg);
            TextView itemPer = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_per);
            TextView itemWK1 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk1);
            TextView itemWK2 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk2);
            TextView itemWK3 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk3);
            TextView itemWK4 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk4);
            TextView itemWK5 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk5);
            TextView itemWK6 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk6);
            TextView itemWK7 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk7);
            TextView itemWK8 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk8);
            TextView itemWK9 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk9);
            TextView itemTotal = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_total);

            itemType.Text = "";
            itemSKU.Text = "";
            itemBase.Text = "";
            itemAvg.Text = "";
            itemPer.Text = "";
            itemWK1.Text = "";
            itemWK2.Text = "";
            itemWK3.Text = "";
            itemWK4.Text = "";
            itemWK5.Text = "";
            itemWK6.Text = "";
            itemWK7.Text = "";
            itemWK8.Text = "";
            itemWK9.Text = "";
            itemTotal.Text = "";

            TETable.AddView(rowItem);
        }
        private void AddRow(vwSalesTE item)
        {
            View view = LayoutInflater.From(this.parentView.Context).Inflate(Resource.Layout.item_vwsaleste_item, this.parentView, false);
            TableRow rowItem = (TableRow)view;

            TextView itemType = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_type);
            TextView itemSKU = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_sku);
            TextView itemBase = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_base);
            TextView itemAvg = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_avg);
            TextView itemPer = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_per);
            TextView itemWK1 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk1);
            TextView itemWK2 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk2);
            TextView itemWK3 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk3);
            TextView itemWK4 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk4);
            TextView itemWK5 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk5);
            TextView itemWK6 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk6);
            TextView itemWK7 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk7);
            TextView itemWK8 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk8);
            TextView itemWK9 = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_wk9);
            TextView itemTotal = (TextView)rowItem.FindViewById<TextView>(Resource.Id.item_total);

            itemType.Text = item.getSalesType();
            itemSKU.Text = item.getSKUCode();
            itemBase.Text = item.getVolBaseString();
            itemAvg.Text = item.getVolAvgString();
            itemPer.Text = item.getVolPercentString();
            itemWK1.Text = item.getVol01String();
            itemWK2.Text = item.getVol02String();
            itemWK3.Text = item.getVol03String();
            itemWK4.Text = item.getVol04String();
            itemWK5.Text = item.getVol05String();
            itemWK6.Text = item.getVol06String();
            itemWK7.Text = item.getVol07String();
            itemWK8.Text = item.getVol08String();
            itemWK9.Text = item.getVol09String();
            itemTotal.Text = item.getVolTotalString();

            TETable.AddView(rowItem);
        }
    }
}