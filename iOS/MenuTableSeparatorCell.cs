using System;

using Foundation;
using UIKit;

namespace MyPatchSG.iOS
{
    public partial class MenuTableSeparatorCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MenuTableSeparatorCell");
        public static readonly UINib Nib = UINib.FromName("MenuTableSeparatorCell", NSBundle.MainBundle);

        static MenuTableSeparatorCell()
        {
            Nib = UINib.FromName("MenuTableSeparatorCell", NSBundle.MainBundle);
        }

        public static MenuTableSeparatorCell Create()
        {
            return (MenuTableSeparatorCell)Nib.Instantiate(null, null)[0];
        }

        protected MenuTableSeparatorCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
