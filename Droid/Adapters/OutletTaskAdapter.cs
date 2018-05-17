using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;
using Android.Content;

using MyPatchSG.DL.Models;

namespace MyPatchSG.Droid.Adapters
{
    class OutletTaskAdapter : RecyclerView.Adapter
    {
        private readonly List<OutletTask> mOutletTask;

        private ViewGroup adapterParent;
        public OutletTaskAdapter(List<OutletTask> taskList)
        {
            this.mOutletTask = taskList;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            this.adapterParent = parent;
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.outlet_task_item_holder, parent, false);

            OutletTaskItemViewHolder vh = new OutletTaskItemViewHolder(view);

            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OutletTaskItemViewHolder vh = holder as OutletTaskItemViewHolder;
            OutletTask item = mOutletTask.ElementAt(position);

            vh.BindItem(item);
        }
        public override int ItemCount
        {
            get { return mOutletTask.Count(); }
        }

    }
}