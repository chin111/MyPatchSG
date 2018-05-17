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

using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

using AndroidHUD;
using DSoft.Messaging;

using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Adapters;
using MyPatchSG.Droid.Globals;
using MyPatchSG.Droid.ViewModels;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "OutListItemViewActivity")]
    class OutletListItemViewActivity : BaseActivity
    {
        RecyclerView recyclerView;

        private RCSOUTLET mRCSOutlet = null;
        private List<OutletTask> mOutletTask = null;
        private List<vwSalesOutletChart> mVwSalesOutletChart = null;
        private List<vwSalesOutlet> mVwSalesOutlet = null;
        List<LKWk> listLKWk = new List<LKWk>();

        OutletItemDetailAdapter adapter;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_outletlistitem_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar_outletlistitem;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_arrow_back_white_24dp);

            this.Title = "Outlet Item";

            if (GlobalsAndroid.SelectedOutletListItem != null)
            {
                this.Title = GlobalsAndroid.SelectedOutletListItem.getCustomerName();
            }

            var masterDB = GlobalsAndroid.GetMasterDBInstance();
            if (masterDB != null)
            {
                string CustomerID = GlobalsAndroid.SelectedOutletListItem.getCustomerID();
                mRCSOutlet = masterDB.GetRCSOUTLETByID(CustomerID);
                mOutletTask = masterDB.GetOutletTaskByID(CustomerID);
                mVwSalesOutletChart = masterDB.GetViewSalesOutletChartByID(CustomerID);
                mVwSalesOutlet = masterDB.GetViewSalesOutletByID(CustomerID);
                listLKWk = masterDB.GetLKWk();
            }

            recyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerview_outletlistitem);

            // Plug in the linear layout manager:
            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            // Plug in my adapter:
            adapter = new OutletItemDetailAdapter(GlobalsAndroid.SelectedOutletListItem, mOutletTask, mRCSOutlet, mVwSalesOutletChart, mVwSalesOutlet, listLKWk);
            adapter.OutletItemRemarkClick += OnOutletItemRemarkClicked;
            recyclerView.SetAdapter(adapter);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var newEvent = new CoreMessageBusEvent("OutletListItemViewAppeared");
            MessageBus.PostEvent(newEvent);
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
        public override void OnBackPressed()
        {
            Finish();
        }
        private void OnOutletItemRemarkClicked(object sender, OutletItemDetailRemarkClickedEventArgs e)
        {
            ShowRemarkTextBox(e.remark);
        }
        private void ShowRemarkTextBox(string remark)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Outlet Remarks");
            LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
            var customView = inflater.Inflate(Resource.Layout.fragment_dashboard_rcste_remark_box, null);
            builder.SetView(customView);

            EditText editText = customView.FindViewById<EditText>(Resource.Id.fragment_dashboard_rcste_remark_text);
            editText.Text = remark;
            builder.SetPositiveButton("SAVE", (sender, e) =>
            {
                string text = editText.Text;
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                vwOutletListViewModel SelectedItem = GlobalsAndroid.SelectedOutletListItem;
                string CustomerID = SelectedItem.getCustomerID();

                if (CustomerID != "")
                {
                    GlobalsAndroid.SaveRCSOutlet("OUTLET_REMARK", CustomerID, text, today);

                    var masterDB = GlobalsAndroid.GetMasterDBInstance();
                    if (masterDB != null)
                    {
                        masterDB.DeleteRCSOutletByID(CustomerID);
                        masterDB.InsertRCSOutlet("OUTLET_REMARK", CustomerID, text, today);

                        mRCSOutlet = masterDB.GetRCSOUTLETByID(CustomerID);
                        adapter.UpdateRCSOutletRemark(mRCSOutlet);
                        adapter.NotifyItemChanged(2);
                    }
                }
            });
            builder.SetNegativeButton("CANCEL", (sender, e) =>
            {

            });
            builder.Show();
        }
    }
}