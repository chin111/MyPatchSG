// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyPatchSG.iOS.Classes.Modules.SideMenu
{
    [Register ("MenuTableCell")]
    partial class MenuTableCell
    {
        [Outlet]
        UIKit.UIImageView ivIcon { get; set; }

        [Outlet]
        UIKit.UILabel lbTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ivIcon != null)
            {
                ivIcon.Dispose();
                ivIcon = null;
            }

            if (lbTitle != null)
            {
                lbTitle.Dispose();
                lbTitle = null;
            }
        }
    }
}