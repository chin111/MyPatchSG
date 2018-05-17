using System;

using CoreGraphics;
using UIKit;

using MyPatchSG.iOS.Const;

namespace MyPatchSG.iOS
{
    public class CustomNavController : UINavigationController
    {
        public CustomNavController()
        {
            SetCustomStyle();
        }

        public CustomNavController(UIViewController rootViewController) : base(rootViewController)
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
            // Set Background Color Primary Color
            this.NavigationBar.BarTintColor = Constants.PALETTE_CUSTOM_GREY_PRIMARY;

            // Set Title Color White
            this.NavigationBar.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };



        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        public override bool PrefersStatusBarHidden()
        {
            return false;
        }
    }
}

