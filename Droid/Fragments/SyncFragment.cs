using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Android.Content;
using Android.Support.V4.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using DSoft.Messaging;

using MyPatchSG.DL;
using MyPatchSG.BL.Enums;
using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Activities;
using MyPatchSG.Droid.Utils;
using MyPatchSG.Droid.Globals;

namespace MyPatchSG.Droid.Fragments
{
	public class SyncFragment : Fragment
	{
        public TextView TxtLoginID { get; set; }
        public TextView TxtDBVersion { get; set; }
        public Button BtnDownload { get; set; }
        public Button BtnUpload { get; set; }


        Android.App.ProgressDialog progress;
        public ServiceManager serviceManager;
        private Timer downloadCheckCallObserver;
        private Timer uploadCheckCallObserver;

        int timeLimit = 1;

        public SyncFragment()
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
            HasOptionsMenu = false;
			var view = inflater.Inflate(Resource.Layout.fragment_sync, null);

            TxtLoginID = (TextView)view.FindViewById<TextView>(Resource.Id.textview_loginid);
            TxtDBVersion = (TextView)view.FindViewById<TextView>(Resource.Id.textview_dbversion);

            TxtLoginID.Text = Core.Globals.LoginUsername;
            TxtDBVersion.Text = Core.Globals.MasterDBInfo.Version;

            BtnDownload = (Button)view.FindViewById<Button>(Resource.Id.btn_download);
            BtnDownload.Click += DownloadClicked;

            BtnUpload = (Button)view.FindViewById<Button>(Resource.Id.btn_upload);
            BtnUpload.Enabled = false;

            return view;
		}

        public override void OnResume()
        {
            base.OnResume();

            string auditDBPath = new FileUtil().GetAuditDBPath();
            if (File.Exists(auditDBPath))
            {
                if (GlobalsAndroid.AuditDBCleaned)
                {
                    BtnUpload.Enabled = false;
                    BtnUpload.Click += null;
                }
                else
                {
                    BtnUpload.Enabled = true;
                    BtnUpload.Click += UploadClicked;
                }
            }
        }

        private void DownloadClicked(object sender, EventArgs e)
        {
            ShowProgressDialog();

            timeLimit = 1;
            serviceManager = ServiceManager.SharedInstance;
            Task.Run(() => this.DownloadDBFiles());
        }

        private void UploadClicked(object sender, EventArgs e)
        {
            ShowProgressDialog();

            timeLimit = 1;
            serviceManager = ServiceManager.SharedInstance;
            Task.Run(() => this.UploadAuditDB());
        }

        private void ShowProgressDialog()
        {
            progress = new Android.App.ProgressDialog(this.Context);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetMessage("Please wait...");
            progress.SetCancelable(false);
            progress.Show();
        }

        private async Task UploadAuditDB()
        {
            Stopwatch sw = new Stopwatch();
            Core.Globals.UploadAuditDBResult = UploadDBResultType.None;

            sw.Start();
            bool uploaded = await serviceManager.UploadAuditDBFile(Core.Globals.LoginUsername).ConfigureAwait(false);
            sw.Stop();

            int elapsed = sw.Elapsed.Milliseconds;

            if (uploaded)
            {
                // Successful
                Console.WriteLine("Upload call success");

                // Add Upload Log
                var UploadDate = Core.Globals.UploadDate;
                var LoginID = Core.Globals.LoginUsername;
                var Status = Core.Globals.UploadAuditDBResult == UploadDBResultType.Success ? "OK" : Core.Globals.ErrorTitle;
                var FilePath = Core.Globals.UploadAuditDBResult == UploadDBResultType.Success ? Core.Globals.FilePath : "";
                var TotalTime = sw.Elapsed.TotalSeconds.ToString("F3");
                var AppVersion = Core.Globals.AppVersion;
                var OSType = Core.Globals.OSType;
                var OSVersion = Core.Globals.OSVersion;
                var MacAddr = Core.Globals.MACAddress;

                Task.Run(() => serviceManager.AddUploadLog(UploadDate, LoginID, Status, FilePath, TotalTime, AppVersion, OSType, OSVersion, MacAddr));
            }
            else
            {
                // There might be some error with the internet connection.
                // Unexpected error
                Console.WriteLine("Upload call failure");
            }

            if (elapsed < timeLimit * 1000)
            {
                uploadCheckCallObserver = new Timer(this.CheckUploadResult, null, timeLimit * 1000 - elapsed, System.Threading.Timeout.Infinite);
            }
            else
            {
                CheckUploadResult(null);
            }
        }

