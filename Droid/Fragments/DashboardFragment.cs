using System;
using System.IO;
using System.Collections.Generic;

using Android.Graphics;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;

using DSoft.Messaging;

using Core = MyPatchSG.BL.Globals;
using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.Droid.Activities;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Adapters;
using MyPatchSG.Droid.Globals;

namespace MyPatchSG.Droid.Fragments
{
	public class DashboardFragment : Fragment
	{
        IMenu menu;

        List<vwTE> listViewTE = new List<vwTE>();
        List<RCSTE> listRCSTE = new List<RCSTE>();
        List<vwSalesTEChart> listViewSalesTEChart = new List<vwSalesTEChart>();
        List<vwSalesTE> listViewSalesTE = new List<vwSalesTE>();
        List<RCSOUTLET> listRCSOUTLET = new List<RCSOUTLET>();
        List<vwSalesOutletChart> listViewSalesOutletChart = new List<vwSalesOutletChart>();
        List<LKWk> listLKWk = new List<LKWk>();

        RecyclerView recyclerView;
        DashboardListAdapter adapter;

        public DashboardFragment()
		{
			RetainInstance = true;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            // Create your fragment here
            var masterDB = GlobalsAndroid.GetMasterDBInstance();
            if (masterDB != null)
            {
                listViewTE = masterDB.GetViewTE();
                listRCSTE = masterDB.GetRCSTE();
                listViewSalesTEChart = masterDB.GetViewSalesTEChartTotal();
                listViewSalesTE = masterDB.GetViewSalesTE();
                listRCSOUTLET = masterDB.GetRCSOUTLET();
                listViewSalesOutletChart = masterDB.GetViewSalesOutletChart();
                listLKWk = masterDB.GetLKWk();
            }
        }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            HasOptionsMenu = true;
			var view = inflater.Inflate(Resource.Layout.fragment_dashboard, null);
            recyclerView = (RecyclerView)view.FindViewById(Resource.Id.recyclerview_dashboard);

            // Plug in the linear layout manager:
            var layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);

            // Plug in my adapter:
            adapter = new DashboardListAdapter(listViewTE[0], listRCSTE[0], listViewSalesTEChart, listViewSalesTE, listLKWk, this);
            adapter.DashboardItemRemarkClick += OnDashboardItemRemarkClicked;
            recyclerView.SetAdapter(adapter);

            return view;
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
            inflater.Inflate(Resource.Menu.fragment_about_menu, menu);
            this.menu = menu;

			base.OnCreateOptionsMenu(menu, inflater);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
            return false;
		}

        private void OnDashboardItemRemarkClicked(object sender, DashboardItemTERemarkClickedEventArgs e)
        {
            ShowRemarkTextBox(e.remark);
        }

		private void ShowRemarkTextBox(string remark)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this.Context);
            builder.SetTitle("TE Remarks");
            LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            var customView = inflater.Inflate(Resource.Layout.fragment_dashboard_rcste_remark_box, null);
            builder.SetView(customView);

            EditText editText = customView.FindViewById<EditText>(Resource.Id.fragment_dashboard_rcste_remark_text);
            editText.Text = remark;
            builder.SetPositiveButton("SAVE", (sender, e) =>
            {
                string text = editText.Text;
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                string teID = listRCSTE[0].getTEID();
                if (teID != "")
                {
                    GlobalsAndroid.SaveRCSTE("TE_REMARK", teID, text, today);

                    var masterDB = GlobalsAndroid.GetMasterDBInstance();
                    if (masterDB != null)
                    {
                        masterDB.DeleteRCSTE(teID);
                        masterDB.InsertRCSTE("TE_REMARK", teID, text, today);

                        listRCSTE = masterDB.GetRCSTE();
                        adapter.UpdateRCSTERemark(listRCSTE[0]);
                        adapter.NotifyItemChanged(1);
                    }
                }
            });
            builder.SetNegativeButton("CANCEL", (sender, e) =>
            {
                //string masterDBPath = new FileUtil().GetMasterDBPath();
                //if (File.Exists(masterDBPath))
                //{
                //    File.Delete(masterDBPath);
                //}

                //masterDB = null;

                //FileUtil.ClearCache();
                //FileUtil.DeleteCache(this.Activity.ApplicationContext);

                //GC.Collect();
                //GC.WaitForPendingFinalizers();

                //string tempDB = new FileUtil().GetTempDBFileName();
                //File.Move(tempDB, masterDBPath);

            });
            builder.Show();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            //base.OnSaveInstanceState(outState);
        }

    }
}

