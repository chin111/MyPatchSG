using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MyPatchSG.DL;
using MyPatchSG.BL.Enums;
using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Utils;

namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "DownloadActivity")]
    public class DownloadActivity : BaseActivity
    {
        public ServiceManager serviceManager;
        private Timer downloadCheckCallObserver;

        public RelativeLayout DownloadProcessingLayout;
        public RelativeLayout DownloadErrorFilenameLayout;
        public RelativeLayout DownloadErrorMasterLayout;
        public RelativeLayout DownloadErrorAuditLayout;

        public Button BtnDownloadFilenameRetry;
        public Button BtnDownloadMasterRetry;
        public Button BtnDownloadAuditContinue;
        public Button BtnDownloadAuditRetry;

        public Button BtnDownloadFilenameLogout;
        public Button BtnDownloadMasterLogout;

        int timeLimit = 5;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_download_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar_download;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.SetDisplayHomeAsUpEnabled(false); // remove the left caret
            SupportActionBar.SetHomeButtonEnabled(false); // disable the button

            this.Title = "Downloading";

            DownloadProcessingLayout = FindViewById<RelativeLayout>(Resource.Id.download_layout_processing);
            DownloadErrorFilenameLayout = FindViewById<RelativeLayout>(Resource.Id.download_layout_error_filename);
            DownloadErrorMasterLayout = FindViewById<RelativeLayout>(Resource.Id.download_layout_error_master);
            DownloadErrorAuditLayout = FindViewById<RelativeLayout>(Resource.Id.download_layout_error_audit);

            BtnDownloadFilenameRetry = FindViewById<Button>(Resource.Id.download_error_filename_btn_retry);
            BtnDownloadMasterRetry = FindViewById<Button>(Resource.Id.download_error_master_btn_retry);
            BtnDownloadAuditContinue = FindViewById<Button>(Resource.Id.download_error_audit_btn_continue);
            BtnDownloadAuditRetry = FindViewById<Button>(Resource.Id.download_error_audit_btn_retry);

            BtnDownloadFilenameLogout = FindViewById<Button>(Resource.Id.download_error_filename_btn_logout);
            BtnDownloadMasterLogout = FindViewById<Button>(Resource.Id.download_error_master_btn_logout);

            DownloadProcessingLayout.Visibility = ViewStates.Visible;
            DownloadErrorFilenameLayout.Visibility = ViewStates.Gone;
            DownloadErrorMasterLayout.Visibility = ViewStates.Gone;
            DownloadErrorAuditLayout.Visibility = ViewStates.Gone;

            BtnDownloadFilenameRetry.Click += DownloadErrorFilenameRetryClicked;
            BtnDownloadMasterRetry.Click += DownloadErrorMasterRetryClicked;
            BtnDownloadAuditRetry.Click += DownloadErrorAuditRetryClicked;
            BtnDownloadAuditContinue.Click += DownloadErrorAuditContinueClicked;

            BtnDownloadFilenameLogout.Click += DownloadErrorFilenameLogoutClicked;
            BtnDownloadMasterLogout.Click += DownloadErrorMasterLogoutClicked;

            serviceManager = ServiceManager.SharedInstance;
            Task.Run(() => this.DownloadDBFiles());
            timeLimit = 5;
            downloadCheckCallObserver = new Timer(this.CheckDownloadResult, null, timeLimit * 1000, System.Threading.Timeout.Infinite);
        }

        private void DownloadErrorFilenameRetryClicked(object sender, EventArgs e)
        {
            DownloadProcessingLayout.Visibility = ViewStates.Visible;
            DownloadErrorFilenameLayout.Visibility = ViewStates.Gone;

            Task.Run(() => this.DownloadDBFiles());
            timeLimit = 1;
            downloadCheckCallObserver = new Timer(this.CheckDownloadResult, null, timeLimit * 1000, System.Threading.Timeout.Infinite);
        }
        private void DownloadErrorMasterRetryClicked(object sender, EventArgs e)
        {
            DownloadProcessingLayout.Visibility = ViewStates.Visible;
            DownloadErrorMasterLayout.Visibility = ViewStates.Gone;

            Task.Run(() => this.DownloadOnlyDBFiles());
            timeLimit = 1;
            downloadCheckCallObserver = new Timer(this.CheckDownloadResult, null, timeLimit * 1000, System.Threading.Timeout.Infinite);
        }
        private void DownloadErrorAuditRetryClicked(object sender, EventArgs e)
        {
            DownloadProcessingLayout.Visibility = ViewStates.Visible;
            DownloadErrorAuditLayout.Visibility = ViewStates.Gone;

            Task.Run(() => this.DownloadOnlyAuditDBFile());
            timeLimit = 1;
            downloadCheckCallObserver = new Timer(this.CheckDownloadResult, null, timeLimit * 1000, System.Threading.Timeout.Infinite);
        }
        private void DownloadErrorAuditContinueClicked(object sender, EventArgs e)
        {
            TransitionToMain();
        }

        private void DownloadErrorFilenameLogoutClicked(object sender, EventArgs e)
        {
            Logout();
        }
        private void DownloadErrorMasterLogoutClicked(object sender, EventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            Finish();
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
        }

        private void PrepareTestSpec()
        {
            // Download Success
            //Globals.DownloadResult = DownloadResultType.Success;

            // Download Error - Bad Request
            //Globals.DownloadResult = DownloadResultType.BadRequestError;

            // Download Error - Not Found
            //Globals.DownloadResult = DownloadResultType.NotFoundError;

            // Download Error - Network
            //Globals.DownloadResult = DownloadResultType.NetworkError;
        }
        private async Task DownloadDBFiles()
        {
            Stopwatch sw = new Stopwatch();
            Core.Globals.GetFileNameResult = GetFileNameResultType.None;
            Core.Globals.DownloadMasterDBResult = DownloadDBResultType.None;
            Core.Globals.DownloadAuditDBResult = DownloadDBResultType.None;

            string UserName = Core.Globals.LoginUsername;

            DateTime Today = DateTime.Now;

            sw.Start();
            bool filenameRetrieved = await serviceManager.GetFileName(UserName, Core.Globals.AppID, Core.Globals.OSType).ConfigureAwait(false);
            sw.Stop();

            int elapsed = sw.Elapsed.Seconds;

            if (filenameRetrieved && Core.Globals.GetFileNameResult == GetFileNameResultType.Success)
            {
                // Successful
                Console.WriteLine("GetFileName call success");

                // Store DB Information into the App Preferences
                var appPreferences = new AppPreferences(this.ApplicationContext);
                appPreferences.SaveMasterDBInfoFileName(Core.Globals.MasterDBInfo.FileName);
                appPreferences.SaveMasterDBInfoFileSize(Core.Globals.MasterDBInfo.FileSize);
                appPreferences.SaveMasterDBInfoFileType(Core.Globals.MasterDBInfo.FileType);
                appPreferences.SaveMasterDBInfoIsComplete(Core.Globals.MasterDBInfo.IsComplete);
                appPreferences.SaveMasterDBInfoPathHttp(Core.Globals.MasterDBInfo.PathHTTP);
                appPreferences.SaveMasterDBInfoVersion(Core.Globals.MasterDBInfo.Version);

                // Clean database
                MasterDB masterDB = null;
                string dbPath = new FileUtil().GetMasterDBPath();
                if (File.Exists(dbPath))
                {
                    masterDB = new MasterDB();
                    masterDB.dbPath = dbPath;
                    masterDB.CleanDatabase();
                }

                // Start downloading databases
                sw.Start();
                bool downloaded = await serviceManager.DownloadFile().ConfigureAwait(false);
                sw.Stop();

                elapsed += sw.Elapsed.Seconds;

                if (downloaded)
                {
                    // Successful
                    Console.WriteLine("DownloadFile call success");

                    // Add Download Log
                    var DownloadDate = Today.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var LoginID = Core.Globals.LoginUsername;
                    var Status = "OK";
                    if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.Success)
                    {
                        Status = "OK";
                    }
                    else if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.NotFoundError || Core.Globals.DownloadMasterDBResult == DownloadDBResultType.BadRequestError)
                    {
                        Status = "Error 404 File Not Found";
                    }
                    var FilePath = Core.Globals.MasterDBInfo.PathHTTP + Core.Globals.MasterDBInfo.FileName;
                    var TotalTime = sw.Elapsed.TotalSeconds.ToString("F3");
                    var AppVersion = Core.Globals.AppVersion;
                    var OSType = Core.Globals.OSType;
                    var OSVersion = Core.Globals.OSVersion;
                    var MacAddr = Core.Globals.MACAddress;

                    Task.Run(() => serviceManager.AddDownloadLog(DownloadDate, LoginID, Status, FilePath, TotalTime, AppVersion, OSType, OSVersion, MacAddr));
                }
                else
                {
                    // There might be some error with the internet connection.
                    // Unexpected error
                    Console.WriteLine("DownloadFile call failure");
                }
            }
            else
            {
                // There might be some error with the internet connection.
                // Unexpected error
                Console.WriteLine("GetFileName call failure");

            }

            if (elapsed <= timeLimit)
            {
                //Do Nothing
                //As CheckLoginStatus would be called later to do the stuff

            }
            else
            {
                CheckDownloadResult(null);
            }
        }
        private async Task DownloadOnlyDBFiles()
        {
            Core.Globals.DownloadMasterDBResult = DownloadDBResultType.None;
            Core.Globals.DownloadAuditDBResult = DownloadDBResultType.None;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool downloaded = await serviceManager.DownloadFile().ConfigureAwait(false);
            sw.Stop();

            int elapsed = sw.Elapsed.Seconds;

            if (elapsed < timeLimit)
            {
                //Do Nothing
                //As CheckLoginStatus would be called later to do the stuff

            }
            else
            {
                CheckDownloadResult(null);
            }
        }

        private async Task DownloadOnlyAuditDBFile()
        {
            Core.Globals.DownloadAuditDBResult = DownloadDBResultType.None;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool downloaded = await serviceManager.DownloadAuditDBFile().ConfigureAwait(false);
            sw.Stop();

            int elapsed = sw.Elapsed.Seconds;

            if (elapsed < timeLimit)
            {
                //Do Nothing
                //As CheckLoginStatus would be called later to do the stuff

            }
            else
            {
                CheckDownloadResult(null);
            }
        }
        private void CheckDownloadResult(object state)
        {
            //PrepareTestSpec();

            if (Core.Globals.GetFileNameResult == GetFileNameResultType.Success)
            {
                // Success
                if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.Success)
                {
                    var appPreferences = new AppPreferences(this.ApplicationContext);
                    appPreferences.SaveMasterDBDownloaded(true);

                    if (Core.Globals.DownloadAuditDBResult == DownloadDBResultType.Success)
                    {
                        appPreferences.SaveAuditDBDownloaded(true);

                        TransitionToMain();
                    }
                    else if (Core.Globals.DownloadAuditDBResult == DownloadDBResultType.NotFoundError || Core.Globals.DownloadAuditDBResult == DownloadDBResultType.BadRequestError)
                    {
                        RunOnUiThread(() =>
                        {
                            DownloadProcessingLayout.Visibility = ViewStates.Gone;
                            DownloadErrorAuditLayout.Visibility = ViewStates.Visible;
                        });
                    }
                }
                else if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.NotFoundError || Core.Globals.DownloadMasterDBResult == DownloadDBResultType.BadRequestError)
                {
                    RunOnUiThread(() =>
                    {
                        DownloadProcessingLayout.Visibility = ViewStates.Gone;
                        DownloadErrorMasterLayout.Visibility = ViewStates.Visible;
                    });
                }
            }
            else
            {
                // Error
                RunOnUiThread(() =>
                {
                    DownloadProcessingLayout.Visibility = ViewStates.Gone;
                    DownloadErrorFilenameLayout.Visibility = ViewStates.Visible;
                });
            }
        }

        private void TransitionToMain()
        {
            RunOnUiThread(() =>
            {
                var main = new Intent(this, typeof(MainActivity));
                StartActivity(main);
                Finish();
            });
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

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("WORKAROUND_FOR_BUG_19917_KEY", "WORKAROUND_FOR_BUG_19917_VALUE");
            base.OnSaveInstanceState(outState);
        }
    }
}
