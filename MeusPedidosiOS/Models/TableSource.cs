using System;
using System.Collections.Generic;
using System.Net;
using Foundation;
using MeusPedidosiOS.Contracts;
using MeusPedidosiOS.Custom;
using MeusPedidosiOS.Services.ConnectionService;
using SystemConfiguration;
using UIKit;

namespace MeusPedidosiOS.Models
{
    public class TableSource : UITableViewSource
    {
        IConnectionService _defaultRouteReachability;
        List<Products> tableItems;
        NSString cellIdentifier = new NSString("TableCell");

        public TableSource(List<Products> items)
        {
            _defaultRouteReachability = new ConnectionService();
            tableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            new UIAlertView("Row Selected"
                , tableItems[indexPath.Row].Name, null, "OK", null).Show();
            tableView.DeselectRow(indexPath, true);
        }
            
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // request a recycled cell to save memory
            CustomCatalogCell cell = tableView.DequeueReusableCell(cellIdentifier) as CustomCatalogCell;

            // if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new CustomCatalogCell(cellIdentifier);
            }

            var connectionStatus =  _defaultRouteReachability.CheckConnectionTest();

            if (!connectionStatus) 
            {
                cell.UpdateCell(UIImage.FromFile("Images/ic_launcher.png"), tableItems[indexPath.Row].Name, tableItems[indexPath.Row].Price.ToString());
            }
            else
            {
                var url = new NSUrl(tableItems[indexPath.Row].Photo);
                var data = NSData.FromUrl(url);
                cell.UpdateCell(UIImage.LoadFromData(data), tableItems[indexPath.Row].Name, tableItems[indexPath.Row].Price.ToString());
            }           
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 75f;
        }
       
    }
}