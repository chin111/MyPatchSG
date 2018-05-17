using System;
using System.Drawing;

using UIKit;
using Foundation;

namespace MyPatchSG.iOS.Const
{
    static class Constants
    {
        public static readonly UIColor TEXT_COLOR_PRIMARY = UIColor.Clear.FromHex(0x212121);
        public static readonly UIColor TEXT_COLOR_SECONDARY = UIColor.Clear.FromHex(0x757575);
        public static readonly UIColor DIVIDER_COLOR = UIColor.Clear.FromHex(0xBDBDBD);

        // Blue Theme
        public static readonly UIColor PALETTE_BLUE_PRIMARY         = UIColor.Clear.FromHex(0x2196F3);
        public static readonly UIColor PALETTE_BLUE_PRIMARY_DARK    = UIColor.Clear.FromHex(0x1976D2);
        public static readonly UIColor PALETTE_BLUE_PRIMARY_LIGHT   = UIColor.Clear.FromHex(0xBBDEFB);
        public static readonly UIColor PALETTE_BLUE_PRIMARY_ACCENT  = UIColor.Clear.FromHex(0x2196F3);

        // Light Blue Theme
        public static readonly UIColor PALETTE_BLUE_LIGHT_PRIMARY           = UIColor.Clear.FromHex(0x03A9F4);
        public static readonly UIColor PALETTE_BLUE_LIGHT_PRIMARY_DARK      = UIColor.Clear.FromHex(0x0288D1);
        public static readonly UIColor PALETTE_BLUE_LIGHT_PRIMARY_LIGHT     = UIColor.Clear.FromHex(0xB3E5FC);
        public static readonly UIColor PALETTE_BLUE_LIGHT_PRIMARY_ACCENT    = UIColor.Clear.FromHex(0x03A9F4);

        // Indigo Theme
        public static readonly UIColor PALETTE_INDIGO_PRIMARY               = UIColor.Clear.FromHex(0x3F51B5);
        public static readonly UIColor PALETTE_INDIGO_PRIMARY_DARK          = UIColor.Clear.FromHex(0x303F9F);
        public static readonly UIColor PALETTE_INDIGO_PRIMARY_LIGHT         = UIColor.Clear.FromHex(0xC5CAE9);
        public static readonly UIColor PALETTE_INDIGO_PRIMARY_ACCENT        = UIColor.Clear.FromHex(0x3F51B5);

        // Teal Theme
        public static readonly UIColor PALETTE_TEAL_PRIMARY          = UIColor.Clear.FromHex(0x009688);
        public static readonly UIColor PALETTE_TEAL_PRIMARY_DARK     = UIColor.Clear.FromHex(0x00796B);
        public static readonly UIColor PALETTE_TEAL_PRIMARY_LIGHT    = UIColor.Clear.FromHex(0xB2DFDB);
        public static readonly UIColor PALETTE_TEAL_PRIMARY_ACCENT   = UIColor.Clear.FromHex(0x009688);

        // Red Theme
        public static readonly UIColor PALETTE_RED_PRIMARY           = UIColor.Clear.FromHex(0xF44336);
        public static readonly UIColor PALETTE_RED_PRIMARY_DARK      = UIColor.Clear.FromHex(0xD32F2F);
        public static readonly UIColor PALETTE_RED_PRIMARY_LIGHT     = UIColor.Clear.FromHex(0xFFCDD2);
        public static readonly UIColor PALETTE_RED_PRIMARY_ACCENT    = UIColor.Clear.FromHex(0xF44336);

        // Purple Theme
        public static readonly UIColor PALETTE_PURPLE_PRIMARY        = UIColor.Clear.FromHex(0x9C27B0);
        public static readonly UIColor PALETTE_PURPLE_PRIMARY_DARK   = UIColor.Clear.FromHex(0x7B1FA2);
        public static readonly UIColor PALETTE_PURPLE_PRIMARY_LIGHT  = UIColor.Clear.FromHex(0xE1BEE7);
        public static readonly UIColor PALETTE_PURPLE_PRIMARY_ACCENT = UIColor.Clear.FromHex(0x9C27B0);

        // Green Theme
        public static readonly UIColor PALETTE_GREEN_PRIMARY         = UIColor.Clear.FromHex(0x4CAF50);
        public static readonly UIColor PALETTE_GREEN_PRIMARY_DARK    = UIColor.Clear.FromHex(0x388E3C);
        public static readonly UIColor PALETTE_GREEN_PRIMARY_LIGHT   = UIColor.Clear.FromHex(0xC8E6C9);
        public static readonly UIColor PALETTE_GREEN_PRIMARY_ACCENT  = UIColor.Clear.FromHex(0x4CAF50);

