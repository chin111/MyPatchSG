using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V4.Content;
using Android.Views;

using Android.Support.Design.Widget;

using DSoft.Messaging;

using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Fragments;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Globals;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "MainActivity")]
    public class MainActivity : BaseActivity
    {
        DrawerLayout mDrawerLayout;
        NavigationView mNavigationView;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_main_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            ChangeTheme(Resource.Color.palette_blue_primary, Resource.Color.palette_blue_primary_dark);

            this.Title = "Home";

            var newEventHandler = new MessageBusEventHandler()
            {
                EventId = "SyncFinished",
                EventAction = SyncFinishedEventHandler
            };

            MessageBus.Default.Register(newEventHandler);

            mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mNavigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home:
                        ChangeTheme(Resource.Color.palette_blue_primary, Resource.Color.palette_blue_primary_dark);
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_outletlist:
                        ChangeTheme(Resource.Color.palette_green_primary, Resource.Color.palette_green_primary_dark);
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_uom:
                        ChangeTheme(Resource.Color.palette_teal_primary, Resource.Color.palette_teal_primary_dark);
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_sync:
                        ChangeTheme(Resource.Color.palette_blue_light_primary, Resource.Color.palette_blue_light_primary_dark);
                        ListItemClicked(3);
                        break;
                    case Resource.Id.nav_about:
                        ChangeTheme(Resource.Color.palette_orange_primary, Resource.Color.palette_orange_primary_dark);
                        ListItemClicked(4);
                        break;
                    case Resource.Id.nav_logout:
                        ListItemClicked(5);
                        break;
                }

                mDrawerLayout.CloseDrawers();
            };

            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                mNavigationView.Menu.GetItem(0).SetChecked(true);
                ListItemClicked(0);
            }
        }

        private ColorDrawable GetColorDrawableFromColor(int colorId)
        {
            return new Android.Graphics.Drawables.ColorDrawable(new Android.Graphics.Color(ContextCompat.GetColor(this, colorId)));
        }

        private void ChangeStatusBarColor(int colorId)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(new Color(ContextCompat.GetColor(this, colorId)));
            }
        }

        private void ChangeTheme(int primaryColorId, int primaryDarkColorId)
        {
            if (SupportActionBar != null)
            {
                SupportActionBar.SetBackgroundDrawable(GetColorDrawableFromColor(primaryColorId));
                ChangeStatusBarColor(primaryDarkColorId);
            }
        }

        private void ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    this.Title = "Dashboard";
                    fragment = new DashboardFragment();
                    break;
                case 1:
                    this.Title = "Outlet List";
                    fragment = new OutletListFragment();
                    break;
                case 2:
                    this.Title = "Unit of Measurement";
                    fragment = new UOMFragment();
                    break;
                case 3:
                    this.Title = "Synchronization";
                    fragment = new SyncFragment();
                    break;
                case 4:
                    this.Title = "About";
                    fragment = new AboutFragment();
                    break;
                case 5:
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Confirm");
                    builder.SetMessage("Are you sure you want to log out?");
                    builder.SetPositiveButton("NO", (sender, e) =>
                    {

                    });
                    builder.SetNegativeButton("YES", (sender, e) =>
                    {
                        var appPreferences = new AppPreferences(this.ApplicationContext);
                        appPreferences.SaveUsername("");
                        appPreferences.SavePassword("");
                        appPreferences.SaveMasterDBDownloaded(false);
                        appPreferences.SaveAuditDBDownloaded(false);

                        string masterDBPath = new FileUtil().GetMasterDBPath();
                        if (File.Exists(masterDBPath))
                        {
                            File.Delete(masterDBPath);
                        }

                        string auditDBPath = new FileUtil().GetAuditDBPath();
                        if (File.Exists(auditDBPath))
                        {
                            File.Delete(auditDBPath);
                        }

                        string auditDBBlankPath = new FileUtil().GetAuditDBBlankPath();
                        if (File.Exists(auditDBBlankPath))
                        {
                            File.Delete(auditDBBlankPath);
                        }

                        string tempZipFile = new FileUtil().GetTempZipFileName();
                        if (File.Exists(tempZipFile))
                        {
                            File.Delete(tempZipFile);
                        }

                        string tempDirectoryPath = new FileUtil().GetTempDirectoryPath();
                        string auditZipPath = System.IO.Path.Combine(tempDirectoryPath, Core.Globals.LoginUsername + ".zip");
                        if (File.Exists(auditZipPath))
                        {
                            File.Delete(auditZipPath);
                        }

                        FileUtil.ClearCache();
                        FileUtil.DeleteCache(this.ApplicationContext);

                        GlobalsAndroid.masterDB = null;

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        Finish();
                    });
                    builder.Show();
                    return;
            }

            if (!IsFinishing)
            {
                SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .CommitAllowingStateLoss();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    mDrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("WORKAROUND_FOR_BUG_19917_KEY", "WORKAROUND_FOR_BUG_19917_VALUE");
            base.OnSaveInstanceState(outState);
        }

        public override void OnBackPressed()
        {

        }

        private void SyncFinishedEventHandler(object sender, MessageBusEvent e)
        {
            RunOnUiThread(() =>
            {
                mNavigationView.Menu.GetItem(1).SetChecked(true);
                ChangeTheme(Resource.Color.palette_green_primary, Resource.Color.palette_green_primary_dark);
                ListItemClicked(1);
            });
        }
    }
}
