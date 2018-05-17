using System;
using System.Collections.Generic;

namespace MyPatchSG.iOS
{
    public static class Utils
    {
        public static List<MenuItem> GenerateMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem{
                    ItemID = 1,
                    Title = "Dashboard",
                    ItemIconName = "icoDashboard36pt",
                    ItemType = 1
                },
                new MenuItem{
                    ItemID = 2,
                    Title = "Outlet List",
                    ItemIconName = "icoList36pt",
                    ItemType = 1
                },
                new MenuItem{
                    ItemID = 3,
                    Title = "UOM",
                    ItemIconName = "icoSettings36pt",
                    ItemType = 1
                },
                new MenuItem{
                    ItemID = 4,
                    Title = "Synchronization",
                    ItemIconName = "icoSync36pt",
                    ItemType = 1
                },
                new MenuItem{
                    ItemID = 5,
                    Title = "About",
                    ItemIconName = "icoHelp36pt",
                    ItemType = 1
                },
                new MenuItem{
                    ItemID = 0,
                    Title = "",
                    ItemIconName = "",
                    ItemType = 0
                },
                new MenuItem{
                    ItemID = 6,
                    Title = "Log Out",
                    ItemIconName = "icoExit",
                    ItemType = 1
                }
            };
        }
    }
}