        // Orange Theme
        public static readonly UIColor PALETTE_ORANGE_PRIMARY        = UIColor.Clear.FromHex(0xFF9800);
        public static readonly UIColor PALETTE_ORANGE_PRIMARY_DARK   = UIColor.Clear.FromHex(0xF57C00);
        public static readonly UIColor PALETTE_ORANGE_PRIMARY_LIGHT  = UIColor.Clear.FromHex(0xFFE0B2);
        public static readonly UIColor PALETTE_ORANGE_PRIMARY_ACCENT = UIColor.Clear.FromHex(0xFF9800);

        // Amber Theme
        public static readonly UIColor PALETTE_AMBER_PRIMARY         = UIColor.Clear.FromHex(0xFFC107);
        public static readonly UIColor PALETTE_AMBER_PRIMARY_DARK    = UIColor.Clear.FromHex(0xFFA000);
        public static readonly UIColor PALETTE_AMBER_PRIMARY_LIGHT   = UIColor.Clear.FromHex(0xFFECB3);
        public static readonly UIColor PALETTE_AMBER_PRIMARY_ACCENT  = UIColor.Clear.FromHex(0xFFC107);

        // Grey Theme
        public static readonly UIColor PALETTE_GREY_PRIMARY          = UIColor.Clear.FromHex(0x9E9E9E);
        public static readonly UIColor PALETTE_GREY_PRIMARY_DARK     = UIColor.Clear.FromHex(0x616161);
        public static readonly UIColor PALETTE_GREY_PRIMARY_LIGHT    = UIColor.Clear.FromHex(0xF5F5F5);
        public static readonly UIColor PALETTE_GREY_PRIMARY_ACCENT   = UIColor.Clear.FromHex(0x9E9E9E);

        // Light Grey Theme
        public static readonly UIColor PALETTE_GREY_LIGHT_PRIMARY        = UIColor.Clear.FromHex(0x595959);
        public static readonly UIColor PALETTE_GREY_LIGHT_PRIMARY_DARK   = UIColor.Clear.FromHex(0x464646);
        public static readonly UIColor PALETTE_GREY_LIGHT_PRIMARY_LIGHT  = UIColor.Clear.FromHex(0xF5F5F5);
        public static readonly UIColor PALETTE_GREY_LIGHT_PRIMARY_ACCENT = UIColor.Clear.FromHex(0x595959);

        // Blue Grey Theme
        public static readonly UIColor PALETTE_GREY_BLUE_PRIMARY         = UIColor.Clear.FromHex(0x607D8B);
        public static readonly UIColor PALETTE_GREY_BLUE_PRIMARY_DARK    = UIColor.Clear.FromHex(0x455A64);
        public static readonly UIColor PALETTE_GREY_BLUE_PRIMARY_LIGHT   = UIColor.Clear.FromHex(0xCFD8DC);
        public static readonly UIColor PALETTE_GREY_BLUE_PRIMARY_ACCENT  = UIColor.Clear.FromHex(0x607D8B);

        // Custom Grey Theme
        public static readonly UIColor PALETTE_CUSTOM_GREY_PRIMARY           = UIColor.Clear.FromHex(0x343434);
        public static readonly UIColor PALETTE_CUSTOM_GREY_PRIMARY_DARK      = UIColor.Clear.FromHex(0x282828);
        public static readonly UIColor PALETTE_CUSTOM_GREY_PRIMARY_LIGHT     = UIColor.Clear.FromHex(0x595959);
        public static readonly UIColor PALETTE_CUSTOM_GREY_PRIMARY_ACCENT    = UIColor.Clear.FromHex(0x3F7818);
        public static readonly UIColor PALETTE_CUSTOM_GREY_CONTROL_NORMAL    = UIColor.Clear.FromHex(0x757575);
        public static readonly UIColor PALETTE_CUSTOM_GREY_CONTROL_HIGHLIGHT = UIColor.Clear.FromHex(0xE0E0E0);

        // Custom Red Theme
        public static readonly UIColor PALETTE_CUSTOM_RED_PRIMARY        = UIColor.Clear.FromHex(0xD32F2F);
        public static readonly UIColor PALETTE_CUSTOM_RED_PRIMARY_DARK   = UIColor.Clear.FromHex(0xB71C1C);
        public static readonly UIColor PALETTE_CUSTOM_RED_PRIMARY_LIGHT  = UIColor.Clear.FromHex(0xFFCDD2);
        public static readonly UIColor PALETTE_CUSTOM_RED_PRIMARY_ACCENT = UIColor.Clear.FromHex(0xD32F2F);

        // Custom Green Theme
        public static readonly UIColor PALETTE_CUSTOM_GREEN_PRIMARY          = UIColor.Clear.FromHex(0x388E3C);
        public static readonly UIColor PALETTE_CUSTOM_GREEN_PRIMARY_DARK     = UIColor.Clear.FromHex(0x1B5E20);
        public static readonly UIColor PALETTE_CUSTOM_GREEN_PRIMARY_LIGHT    = UIColor.Clear.FromHex(0xC8E6C9);
        public static readonly UIColor PALETTE_CUSTOM_GREEN_PRIMARY_ACCENT   = UIColor.Clear.FromHex(0x388E3C);


    }
}