        private void CheckUploadResult(object state)
        {
            Activity.RunOnUiThread(() =>
            {
                progress.Hide();
                progress.Dismiss();
            });

            if (Core.Globals.UploadAuditDBResult == UploadDBResultType.Success)
            {
                // Upload Successful
                Activity.RunOnUiThread(() =>
                {
                    ShowSuccessToast("Uploaded Successfully");

                    BtnUpload.Enabled = false;
                    BtnUpload.Click += null;

                    GlobalsAndroid.DeleteAuditDB();
                });
            }
            else if (Core.Globals.UploadAuditDBResult == UploadDBResultType.BadRequestError || Core.Globals.UploadAuditDBResult == UploadDBResultType.NetworkError)
            {
                // Bad Request Error / Network Error
                Activity.RunOnUiThread(() =>
                {
                    ShowErrorDialog("Something went wrong. Please try it later.");
                });
            }
            else
            {
                // Error
                Activity.RunOnUiThread(() =>
                {
                    ShowErrorDialog("It looks like you are in an error that does not have the data service or something went wrong.");
                });
            }
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

            int elapsed = sw.Elapsed.Milliseconds;

            if (filenameRetrieved && Core.Globals.GetFileNameResult == GetFileNameResultType.Success)
            {
                // Successful
                Console.WriteLine("GetFileName call success");

                // Store DB Information into the App Preferences
                var appPreferences = new AppPreferences(this.Activity.ApplicationContext);
                appPreferences.SaveMasterDBInfoFileName(Core.Globals.MasterDBInfo.FileName);
                appPreferences.SaveMasterDBInfoFileSize(Core.Globals.MasterDBInfo.FileSize);
                appPreferences.SaveMasterDBInfoFileType(Core.Globals.MasterDBInfo.FileType);
                appPreferences.SaveMasterDBInfoIsComplete(Core.Globals.MasterDBInfo.IsComplete);
                appPreferences.SaveMasterDBInfoPathHttp(Core.Globals.MasterDBInfo.PathHTTP);
                appPreferences.SaveMasterDBInfoVersion(Core.Globals.MasterDBInfo.Version);

                // Clean database
                string masterDBPath = new FileUtil().GetMasterDBPath();
                if (File.Exists(masterDBPath))
                {
                    File.Delete(masterDBPath);
                }

                GlobalsAndroid.masterDB = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Start downloading databases
                sw.Start();
                bool downloaded = await serviceManager.DownloadFile().ConfigureAwait(false);
                sw.Stop();

                elapsed += sw.Elapsed.Milliseconds;

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

            
            if (elapsed < timeLimit)
            {
                downloadCheckCallObserver = new Timer(this.CheckDownloadResult, null, timeLimit * 1000 - elapsed, System.Threading.Timeout.Infinite);
            }
            else
            {
                CheckDownloadResult(null);
            }
        }

        private void CheckDownloadResult(object state)
        {
            Activity.RunOnUiThread(() =>
            {
                progress.Hide();
                progress.Dismiss();
            });

            if (Core.Globals.GetFileNameResult == GetFileNameResultType.Success)
            {
                // Success
                if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.Success)
                {
                    if (Core.Globals.DownloadAuditDBResult == DownloadDBResultType.Success)
                    {
                        // Master & Audit DB are downloaded.
                        var newEvent = new CoreMessageBusEvent("SyncFinished");
                        MessageBus.PostEvent(newEvent);
                    }
                    else if (Core.Globals.DownloadAuditDBResult == DownloadDBResultType.NotFoundError || Core.Globals.DownloadAuditDBResult == DownloadDBResultType.BadRequestError)
                    {
                        Activity.RunOnUiThread(() =>
                        {
                            ShowErrorDialog("Audit database file cannot be retrieved. You can still be able to use the app.");
                        });
                    }
                }
                else if (Core.Globals.DownloadMasterDBResult == DownloadDBResultType.NotFoundError || Core.Globals.DownloadMasterDBResult == DownloadDBResultType.BadRequestError)
                {
                    Activity.RunOnUiThread(() =>
                    {
                        ShowErrorDialog("Master database file cannot be retrieved. You can still use the existing database.");
                    });
                }
            }
            else
            {
                // Error
                Activity.RunOnUiThread(() =>
                {
                    ShowErrorDialog("It looks like you are in an error that does not have the data service or the database file information cannot be retrieved.");
                });
            }
        }

        private void ShowErrorDialog(string message)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this.Context);
            builder.SetTitle("Error");
            builder.SetMessage(message);
            builder.SetPositiveButton("OK", (sender, e) =>
            {

            });
            builder.Show();
        }

        private void ShowSuccessToast(string message)
        {
            Toast.MakeText(this.Context, message, ToastLength.Long).Show();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            //base.OnSaveInstanceState(outState);
        }
    }
}

