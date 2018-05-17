using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V4.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;

using MyPatchSG.Droid.ViewModels;
using MyPatchSG.Droid.Fragments;

using System.Globalization;

namespace MyPatchSG.Droid.Adapters
{
    public class OutletListItemViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public IOutletListItemClickListener mListener { get; private set; }
        public FrameLayout ListItemFrame { get; private set; }
        public TextView CustomerName { get; private set; }
        public TextView Project01 { get; private set; }
        public TextView Project02 { get; private set; }
        public TextView Project03 { get; private set; }
        public TextView CustomerID { get; private set; }
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
        public TextView RemarkLabel { get; private set; }
        public TextView Remark { get; private set; }

        private OutletListFragment outletListFragment;
        
        public OutletListItemViewHolder(View itemView, IOutletListItemClickListener listener, OutletListFragment fragment) : base (itemView)
        {
            this.mListener = listener;
            this.outletListFragment = fragment;

            // Locate and cache view references:
            CustomerName = itemView.FindViewById<TextView>(Resource.Id.outletitem_name_text);
            Project01 = itemView.FindViewById<TextView>(Resource.Id.outletitem_project_01);
            Project02 = itemView.FindViewById<TextView>(Resource.Id.outletitem_project_02);
            Project03 = itemView.FindViewById<TextView>(Resource.Id.outletitem_project_03);
            CustomerID = itemView.FindViewById<TextView>(Resource.Id.outletitem_id);
            FISSalesTarget = itemView.FindViewById<TextView>(Resource.Id.outletitem_fis_sales_vs_target);
            FISPer = itemView.FindViewById<TextView>(Resource.Id.outletitem_fis_per);
            FISBal = itemView.FindViewById<TextView>(Resource.Id.outletitem_fis_bal);
            FISMr = itemView.FindViewById<TextView>(Resource.Id.outletitem_fis_mr);
            FISMrPer = itemView.FindViewById<TextView>(Resource.Id.outletitem_fis_mr_per);
            HPLabel = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp);
            HPSalesTarget = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp_sales_vs_target);
            HPPer = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp_per);
            HPBal = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp_bal);
            HPMr = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp_mr);
            HPMrPer = itemView.FindViewById<TextView>(Resource.Id.outletitem_hp_mr_per);
            RemarkLabel = itemView.FindViewById<TextView>(Resource.Id.outletitem_remark_label);
            Remark = itemView.FindViewById<TextView>(Resource.Id.outletitem_remark);

            ListItemFrame = itemView.FindViewById<FrameLayout>(Resource.Id.fragment_outletlist_item);
            ListItemFrame.SetOnClickListener(this);
        }

        public void BindItem(vwOutletListViewModel listItem, ViewGroup parent)
        {
            HPLabel.Text = "( HP )";
            RemarkLabel.Text = "Remark:";

            CustomerName.Text = listItem.getCustomerName();

            if (listItem.getP01Code() != "")
            {
                Project01.Text = listItem.getP01Code();
                Project01.SetBackgroundColor(GetColorFromHexValue(listItem.getP01Color()));
                Project01.SetOnClickListener(this);
            }
            else
            {
                Project01.Text = "";
                Project01.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Project01.SetOnClickListener(null);
            }

            if (listItem.getP02Code() != "")
            {
                Project02.Text = listItem.getP02Code();
                Project02.SetBackgroundColor(GetColorFromHexValue(listItem.getP02Color()));
                Project02.SetOnClickListener(this);
            }
            else
            {
                Project02.Text = "";
                Project02.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Project02.SetOnClickListener(null);
            }

            if (listItem.getP03Code() != "")
            {
                Project03.Text = listItem.getP03Code();
                Project03.SetBackgroundColor(GetColorFromHexValue(listItem.getP03Color()));
                Project03.SetOnClickListener(this);
            }
            else
            {
                Project03.Text = "";
                Project03.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Project03.SetOnClickListener(null);
            }

            CustomerID.Text = listItem.getCustomerID();
            FISSalesTarget.Text = listItem.getFISSales() + "/" + listItem.getFISTarget();
            FISPer.Text = listItem.getFISPer() + "%";
            FISBal.Text = "B: " + listItem.getFISBal();
            FISMr.Text = "MR: " + listItem.getFISMr();
            FISMrPer.Text = listItem.getFISMr() + "%";
            HPSalesTarget.Text = listItem.getHPSales() + "/" + listItem.getHPTarget();
            HPPer.Text = listItem.getHPPer() + "%";
            HPBal.Text = "B: " + listItem.getHPBal();
            HPMr.Text = "MR: " + listItem.getHPMr();
            HPMrPer.Text = listItem.getHPMr() + "%";
            Remark.Text = listItem.getRemark();

            if (listItem.Selected)
            {                
                Android.Graphics.Color bgColor = new Android.Graphics.Color(ContextCompat.GetColor(this.outletListFragment.Context, Resource.Color.color_light_grey_background));
                ListItemFrame.SetBackgroundColor(bgColor);
            } else
            {
                ListItemFrame.SetBackgroundColor(Android.Graphics.Color.White);
            }
        }
        public Android.Graphics.Color GetColorFromHexValue(string hex)
        {
            if (hex == "")
            {
                hex = "#FFFFFFFF";
            }

            string cleanHex = hex.Replace("0x", "").TrimStart('#');

            if (cleanHex.Length == 6)
            {
                //Affix fully opaque alpha hex value of FF (225)
                cleanHex = "FF" + cleanHex;
            }

            int argb;

            if (Int32.TryParse(cleanHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out argb))
            {
                return new Android.Graphics.Color(argb);
            }

            //If method hasn't returned a color yet, then there's a problem
            throw new ArgumentException("Invalid Hex value. Hex must be either an ARGB (8 digits) or RGB (6 digits)");

        }

        public void OnClick(View v)
        {
            int position = this.AdapterPosition;
            if (position == RecyclerView.NoPosition || mListener == null)
            {
                return;
            }

            switch (v.Id)
            {
                case Resource.Id.outletitem_project_01:
                    mListener.OnProject01Clicked(position);
                    break;
                case Resource.Id.outletitem_project_02:
                    mListener.OnProject02Clicked(position);
                    break;
                case Resource.Id.outletitem_project_03:
                    mListener.OnProject03Clicked(position);
                    break;
                case Resource.Id.fragment_outletlist_item:
                    mListener.OnItemClicked(position);
                    break;
            }
        }
    }

    public interface IOutletListItemClickListener
    {
        void OnItemClicked(int position);
        void OnProject01Clicked(int position);
        void OnProject02Clicked(int position);
        void OnProject03Clicked(int position);
    }
}
