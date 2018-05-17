using System;
using System.Collections.Generic;
using System.Linq;

using CoreGraphics;
using Foundation;
using UIKit;

namespace MyPatchSG.iOS
{
    public class MenuTableSource : UITableViewSource
    {
        private SideMenuViewController menuController;
        private List<MenuItem> menuItems;

        private NSIndexPath lastIndexPath;

        string CellIdentifier = "MenuTableCell";
        string CellSeparatorIdentifier = "MenuTableSeparatorCell";

        public float MenuTitleFontSize = 17.0f;

        public MenuTableSource(List<MenuItem> items, SideMenuViewController owner)
        {
            menuItems = items;
            menuController = owner;
            lastIndexPath = NSIndexPath.FromRowSection(0, 0);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return menuItems.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var menuItem = menuItems[indexPath.Row];
            UITableViewCell cell = new UITableViewCell();

            if (menuItem.ItemType == 1)
            {
                // Item Cell
                var itemCell = (MenuTableCell)tableView.DequeueReusableCell(CellIdentifier);
                if (itemCell == null)
                {
                    itemCell = MenuTableCell.Create();
                }

                itemCell.Model = menuItem;
                itemCell.MenuCellTitleFontSize = MenuTitleFontSize;
                if (indexPath.Row == lastIndexPath.Row)
                {
                    itemCell.SelectCell();
                }
                else
                {
                    itemCell.DeselectCell();
                }
                itemCell.SelectionStyle = UITableViewCellSelectionStyle.None;

                cell = itemCell;
            }
            else
            {
                // Separator Cell
                var separatorCell = (MenuTableSeparatorCell)tableView.DequeueReusableCell(CellSeparatorIdentifier);
                if (separatorCell == null)
                {
                    separatorCell = MenuTableSeparatorCell.Create();
                }

                separatorCell.SelectionStyle = UITableViewCellSelectionStyle.None;

                cell = separatorCell;
            }

            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var menuItem = menuItems[indexPath.Row];
            if (menuItem.ItemType == 0)
            {
                return 16;
            }
            return 48;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var menuItem = menuItems[indexPath.Row];
            if (menuItem.ItemType == 1)
            {
                // Item Cell
                MenuTableCell cell = null;
                if (lastIndexPath != null)
                {
                    cell = (MenuTableCell)tableView.CellAt(lastIndexPath);
                    if (cell != null)
                    {
                        cell.DeselectCell();
                    }
                }

                cell = (MenuTableCell)tableView.CellAt(indexPath);
                cell.SelectCell();

                menuController.ChangeContentView(menuItem.ItemID);
                lastIndexPath = indexPath;

            }
        }
    }
}

