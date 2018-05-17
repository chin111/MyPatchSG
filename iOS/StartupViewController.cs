using Foundation;
using System;
using UIKit;

using System.Threading;
using System.Threading.Tasks;

namespace MyPatchSG.iOS
{
    public partial class StartupViewController : UIViewController
    {
        private Timer transitionTimer;

        public StartupViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ConfigureView();

            transitionTimer = new Timer(this.TransitionViewController, null, 2000, System.Threading.Timeout.Infinite);
        }

        public override void ViewWillAppear(bool animated)
        {
            this.NavigationController.SetNavigationBarHidden(true, false);

            base.ViewWillAppear(animated);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use
        }

        // Custom Methods

        private void ConfigureView()
        {

        }

        private void TransitionViewController(object state)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(delegate
            {
                SignInViewController signinController = (SignInViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("SignInViewController");
                this.NavigationController.PushViewController(signinController, true);
            });
        }

    }
}