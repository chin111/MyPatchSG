using System;
using System.Drawing;

using CoreGraphics;
using UIKit;

namespace MyPatchSG.iOS
{
    public class BaseViewController : UIViewController
    {
        public BaseViewController(IntPtr handle) : base(handle)
        {

        }

        // Custom Methods

        protected void StyleNavigationBar(UIColor primaryColor, UIColor primaryDarkColor, string title)
        {
            this.NavigationController.SetNavigationBarHidden(true, false);

            UINavigationBar newNavBar = new UINavigationBar(new CGRect(0, 0, this.View.Bounds.Width, 64.0));
            newNavBar.BarTintColor = primaryColor;

            UINavigationItem navItem = new UINavigationItem();
            navItem.HidesBackButton = true;

            var titleView = new UIView(new CGRect(0, 0, this.View.Bounds.Width, 44.0f));
            var titleLabel = new UILabel(new CGRect(0, 0, this.View.Bounds.Width, 44.0f));
            titleLabel.Font = UIFont.SystemFontOfSize(17, UIFontWeight.Semibold);
            titleLabel.TextColor = UIColor.White;
            titleLabel.AdjustsFontSizeToFitWidth = true;
            titleLabel.Text = title;
            titleView.AddSubview(titleLabel);

            titleView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;

            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            NSLayoutConstraint trailing = NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, titleView, NSLayoutAttribute.Trailing, 1.0f, 0.0f);
            NSLayoutConstraint leading = NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, titleView, NSLayoutAttribute.Leading, 1.0f, 12.0f);
            NSLayoutConstraint bottom = NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, titleView, NSLayoutAttribute.Bottom, 1.0f, 0.0f);
            NSLayoutConstraint top = NSLayoutConstraint.Create(titleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, titleView, NSLayoutAttribute.Top, 1.0f, 0.0f);

            titleView.AddConstraint(trailing);
            titleView.AddConstraint(leading);
            titleView.AddConstraint(bottom);
            titleView.AddConstraint(top);

            navItem.TitleView = titleView;

            var statusBarView = new UIView(new CGRect(0, 0, this.View.Bounds.Width, 20));
            statusBarView.BackgroundColor = primaryDarkColor;

            newNavBar.SetItems(new[] { navItem }, false);
            newNavBar.AddSubview(statusBarView);

            this.View.AddSubview(newNavBar);
        }
    }
}

