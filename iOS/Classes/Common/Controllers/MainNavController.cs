using System;

using CoreGraphics;
using UIKit;

using MyPatchSG.iOS.Const;

namespace MyPatchSG.iOS
{
    public class MainNavController : UINavigationController
    {
        public MainNavController()
        {
            SetCustomStyle();
        }

        public MainNavController(UIViewController rootViewController) : base(rootViewController)
        {
            SetCustomStyle();
        }

        public override bool ShouldAutorotate()
        {
            return false;
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.PortraitUpsideDown;
        }

        private void SetCustomStyle()
        {
            // Set Tint Color
            this.NavigationBar.TintColor = UIColor.White;

            // Set Title Color White
            this.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

            // Set Navigation Bar Transparent
            //this.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            //this.NavigationBar.ShadowImage = new UIImage();
            //this.NavigationBar.Translucent = true;

        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}

