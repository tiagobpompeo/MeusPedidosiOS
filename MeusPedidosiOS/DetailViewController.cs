using System;

using UIKit;

namespace MeusPedidosiOS
{
    public partial class DetailViewController : UITableViewController
    {
        public object DetailItem { get; set; }
        string[] nomes = { "Mac", "Jessica", "MiMi" };

        protected DetailViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetDetailItem(object newDetailItem)
        {
            if (DetailItem != newDetailItem)
            {
                DetailItem = newDetailItem;

                // Update the view
                ConfigureView();
            }
        }

        void ConfigureView()
        {

            switch (DetailItem) 
            {

                case "Home":
                    // Update the user interface for the detail item               


                    break;

                case "Carrinho":
                    // Update the user interface for the detail item
                   
                    break;

                case "Sobre o App":
                    // Update the user interface for the detail item
                   
                    break;

            }


        }

        public override UITableView TableView { get => base.TableView; set => base.TableView = value; }

        // Customize the number of sections in the table view.
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

