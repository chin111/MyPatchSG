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

using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Adapters;
using MyPatchSG.Droid.Globals;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "FilterActivity")]
    class FilterActivity : BaseActivity
    {
        RecyclerView recyclerView;

        MasterDB masterDB = null;
        List<OutletTask> outletTask = new List<OutletTask>();

        TextView OutletTaskHeaderCode;
        TextView OutletTaskHeaderName;
        TextView OutletTaskHeaderDesc;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_filter_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar_filter;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_arrow_back_white_24dp);

            this.Title = "Filter";

            OutletTaskHeaderCode = FindViewById<TextView>(Resource.Id.outlettask_header_code);
            OutletTaskHeaderName = FindViewById<TextView>(Resource.Id.outlettask_header_name);
            OutletTaskHeaderDesc = FindViewById<TextView>(Resource.Id.outlettask_header_desc);

            OutletTaskHeaderCode.Text = "Code";
            OutletTaskHeaderName.Text = "Name";
            OutletTaskHeaderDesc.Text = "Task Description";

            string dbPath = new FileUtil().GetMasterDBPath();
            if (File.Exists(dbPath))
            {
                masterDB = new MasterDB();
                masterDB.dbPath = dbPath;

                if (GlobalsAndroid.FilterProjectCode == "")
                {
                    outletTask = masterDB.GetAllOutletTaskList();
                }
                else
                {
                    outletTask = masterDB.GetOutletTaskListByProjectCode(GlobalsAndroid.FilterProjectCode);
                }
            }

            recyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerview_outlet_task);

            // Plug in the linear layout manager:
            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            // Plug in my adapter:
            var adapter = new OutletTaskAdapter(outletTask);
            recyclerView.SetAdapter(adapter);
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