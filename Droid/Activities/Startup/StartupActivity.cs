using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Widget;
using Android.Support.Design.Widget;

using MyPatchSG.BL.Entities;
using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Utils;

using SQLite;
using SQLiteNetExtensions;
using SQLitePCL;
using Xamarin.Forms;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "MyPatchSG", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/icon", NoHistory = false)]
    public class StartupActivity : BaseActivity
    {
        public ServiceManager serviceManager;
        private Timer transitionTimer;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_startup_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return 0;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            

            var PackageInfo = PackageManager.GetPackageInfo(PackageName, 0);
            Core.Globals.AndroidVersionCode = PackageInfo.VersionCode;
            Core.Globals.AndroidVersionName = PackageInfo.VersionName;
            Core.Globals.IsUpdateNeeded = false;

            Core.Globals.AppID = "1001";
            Core.Globals.AppVersion = Core.Globals.AndroidVersionCode.ToString("D6");
            Core.Globals.OSType = "ANDROID";
            Core.Globals.OSVersion = Android.OS.Build.VERSION.Release;

            Core.Globals.MACAddress = DeviceUtil.GetMACAddress();

            var appPreferences = new AppPreferences(this.ApplicationContext);
            Core.Globals.MasterDBInfo = new DBFileInfo();
            Core.Globals.MasterDBInfo.FileName = appPreferences.GetMasterDBInfoFileName();
            Core.Globals.MasterDBInfo.FileSize = appPreferences.GetMasterDBInfoFileSize();
            Core.Globals.MasterDBInfo.FileType = appPreferences.GetMasterDBInfoFileType();
            Core.Globals.MasterDBInfo.IsComplete = appPreferences.GetMasterDBInfoIsComplete();
            Core.Globals.MasterDBInfo.PathHTTP = appPreferences.GetMasterDBInfoPathHttp();
            Core.Globals.MasterDBInfo.Version = appPreferences.GetMasterDBInfoVersion();

            Core.Globals.AuditDBInfo = new DBFileInfo();

            //if (int.Parse(Core.Globals.AppInformation.Version) > Core.Globals.AndroidVersionCode)
            //{
            //    Core.Globals.IsUpdateNeeded = true;
            //} else
            //{
            //    Core.Globals.IsUpdateNeeded = false;
            //}

            //transitionTimer = new Timer(this.TransitionViewController, null, 2000, System.Threading.Timeout.Infinite);
        }

        protected override void OnResume()
        {
            base.OnResume();

            transitionTimer = new Timer(this.TransitionViewController, null, 2000, System.Threading.Timeout.Infinite);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        private void TransitionViewController(object state)
        {
            var appPreferences = new AppPreferences(this.ApplicationContext);
            string Username = appPreferences.GetUsername();
            if (Username == "")
            {
                // No User Remembered
                appPreferences.SaveLoginSkipped(false);
                TransitionToSignIn();
            }
            else
            {
                // User exists.
                appPreferences.SaveLoginSkipped(true);

                bool MasterDBDownloaded = appPreferences.GetMasterDBDownloaded();
                if (MasterDBDownloaded)
                {
                    // Master DB Downloaded
                    TransitionToMain();
                }
                else
                {
                    // Master DB Not Found
                    TransitionToDownload();
                }
            }
        }

        private void TransitionToSignIn()
        {
            RunOnUiThread(() =>
            {
                var signinActivity = new Intent(this, typeof(SignInActivity));
                StartActivity(signinActivity);
            });
        }

        private void TransitionToMain()
        {
            RunOnUiThread(() =>
            {
                var main = new Intent(this, typeof(MainActivity));
                StartActivity(main);
            });
        }

        private void TransitionToDownload()
        {
            RunOnUiThread(() =>
            {
                var downloadActivity = new Intent(this, typeof(DownloadActivity));
                StartActivity(downloadActivity);
            });
        }
    }
}
