using System;
using CoreGraphics;
using UIKit;

using SidebarNavigation;

namespace MyPatchSG.iOS
{
    public class MainRootController : UIViewController
    {
        // the sidebar controller for the app
        public SidebarController SidebarController { get; private set; }

        public MainRootController() : base(null, null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            CGRect bounds = UIScreen.MainScreen.Bounds;

            if (this.NavigationController != null)
            {
                this.NavigationController.SetNavigationBarHidden(true, false);
            }

            SideMenuViewController menuViewController = (SideMenuViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("SideMenuViewController");
            HomeViewController homeViewController = (HomeViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("HomeViewController");

            CustomNavController navController = new CustomNavController(homeViewController);
            SidebarController = new SidebarController(this, navController, menuViewController);
            SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Left;
            SidebarController.MenuWidth = (int)bounds.Size.Width - 56;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
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

