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
    class OutletTaskItemViewHolder : RecyclerView.ViewHolder
    {
        public TextView OutletTaskCode { get; private set; }
        public TextView OutletTaskName { get; private set; }
        public TextView OutletTaskDesc { get; private set; }
        public OutletTaskItemViewHolder(View itemView) : base(itemView)
        {
            OutletTaskCode = itemView.FindViewById<TextView>(Resource.Id.outlettask_item_code);
            OutletTaskName = itemView.FindViewById<TextView>(Resource.Id.outlettask_item_name);
            OutletTaskDesc = itemView.FindViewById<TextView>(Resource.Id.outlettask_item_desc);
        }
        public void BindItem(OutletTask listItem)
        {
            OutletTaskCode.Text = listItem.getProjectCode();
            OutletTaskName.Text = listItem.getProjectName();
            OutletTaskDesc.Text = listItem.getTaskDesc();
        }

    }
}