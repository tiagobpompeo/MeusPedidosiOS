using System;
using System.Collections.Generic;
using Foundation;
using MeusPedidosiOS.Contracts;
using MeusPedidosiOS.Contracts.Data;
using MeusPedidosiOS.Delegate;
using MeusPedidosiOS.Models;
using MeusPedidosiOS.Services.BaseCacheService;
using MeusPedidosiOS.Services.ConnectionService;
using MeusPedidosiOS.Services.Data;
using UIKit;

namespace MeusPedidosiOS
{
    public partial class DetailViewController : UITableViewController
    {

        #region Attributes and Properties
        public object DetailItem { get; set; }
        UITableView table;
        BaseService baseService;
        AlertDelegateClass alertDialog;
        public IConnectionService _connection;
        public ICatalogDataService _catalogDataService;
        List<Products> tableItems = new List<Products>();
        private UIAlertView alert;
        #endregion

        protected DetailViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.

            _connection = new ConnectionService();
            _catalogDataService = new CatalogDataService();
            baseService = new BaseService();
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

        public async void ConfigureView()
        {
            table = new UITableView(View.Bounds);
            table.AutoresizingMask = UIViewAutoresizing.All;
            table.SeparatorColor = UIColor.FromRGB(127, 106, 0);

            var connection = await this._connection.CheckConnection();

            switch (DetailItem)
            {

                case "Home":
                    if (!connection.IsSuccess)
                    {
                        var catalogCached = await baseService.GetFromCache<List<Products>>("CatalogData");
                        if (catalogCached != null)
                        {
                            foreach (var data in catalogCached)
                            {
                                tableItems.Add(new Products
                                {
                                    Id = data.Id,
                                    Price = data.Price,
                                    Name = data.Name,
                                    Description = data.Description,
                                    Category_id = data.Category_id,
                                    Photo = data.Photo
                                });
                            }
                            table.Source = new TableSource(tableItems);
                            Add(table);
                            alert = new UIAlertView("Ops!", "Verifique as sua internet", alertDialog, "OK", "Fechar");
                            alert.Show();
                        }
                        else
                        {
                            alert = new UIAlertView("Ops!", "Verifique as sua internet", alertDialog, "OK", "Fechar");
                            alert.Show();
                        }
                    }
                    else
                    {
                        var catalogCached = await baseService.GetFromCache<List<Products>>("CatalogData");
                        if (catalogCached != null)
                        {
                            foreach (var data in catalogCached)
                            {
                                tableItems.Add(new Products
                                {
                                    Id = data.Id,
                                    Price = data.Price,
                                    Name = data.Name,
                                    Description = data.Description,
                                    Category_id = data.Category_id,
                                    Photo = data.Photo
                                });
                            }
                            table.Source = new TableSource(tableItems);
                            Add(table);
                        }
                        else
                        {
                            var products = await _catalogDataService.GetAllCatalogAsync();
                            await baseService.InsertObject("CatalogData", products, DateTimeOffset.Now.AddMinutes(10));

                            if (products != null)
                            {
                                foreach (var be in products)
                                {
                                    tableItems.Add(new Products
                                    {
                                        Id = be.Id,
                                        Price = be.Price,
                                        Name = be.Name,
                                        Description = be.Description,
                                        Category_id = be.Category_id,
                                        Photo = be.Photo
                                    });
                                }

                                table.Source = new TableSource(tableItems);
                                Add(table);
                            }
                            else
                            {
                                alert = new UIAlertView("Ops!", "Sem conexao com servidores, por favor, tente mais tarde!", alertDialog, "OK", "Fechar");
                                alert.Show();
                            }
                        }
                    }

                    break;

                case "Carrinho":
                    // Update the user interface for the detail item

                    break;

                case "Sobre o App":
                    // Update the user interface for the detail item
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("https://mercos.com/meus-pedidos/?utm_source=google&utm_medium=paidsearch&utm_campaign=mercos-meuspedidos-search&gclid=EAIaIQobChMIsNz-8OOO4QIVj1cNCh0VeQjBEAAYASAAEgI6MfD_BwE"));
                    break;
                    //UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.google.com/"));

            }


        }



        // Customize the number of sections in the table view.
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.SetRightBarButtonItem(
                 new UIBarButtonItem(UIImage.FromBundle("baseline_list_black_18dp.png")
                     , UIBarButtonItemStyle.Plain
                     , (sender, args) =>
                     {
                        new UIAlertView("Lista Categorias Clicked", "Categorias", null, "OK", null).Show();
                     })
                 , true);

            //UIBarButtonItem btn = new UIBarButtonItem();
            //btn.Image = UIImage.FromFile("Image.gif");
            //btn.Clicked += (sender, e) => { System.Diagnostics.Debug.WriteLine("Button tap"); };
            //NavigationItem.RightBarButtonItem = customButton;
            ConfigureView();
        }



        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void OpenCategories(object sender, EventArgs args)
        {

        }
    }
}


