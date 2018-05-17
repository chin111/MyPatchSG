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
    class OutletItemTaskHolder : RecyclerView.ViewHolder
    {
        public TextView TaskTitle { get; set; }
        public TextView TextBox { get; set; }
        public OutletItemTaskHolder(View itemView) : base(itemView)
        {
            TaskTitle = itemView.FindViewById<TextView>(Resource.Id.item_task_title);
            TextBox = itemView.FindViewById<TextView>(Resource.Id.item_task_text_view);
        }
        public void BindItem(List<OutletTask> taskList)
        {
            TaskTitle.Text = "Task (From System)";
            TextBox.Text = "";
        }
    }
}