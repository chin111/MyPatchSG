using System;

using Foundation;
using UIKit;

using MyPatchSG.iOS.Const;

namespace MyPatchSG.iOS
{
    public partial class MenuTableCell : UITableViewCell
    {
        public MenuItem Model { get; set; }
        public float MenuCellTitleFontSize = 17.0f;

        public static readonly NSString Key = new NSString("MenuTableCell");
        public static readonly UINib Nib = UINib.FromName("MenuTableCell", NSBundle.MainBundle);

        static MenuTableCell()
        {
            Nib = UINib.FromName("MenuTableCell", NSBundle.MainBundle);
        }

        public static MenuTableCell Create()
        {
            return (MenuTableCell)Nib.Instantiate(null, null)[0];
        }

        protected MenuTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.lbTitle.Text = Model.Title;
            this.lbTitle.Font = UIFont.SystemFontOfSize(MenuCellTitleFontSize);

            UIImage imgItem = UIImage.FromBundle(Model.ItemIconName);
            this.ivIcon.Image = imgItem.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        }

        public void SelectCell()
        {
            this.lbTitle.TextColor = Constants.PALETTE_CUSTOM_GREY_PRIMARY;
            this.ivIcon.TintColor = Constants.PALETTE_CUSTOM_GREY_PRIMARY;
            this.BackgroundColor = Constants.PALETTE_CUSTOM_GREY_CONTROL_HIGHLIGHT;
        }

        public void DeselectCell()
        {
            this.lbTitle.TextColor = Constants.TEXT_COLOR_PRIMARY;
            this.ivIcon.TintColor = Constants.TEXT_COLOR_SECONDARY;
            this.BackgroundColor = UIColor.White;
        }
    }
}
