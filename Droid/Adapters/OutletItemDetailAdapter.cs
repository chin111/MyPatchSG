using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
using MyPatchSG.Droid.ViewModels;

namespace MyPatchSG.Droid.Adapters
{
    class OutletItemDetailAdapter : RecyclerView.Adapter, IOutletItemDetailRemarkClickListener
    {
        public event EventHandler<OutletItemDetailRemarkClickedEventArgs> OutletItemRemarkClick;

        private vwOutletListViewModel OutletItem;
        private List<OutletTask> OutletItemTask;
        private RCSOUTLET OutletItemRCS;
        private List<vwSalesOutletChart> OutletItemChartList;
        private List<vwSalesOutlet> OutletItemSalesOutlet;
        private List<LKWk> mLKWk;

        private ViewGroup adapterParent;

        private int VIEW_TYPE_VWOUTLET_LIST = 10;
        private int VIEW_TYPE_OUTLET_TASK = 11;
        private int VIEW_TYPE_RCS_OUTLET = 12;
        private int VIEW_TYPE_VWSALES_OUTLET_CHART = 13;
        private int VIEW_TYPE_VWSALES_OUTLET = 14;

        public OutletItemDetailAdapter(vwOutletListViewModel item, List<OutletTask> task, RCSOUTLET rcs, List<vwSalesOutletChart> chart, List<vwSalesOutlet> salesOutlet, List<LKWk> mLKWk)
        {
            this.OutletItem = item;
            this.OutletItemTask = task;
            this.OutletItemRCS = rcs;
            this.OutletItemChartList = chart;
            this.OutletItemSalesOutlet = salesOutlet;
            this.mLKWk = mLKWk;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            this.adapterParent = parent;

            if (viewType == VIEW_TYPE_VWOUTLET_LIST)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.outletitem_detail_fis_holder, parent, false);
                OutletItemFISHolder vh = new OutletItemFISHolder(view);
                return vh;
            }
            else if (viewType == VIEW_TYPE_OUTLET_TASK)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.outletitem_detail_task_holder, parent, false);
                OutletItemTaskHolder vh = new OutletItemTaskHolder(view);
                return vh;
            }
            else if (viewType == VIEW_TYPE_RCS_OUTLET)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.outletitem_detail_remark_holder, parent, false);
                OutletItemRemarkHolder vh = new OutletItemRemarkHolder(view, this);
                return vh;
            }
            else if (viewType == VIEW_TYPE_VWSALES_OUTLET_CHART)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.outletitem_detail_sales_holder, parent, false);
                OutletItemSalesHolder vh = new OutletItemSalesHolder(view);
                return vh;
            }
            else if (viewType == VIEW_TYPE_VWSALES_OUTLET)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_vwsaleste_table, parent, false);
                OutletItemSalesTableHolder vh = new OutletItemSalesTableHolder(view, this.adapterParent);
                return vh;
            }

            return null;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder.GetType() == typeof(OutletItemFISHolder))
            {
                OutletItemFISHolder vh = (OutletItemFISHolder)holder;
                vh.BindItem(OutletItem);
            }
            else if (holder.GetType() == typeof(OutletItemTaskHolder))
            {
                OutletItemTaskHolder vh = (OutletItemTaskHolder)holder;
                vh.BindItem(OutletItemTask);
            }
            else if (holder.GetType() == typeof(OutletItemRemarkHolder))
            {
                OutletItemRemarkHolder vh = (OutletItemRemarkHolder)holder;
                vh.BindItem(OutletItemRCS);
            }
            else if (holder.GetType() == typeof(OutletItemSalesHolder))
            {
                OutletItemSalesHolder vh = (OutletItemSalesHolder)holder;
                vh.BindItem(OutletItemChartList);
            }
            else if (holder.GetType() == typeof(OutletItemSalesTableHolder))
            {
                OutletItemSalesTableHolder vh = (OutletItemSalesTableHolder)holder;
                vh.BindItem(OutletItemSalesOutlet, mLKWk);
            }
        }

        public override int ItemCount
        {
            get { return 5; }
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return VIEW_TYPE_VWOUTLET_LIST;
            }
            else if (position == 1)
            {
                return VIEW_TYPE_OUTLET_TASK;
            }
            else if (position == 2)
            {
                return VIEW_TYPE_RCS_OUTLET;
            }
            else if (position == 3)
            {
                return VIEW_TYPE_VWSALES_OUTLET_CHART;
            }
            else if (position == 4)
            {
                return VIEW_TYPE_VWSALES_OUTLET;
            }

            return base.GetItemViewType(position);
        }

        void OnClick(int position)
        {

        }
        public void OnRemarkEditClicked(string remark)
        {
            OutletItemRemarkClick?.Invoke(this, new OutletItemDetailRemarkClickedEventArgs { remark = remark });
        }
        public void OnRemarkClicked(string remark)
        {
            OutletItemRemarkClick?.Invoke(this, new OutletItemDetailRemarkClickedEventArgs { remark = remark });
        }

        public void UpdateRCSOutletRemark(RCSOUTLET rcsOutlet)
        {
            this.OutletItemRCS = rcsOutlet;
        }

    }
    public class OutletItemDetailRemarkClickedEventArgs : EventArgs
    {
        public string remark { get; set; }
    }
}