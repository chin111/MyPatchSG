using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Diagnostics;

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

using Core = MyPatchSG.BL.Globals;
using MyPatchSG.Droid.Utils;


namespace MyPatchSG.Droid.Activities
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : BaseActivity
    {
        private InputMethodManager im;

        Android.Support.Design.Widget.TextInputLayout mInputLayoutUsername;
        Android.Support.Design.Widget.TextInputLayout mInputLayoutPassword;

        AppCompatEditText mInputUsername;
        AppCompatEditText mInputPassword;

        Color bottomLineColor = new Color(33, 150, 243);

        public ServiceManager serviceManager;
        private Timer loginCheckCallObserver;

        int timeLimit = 60;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_signin_view;
            }
        }

        protected override int ToolbarResource
        {
            get
            {
                return Resource.Id.toolbar_login;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SupportActionBar.SetDisplayHomeAsUpEnabled(false); // remove the left caret
            SupportActionBar.SetHomeButtonEnabled(false); // disable the button

            mInputUsername = FindViewById<AppCompatEditText>(Resource.Id.input_username);
            mInputPassword = FindViewById<AppCompatEditText>(Resource.Id.input_password);

            mInputUsername.TextChanged += OnInputUsernameTextChanged;
            mInputPassword.TextChanged += OnInputPasswordTextChanged;

            mInputLayoutUsername = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.input_username_layout);
            mInputLayoutPassword = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.input_password_layout);

            var btLogin = FindViewById<Button>(Resource.Id.btn_login);
            btLogin.Click += OnLoginClick;

            this.Title = "";


            im = (InputMethodManager)GetSystemService(Context.InputMethodService);

            serviceManager = ServiceManager.SharedInstance;

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
        }

        protected override void OnStart()
        {
            base.OnStart();

            //mInputLayoutUsername.Error = null;
            //mInputUsername.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
            //mInputUsername.Text = "";

            //mInputLayoutPassword.Error = null;
            //mInputPassword.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
            //mInputPassword.Text = "";
        }

        public void HideKeyboard()
        {
            im.HideSoftInputFromWindow(mInputUsername.WindowToken, 0);
            im.HideSoftInputFromWindow(mInputPassword.WindowToken, 0);
        }

        private void OnInputUsernameTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var username = mInputUsername.Text.Trim();
            if (username == "")
            {
                mInputLayoutUsername.Error = "Enter the username";
            }
            else
            {
                mInputLayoutUsername.Error = null;
                mInputUsername.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
            }
        }

        private void OnInputPasswordTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string password = mInputPassword.Text.Trim();
            if (password == "")
            {
                mInputLayoutPassword.Error = "Enter the password";
            }
            else
            {
                mInputLayoutPassword.Error = null;
                mInputPassword.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
            }
        }

        private bool IsValidEmail(string email)
        {
            return !Android.Text.TextUtils.IsEmpty(email) && Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        private bool IsValidUsername(string username)
        {
            return true;
        }

        private void OnLoginClick(object sender, EventArgs e)
        {
            HideKeyboard();

            var username = mInputUsername.Text.Trim();
            var password = mInputPassword.Text.Trim();

            if (!Validate(username, password))
            {
                if (username == "")
                {
                    mInputLayoutUsername.Error = "Enter the username";
                }
                else
                {
                    mInputLayoutUsername.Error = null;
                    mInputUsername.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
                }

                if (password == "")
                {
                    mInputLayoutPassword.Error = "Enter the password";
                }
                else
                {
                    mInputLayoutPassword.Error = null;
                    mInputPassword.Background.SetColorFilter(bottomLineColor, PorterDuff.Mode.SrcAtop);
                }

                return;
            }

            var macAddr = Core.Globals.MACAddress;

            AndHUD.Shared.Show(this, null, -1, MaskType.Clear);

            Core.Globals.LoginStatus = BL.Enums.LoginStatusType.None;
            Task.Factory.StartNew(() => Login(username, password, macAddr));

            timeLimit = 60;
            loginCheckCallObserver = new Timer(this.CheckLoginResult, null, timeLimit * 1000, Timeout.Infinite);
        }

        private async Task Login(string username, string password, string macAddr)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            bool loginResult = await serviceManager.VerifyLogin(username, password, macAddr).ConfigureAwait(false);
            sw.Stop();

            AndHUD.Shared.Dismiss(this);

            int elapsed = sw.Elapsed.Seconds;
            if (elapsed <= timeLimit)
            {
                loginCheckCallObserver.Dispose();
                loginCheckCallObserver = null;
            }

            if (loginResult)
            {
                if (Core.Globals.LoginStatus == BL.Enums.LoginStatusType.Success)
                {
                    // Login Success
                    var appPreferences = new AppPreferences(this.ApplicationContext);
                    appPreferences.SaveUsername(username);
                    appPreferences.SavePassword(password);
                    appPreferences.SaveLoginSkipped(false);

                    TransitionToDownload();
                }
                else if (Core.Globals.LoginStatus == BL.Enums.LoginStatusType.LoginError)
                {
                    // Login Failure/Error
                    ShowError();
                }
            }
            else
            {
                RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage("Something went wrong. Please try it later.");
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
        }

        private void CheckLoginResult(object state)
        {
            if (Core.Globals.LoginStatus == BL.Enums.LoginStatusType.None)
            {
                // After certain time, it's still stuck with the login request.
                // Error
                RunOnUiThread(() =>
                {
                    AndHUD.Shared.Dismiss(this);

                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage("Something went wrong. Please try it later.");
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
        }

        private void TransitionToDownload()
        {
            RunOnUiThread(() =>
            {
                var downloadActivity = new Intent(this, typeof(DownloadActivity));
                StartActivity(downloadActivity);
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

        private void ShowError()
        {
            if (Core.Globals.ErrorTitle == "invalid_grant")
            {
                RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage(Core.Globals.ErrorDescription);
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
            else if (Core.Globals.ErrorTitle == "invalid_clientId")
            {
                RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage(Core.Globals.ErrorDescription);
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
            else if (Core.Globals.ErrorTitle == "unsupported_grant_type")
            {
                RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage("Something went wrong. Please try it later.");
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
            else if (Core.Globals.ErrorTitle == "Error")
            {
                RunOnUiThread(() =>
                {
                    Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                    builder.SetTitle("Error");
                    builder.SetMessage("The username or password you entered is incorrect. ");
                    builder.SetPositiveButton("OK", (sender, e) =>
                    {

                    });
                    builder.Show();
                });
            }
        }

        private bool Validate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Down)
            {
                View v = CurrentFocus;
                if (v != null) 
                {
                    if (v.GetType() == typeof(AppCompatEditText))
                    {
                        Rect outRect = new Rect();
                        v.GetGlobalVisibleRect(outRect);
                        if (!outRect.Contains((int)ev.RawX, (int)ev.RawY))
                        {
                            v.ClearFocus();
                            HideKeyboard();
                        }
                    }
                }
            }

            return base.DispatchTouchEvent(ev);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
