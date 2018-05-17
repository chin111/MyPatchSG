using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Runtime;
using Android.OS;
using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;

using MyPatchSG.DL;
using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Activities;
using MyPatchSG.Droid.Utils;

namespace MyPatchSG.Droid.Fragments
{
	public class AboutFragment : Fragment
	{
		IMenu menu;
        public TextView VersionTextView { get; set; }
        public LinearLayout UpdatePanel { get; set; }
        public Button BtnUpdate { get; set; }

		public AboutFragment()
		{
			RetainInstance = true;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			HasOptionsMenu = true;
			var view = inflater.Inflate(Resource.Layout.fragment_about, null);

            VersionTextView = (TextView)view.FindViewById<TextView>(Resource.Id.textview_version);
            VersionTextView.Text = "v" + Core.Globals.AndroidVersionName;

            UpdatePanel = (LinearLayout)view.FindViewById<LinearLayout>(Resource.Id.update_panel);
            UpdatePanel.Visibility = ViewStates.Gone;
            if (Core.Globals.IsUpdateNeeded && (Core.Globals.AppInformation.AppStoreURL != null || Core.Globals.AppInformation.AppStoreURL == ""))
            {
                UpdatePanel.Visibility = ViewStates.Visible;
                BtnUpdate = (Button)view.FindViewById<Button>(Resource.Id.btn_update);
                BtnUpdate.Click += BtnUpdate_Click;
            }

            return view;
		}

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse(Core.Globals.AppInformation.AppStoreURL);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
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

        public override void OnSaveInstanceState(Bundle outState)
        {
            //base.OnSaveInstanceState(outState);
        }
    }
}

