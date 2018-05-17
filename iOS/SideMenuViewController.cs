using Foundation;
using System;
using System.Drawing;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

namespace MyPatchSG.iOS
{
    public partial class SideMenuViewController : MenuBaseController
    {
        List<MenuItem> menuItems = new List<MenuItem>();

        public SideMenuViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib

            ConfigureView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use
        }

        // Custom Methods

        private void ConfigureView()
        {
            menuItems = Utils.GenerateMenuItems();

            this.tbMenuList.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            this.tbMenuList.TableFooterView = new UIView(new CGRect(0, 0, 0, 0));
            this.tbMenuList.AlwaysBounceVertical = false;

            MenuTableSource tableSource = new MenuTableSource(menuItems, this);
            this.tbMenuList.Source = tableSource;
            this.tbMenuList.ReloadData();
        }

        public void ChangeContentView(int contentId)
        {
            switch (contentId)
            {
                case 0:
                    // Exception
                    HomeViewController standardViewController = (HomeViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("HomeViewController");
                    SidebarController.ChangeContentView(new CustomNavController(standardViewController));
                    break;
                case 1:
                    // Dashboard
                    HomeViewController homeViewController = (HomeViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("HomeViewController");
                    SidebarController.ChangeContentView(new CustomNavController(homeViewController));
                    break;
                case 2:
                    // Outlet List
                    OutletListViewController outletViewController = (OutletListViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("OutletListViewController");
                    SidebarController.ChangeContentView(new CustomNavController(outletViewController));
                    break;
                case 3:
                    // UOM
                    UOMViewController uomViewController = (UOMViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("UOMViewController");
                    SidebarController.ChangeContentView(new CustomNavController(uomViewController));
                    break;
                case 4:
                    // Synchronization
                    SyncViewController syncViewController = (SyncViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("SyncViewController");
                    SidebarController.ChangeContentView(new CustomNavController(syncViewController));
                    break;
                case 5:
                    // About
                    AboutViewController aboutViewController = (AboutViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("AboutViewController");
                    SidebarController.ChangeContentView(new CustomNavController(aboutViewController));
                    break;
                case 6:
                    // LogOut

                    break;
                default:
                    HomeViewController defaultViewController = (HomeViewController)UIStoryboard.FromName("Main", null).InstantiateViewController("HomeViewController");
                    SidebarController.ChangeContentView(new CustomNavController(defaultViewController));
                    break;
            }
        }
    }
}