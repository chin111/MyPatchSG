using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Android.Views.InputMethods;
using Android.Widget;

using Android.Support.V4.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

using AndroidHUD;

using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Adapters;
using MyPatchSG.Droid.Globals;

using DSoft.Messaging;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "SortActivity")]
    class SortActivity : BaseActivity
    {
        public Button BtnSortByOutletID { get; set; }
        public Button BtnSortByOutletName { get; set; }
        public Button BtnSortByBAT { get; set; }
        public Button BtnSortByPremium { get; set; }
        public Button BtnSortByMR { get; set; }
        public Button BtnCancel { get; set; }

        public string SortBy = "";
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_sort_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar_sort;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_arrow_back_white_24dp);

            this.Title = "Sort";

            BtnSortByOutletID = FindViewById<Button>(Resource.Id.sort_btn_outletid);
            BtnSortByOutletName = FindViewById<Button>(Resource.Id.sort_btn_outletname);
            BtnSortByBAT = FindViewById<Button>(Resource.Id.sort_btn_bat);
            BtnSortByPremium = FindViewById<Button>(Resource.Id.sort_btn_premium);
            BtnSortByMR = FindViewById<Button>(Resource.Id.sort_btn_mr);
            BtnCancel = FindViewById<Button>(Resource.Id.sort_btn_cancel);

            BtnSortByOutletID.Click += OnSortByOutletIDClick;
            BtnSortByOutletName.Click += OnSortByOutletNameClick;
            BtnSortByBAT.Click += OnSortByBATClick;
            BtnSortByPremium.Click += OnSortByPremiumClick;
            BtnSortByMR.Click += OnSortByMRClick;
            BtnCancel.Click += OnCancelClick;

            SortBy = "";
        }
        private void OnSortByOutletIDClick(object sender, EventArgs e)
        {
            SortBy = "OutletID";
            GlobalsAndroid.SortBy = SortBy;
            var newEvent = new CoreMessageBusEvent("OutletListSort");
            MessageBus.PostEvent(newEvent);

            Finish();
        }
        private void OnSortByOutletNameClick(object sender, EventArgs e)
        {
            SortBy = "OutletName";
            GlobalsAndroid.SortBy = SortBy;
            var newEvent = new CoreMessageBusEvent("OutletListSort");
            MessageBus.PostEvent(newEvent);

            Finish();
        }
        private void OnSortByBATClick(object sender, EventArgs e)
        {
            SortBy = "BAT";
            GlobalsAndroid.SortBy = SortBy;
            var newEvent = new CoreMessageBusEvent("OutletListSort");
            MessageBus.PostEvent(newEvent);

            Finish();
        }
        private void OnSortByPremiumClick(object sender, EventArgs e)
        {
            SortBy = "Premium";
            GlobalsAndroid.SortBy = SortBy;
            var newEvent = new CoreMessageBusEvent("OutletListSort");
            MessageBus.PostEvent(newEvent);

            Finish();
        }
        private void OnSortByMRClick(object sender, EventArgs e)
        {
            SortBy = "MR";
            GlobalsAndroid.SortBy = SortBy;
            var newEvent = new CoreMessageBusEvent("OutletListSort");
            MessageBus.PostEvent(newEvent);

            Finish();
        }
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            Finish();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}