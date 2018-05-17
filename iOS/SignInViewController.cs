using Foundation;
using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

using MyPatchSG.BL.Globals;
using MyPatchSG.iOS.Const;
using MyPatchSG.iOS.Globals;

using BemCheckBox;

namespace MyPatchSG.iOS
{
    public partial class SignInViewController : UIViewController
    {
        private bool bRememberMe = false;
        BemCheckBox.BemCheckBox checkbox;

        public SignInViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ConfigureView();

            GlobalsiOS.loginViewController = this;
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
            // Release any cached data, images, etc that aren't in use.
        }

        // Actions

        partial void DidTapOnLogin(NSObject sender)
        {
            TransitionToHome();
        }

        partial void DidTapOnRemember(NSObject sender)
        {
            bRememberMe = !bRememberMe;
            checkbox.On = bRememberMe;
        }

        // Custom Methods

        private void ConfigureView()
        {
            CALayer bottomBorderUsername = new CALayer();
            bottomBorderUsername.BorderColor = Constants.DIVIDER_COLOR.CGColor;
            bottomBorderUsername.BorderWidth = 1;
            bottomBorderUsername.Frame = new CGRect(0, this.tfUsername.Frame.Height - 1.0f, this.tfUsername.Frame.Size.Width, 1.0f);
            this.tfUsername.Layer.AddSublayer(bottomBorderUsername);

            CALayer bottomBorderPassword = new CALayer();
            bottomBorderPassword.BorderColor = Constants.DIVIDER_COLOR.CGColor;
            bottomBorderPassword.BorderWidth = 1;
            bottomBorderPassword.Frame = new CGRect(0, this.tfPassword.Frame.Height - 1.0f, this.tfPassword.Frame.Size.Width, 1.0f);
            this.tfPassword.Layer.AddSublayer(bottomBorderPassword);

            checkbox = new BemCheckBox.BemCheckBox(new CGRect(0, 5, 20, 20), new MyBemCheckBoxDelegate());
            this.vwRemember.AddSubview(checkbox);

            bRememberMe = true;
            checkbox.On = bRememberMe;
        }

        private void TransitionToHome()
        {
            MainRootController mainRootController = new MainRootController();
            this.NavigationController.PushViewController(mainRootController, true);
            (UIApplication.SharedApplication.Delegate as AppDelegate).MainRootController = mainRootController;
        }
    }

    class MyBemCheckBoxDelegate : BemCheckBoxDelegate
    {

        public override void DidTapCheckBox(bool checkBoxIsOn)
        {

        }
    }
}