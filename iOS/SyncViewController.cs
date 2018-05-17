using Foundation;
using System;
using System.Collections.Generic;

using CoreGraphics;
using UIKit;

using MyPatchSG.iOS.Const;
using MyPatchSG.iOS.Globals;

namespace MyPatchSG.iOS
{
    public partial class SyncViewController : UIViewController
    {
        public SyncViewController (IntPtr handle) : base (handle)
        {
        }

        // provide access to the sidebar controller to all inheriting controllers
        protected SidebarNavigation.SidebarController SidebarController
        {
            get
            {
                return (UIApplication.SharedApplication.Delegate as AppDelegate).MainRootController.SidebarController;
            }
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            ConfigureView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //StyleNavigationBar(Constants.PALETTE_BLUE_PRIMARY, Constants.PALETTE_BLUE_PRIMARY_DARK, "Dashboard");

            UIBarButtonItem leftItem = new UIBarButtonItem(UIImage.FromBundle("icoMenuWhite"), UIBarButtonItemStyle.Plain, DidTapOnMenu);
            //NavItem.LeftBarButtonItem = leftItem;

            this.NavigationController.NavigationBarHidden = false;
            this.NavigationItem.SetHidesBackButton(true, false);
            this.NavigationItem.SetLeftBarButtonItem(leftItem, false);
            this.NavigationController.NavigationBar.BarTintColor = Constants.PALETTE_AMBER_PRIMARY;
            this.NavigationController.NavigationBar.BackgroundColor = Constants.PALETTE_AMBER_PRIMARY;
            this.NavigationController.NavigationBar.TintColor = UIColor.White;

            this.Title = "Synchronization";
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        public override bool PrefersStatusBarHidden()
        {
            return false;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use
        }

        // Custom Methods

        private void ConfigureView()
        {
            this.View.SetNeedsLayout();
            this.View.LayoutIfNeeded();
        }

        private void DidTapOnMenu(object sender, EventArgs e)
        {
            if (SidebarController.IsOpen)
            {
                SidebarController.CloseMenu();
            }
            else
            {
                SidebarController.OpenMenu();
            }
        }
    }
}