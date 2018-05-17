using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MyPatchSG.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        public ServiceManager serviceManager;

        public MainRootController MainRootController { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            Xamarin.IQKeyboardManager.SharedManager.Enable = true;
            Xamarin.IQKeyboardManager.SharedManager.ShouldResignOnTouchOutside = true;

            UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.None);

            // create a new window instance based on the screen size
            CGRect bounds = UIScreen.MainScreen.Bounds;
            window = new UIWindow(bounds);

            // If you have defined a root view controller, set it here:
            StartupViewController startupController = (StartupViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("StartupViewController");
            MainNavController navController = new MainNavController(startupController);
            window.RootViewController = navController;

            // make the window visible
            window.MakeKeyAndVisible();

            return true;
        }
    }
}
