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

using MyPatchSG.Droid.ViewModels;
using MyPatchSG.Droid.Fragments;

namespace MyPatchSG.Droid.Adapters
{
    public class OutletListAdapter : RecyclerView.Adapter, IOutletListItemClickListener
    {
        private readonly List<vwOutletListViewModel> mOutletList;
        public event EventHandler<OutletListItemSelectedEventArgs> ItemClick;
        public event EventHandler<OutletListItemProjectCodeClickedEventArgs> ItemProjectCodeClick;

        private ViewGroup adapterParent;
        private OutletListFragment outletListFragment;

        public OutletListAdapter(List<vwOutletListViewModel> outletList, OutletListFragment fragment)
        {
            this.mOutletList = outletList;
            this.outletListFragment = fragment;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            this.adapterParent = parent;
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.fragment_outletlist_item_holder, parent, false);

            OutletListItemViewHolder vh = new OutletListItemViewHolder(view, this, this.outletListFragment);

            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OutletListItemViewHolder vh = holder as OutletListItemViewHolder;
            vwOutletListViewModel item = mOutletList.ElementAt(position);

            vh.BindItem(item, this.adapterParent);
        }

        public override int ItemCount
        {
            get { return mOutletList.Count(); }
        }

        void OnClick(int position)
        {
            if (ItemClick != null)
            {
                vwOutletListViewModel selectedItem = mOutletList.ElementAt(position);
                ItemClick(this, new OutletListItemSelectedEventArgs { Position = position, outletItem = selectedItem });
            }
        }
        public void OnItemClicked(int position)
        {
            if (ItemClick != null)
            {
                vwOutletListViewModel selectedItem = mOutletList.ElementAt(position);
                ItemClick(this, new OutletListItemSelectedEventArgs { Position = position, outletItem = selectedItem });
            }
        }

        public void OnProject01Clicked(int position)
        {
            if (ItemProjectCodeClick != null)
            {
                vwOutletListViewModel selectedItem = mOutletList.ElementAt(position);
                ItemProjectCodeClick(this, new OutletListItemProjectCodeClickedEventArgs { Position = position, ProjectCode = selectedItem.getP01Code() });
            }
        }

        public void OnProject02Clicked(int position)
        {
            if (ItemProjectCodeClick != null)
            {
                vwOutletListViewModel selectedItem = mOutletList.ElementAt(position);
                ItemProjectCodeClick(this, new OutletListItemProjectCodeClickedEventArgs { Position = position, ProjectCode = selectedItem.getP02Code() });
            }
        }

        public void OnProject03Clicked(int position)
        {
            if (ItemProjectCodeClick != null)
            {
                vwOutletListViewModel selectedItem = mOutletList.ElementAt(position);
                ItemProjectCodeClick(this, new OutletListItemProjectCodeClickedEventArgs { Position = position, ProjectCode = selectedItem.getP03Code() });
            }
        }
    }

    public class OutletListItemSelectedEventArgs : EventArgs
    {
        public int Position { get; set; }
        public vwOutletListViewModel outletItem { get; set; }
    }

    public class OutletListItemProjectCodeClickedEventArgs : EventArgs
    {
        public int Position { get; set; }
        public string ProjectCode { get; set; }
    }
}
