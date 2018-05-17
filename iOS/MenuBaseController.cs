using System;
using UIKit;

using SidebarNavigation;

namespace MyPatchSG.iOS
{
    public class MenuBaseController : UIViewController
    {
        // provide access to the sidebar controller to all inheriting controllers
        protected SidebarNavigation.SidebarController SidebarController
        {
            get
            {
                return (UIApplication.SharedApplication.Delegate as AppDelegate).MainRootController.SidebarController;
            }
        }

        public MenuBaseController(IntPtr handle) : base(handle)
        {

        }

    }
}

