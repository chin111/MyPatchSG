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

namespace MyPatchSG.iOS
{
    [Register ("SignInViewController")]
    partial class SignInViewController
    {
        [Outlet]
        UIKit.UITextField tfUsername { get; set; }

        [Outlet]
        UIKit.UITextField tfPassword { get; set; }

        [Outlet]
        UIKit.UIView vwRemember { get; set; }


        [Action ("DidTapOnLogin:")]
        partial void DidTapOnLogin (Foundation.NSObject sender);


        [Action ("DidTapOnRemember:")]
        partial void DidTapOnRemember (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (tfPassword != null) {
                tfPassword.Dispose ();
                tfPassword = null;
            }

            if (tfUsername != null) {
                tfUsername.Dispose ();
                tfUsername = null;
            }

            if (vwRemember != null) {
                vwRemember.Dispose ();
                vwRemember = null;
            }
        }
    }
}