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
    class OutletItemRemarkHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public IOutletItemDetailRemarkClickListener mListener { get; private set; }
        public TextView Remark { get; set; }
        public TextView UpdateDate { get; set; }
        public TextView Edit { get; set; }
        public TextView TextBox { get; set; }
        public OutletItemRemarkHolder(View itemView, IOutletItemDetailRemarkClickListener listener) : base(itemView)
        {
            this.mListener = listener;

            Remark = itemView.FindViewById<TextView>(Resource.Id.item_remark_remarks);
            UpdateDate = itemView.FindViewById<TextView>(Resource.Id.item_remark_remark_date);
            Edit = itemView.FindViewById<TextView>(Resource.Id.item_remark_edit);
            TextBox = itemView.FindViewById<TextView>(Resource.Id.item_remark_text_view);

            Remark.SetOnClickListener(null);
            Remark.Clickable = false;
            UpdateDate.SetOnClickListener(null);
            UpdateDate.Clickable = false;

            Edit.SetOnClickListener(this);
            TextBox.SetOnClickListener(this);
        }

        public void BindItem(RCSOUTLET rcs)
        {
            Remark.Text = "Outlet Remarks (Update By TME)";
            UpdateDate.Text = "";
            Edit.Text = "EDIT";
            TextBox.Text = "";

            if (rcs != null)
            {
                TextBox.Text = rcs.getFIELDVALUE();
                UpdateDate.Text = rcs.getLASTUPDATE();               
            }
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
                case Resource.Id.item_remark_edit:
                    mListener.OnRemarkEditClicked(TextBox.Text);
                    break;
                case Resource.Id.item_remark_text_view:
                    mListener.OnRemarkClicked(TextBox.Text);
                    break;
            }
        }
    }
    public interface IOutletItemDetailRemarkClickListener
    {
        void OnRemarkEditClicked(string remark);
        void OnRemarkClicked(string remark);
    }
}