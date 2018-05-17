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
using MyPatchSG.Droid.Activities;
using MyPatchSG.Droid.Fragments;

namespace MyPatchSG.Droid.Adapters
{
    public class DashboardListAdapter : RecyclerView.Adapter, IDashboardItemRCSTEClickListener
    {
        public event EventHandler<DashboardItemTERemarkClickedEventArgs> DashboardItemRemarkClick;

        private vwTE mVwTE;
        private RCSTE mRCSTE;
        private List<vwSalesTEChart> mVwSalesTEChart;
        private List<vwSalesTE> mVwSalesTE;
        private List<LKWk> mLKWk;

        private int VIEW_TYPE_VWTE = 10;
        private int VIEW_TYPE_RCSTE = 11;
        private int VIEW_TYPE_VWSALESTECHART = 12;
        private int VIEW_TYPE_VWSALESTE = 13;

        private DashboardFragment dashboardFragment;

        public DashboardListAdapter(vwTE mVwTE, RCSTE mRCSTE, List<vwSalesTEChart> mTEChart, List<vwSalesTE> mTE, List<LKWk> mLKWk, DashboardFragment dashboardFragment)
        {
            this.mVwTE = mVwTE;
            this.mRCSTE = mRCSTE;
            this.mVwSalesTEChart = mTEChart;
            this.mVwSalesTE = mTE;
            this.mLKWk = mLKWk;

            this.dashboardFragment = dashboardFragment;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == VIEW_TYPE_VWTE)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.fragment_dashboard_item_vwte_holder, parent, false);
                DashboardItemvwTEHolder vh = new DashboardItemvwTEHolder(view);
                return vh;
            }
            else if (viewType == VIEW_TYPE_RCSTE)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.fragment_dashboard_item_rcste_holder, parent, false);
                DashboardItemRCSTEHolder vh = new DashboardItemRCSTEHolder(view, this, this.dashboardFragment);
                return vh;
            }
            else if (viewType == VIEW_TYPE_VWSALESTECHART)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.fragment_dashboard_item_vwsalestechart_holder, parent, false);
                DashboardItemvwSalesTEChartHolder vh = new DashboardItemvwSalesTEChartHolder(view);
                return vh;
            }
            else if (viewType == VIEW_TYPE_VWSALESTE)
            {
                View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_vwsaleste_table, parent, false);
                DashboardItemvwSalesTEHolder vh = new DashboardItemvwSalesTEHolder(view, parent);
                return vh;
            }

            return null;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder.GetType() == typeof(DashboardItemvwTEHolder))
            {
                DashboardItemvwTEHolder vh = (DashboardItemvwTEHolder)holder;
                vh.BindItem(mVwTE);
            }
            else if (holder.GetType() == typeof(DashboardItemRCSTEHolder))
            {
                DashboardItemRCSTEHolder vh = (DashboardItemRCSTEHolder)holder;
                vh.BindItem(mRCSTE);
            }
            else if (holder.GetType() == typeof(DashboardItemvwSalesTEChartHolder))
            {
                DashboardItemvwSalesTEChartHolder vh = (DashboardItemvwSalesTEChartHolder)holder;
                vh.BindItem(mVwSalesTEChart);
            }
            else if (holder.GetType() == typeof(DashboardItemvwSalesTEHolder))
            {
                DashboardItemvwSalesTEHolder vh = (DashboardItemvwSalesTEHolder)holder;
                vh.BindItem(mVwSalesTE, mLKWk);
            }

        }

        public override int ItemCount
        {
            get { return 4; }
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return VIEW_TYPE_VWTE;
            }
            else if (position == 1)
            {
                return VIEW_TYPE_RCSTE;
            }
            else if (position == 2)
            {
                return VIEW_TYPE_VWSALESTECHART;
            }
            else if (position == 3)
            {
                return VIEW_TYPE_VWSALESTE;
            }

            return base.GetItemViewType(position);
        }

        void OnClick(int position)
        {

        }

        public void OnTERemarkEditClicked(string remark)
        {
            DashboardItemRemarkClick?.Invoke(this, new DashboardItemTERemarkClickedEventArgs { remark = remark });
        }
        public void OnTERemarkClicked(string remark)
        {
            DashboardItemRemarkClick?.Invoke(this, new DashboardItemTERemarkClickedEventArgs { remark = remark });
        }

        public void UpdateRCSTERemark(RCSTE rcsTE)
        {
            this.mRCSTE = rcsTE;
        }
    }
    public class DashboardItemTERemarkClickedEventArgs : EventArgs
    {
        public string remark { get; set; }
    }
}