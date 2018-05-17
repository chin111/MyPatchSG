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

using DSoft.Messaging;

using MyPatchSG.DL.Models;
using System.Globalization;

using MyPatchSG.Droid.Fragments;

namespace MyPatchSG.Droid.Adapters
{
    class DashboardItemRCSTEHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public IDashboardItemRCSTEClickListener mListener { get; private set; }
        public TextView Remark { get; set; }
        public TextView UpdateDate { get; set; }
        public TextView Edit { get; set; }
        public TextView TextBox { get; set; }
        public DashboardFragment dashboardFragment { get; set; }
        public DashboardItemRCSTEHolder(View itemView, IDashboardItemRCSTEClickListener listener, DashboardFragment dashboardFragment) : base(itemView)
        {
            this.mListener = listener;
            this.dashboardFragment = dashboardFragment;

            Remark = itemView.FindViewById<TextView>(Resource.Id.item_rcste_remarks);
            UpdateDate = itemView.FindViewById<TextView>(Resource.Id.item_rcste_remark_date);
            Edit = itemView.FindViewById<TextView>(Resource.Id.item_rcste_edit);
            TextBox = itemView.FindViewById<TextView>(Resource.Id.item_rcste_text_view);

            Remark.SetOnClickListener(null);
            Remark.Clickable = false;
            UpdateDate.SetOnClickListener(null);
            UpdateDate.Clickable = false;

            Edit.SetOnClickListener(this);
            TextBox.SetOnClickListener(this);
        }

        public void BindItem(RCSTE mRCSTE)
        {
            Remark.Text = "TME Remarks";
            Edit.Text = "EDIT";
            UpdateDate.Text = mRCSTE.getLASTUPDATE();
            TextBox.Text = mRCSTE.getFIELDVALUE();
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
                case Resource.Id.item_rcste_edit:
                    mListener.OnTERemarkEditClicked(TextBox.Text);
                    break;
                case Resource.Id.item_rcste_text_view:
                    mListener.OnTERemarkClicked(TextBox.Text);
                    break;
            }
        }
    }
    public interface IDashboardItemRCSTEClickListener
    {
        void OnTERemarkEditClicked(string remark);
        void OnTERemarkClicked(string remark);
    }
}