using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MeusPedidosiOS.Custom
{
    public class CustomCatalogCell : UITableViewCell
    {

        UILabel headingLabel, subheadingLabel;
        UIImageView imageView;

        public CustomCatalogCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            SelectionStyle = UITableViewCellSelectionStyle.Gray;

            ContentView.BackgroundColor = UIColor.White;
            imageView = new UIImageView();        


            headingLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 16f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };

            subheadingLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };

            ContentView.Add(imageView);
            ContentView.Add(headingLabel);
            ContentView.Add(subheadingLabel);
           
        }

        public void UpdateCell(UIImage image,string caption, string subtitle)
        {
            imageView.Image = image;
            headingLabel.Text = caption;
            subheadingLabel.Text = subtitle;
        }

       
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            imageView.Frame = new CGRect(5, 10, 75, 50);
            headingLabel.Frame = new CGRect(85, 10, ContentView.Bounds.Width - 63, 25);
            subheadingLabel.Frame = new CGRect(20, 30, ContentView.Bounds.Width - 63, 20);
        }

       
    }
}