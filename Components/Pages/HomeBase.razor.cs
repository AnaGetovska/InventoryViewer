using Microsoft.AspNetCore.Components;
using InventoryViewer.Services;
using InventoryViewer.Models;
using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.ComponentModel.DataAnnotations;
using InventoryViewer.Utils;

namespace InventoryViewer.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject] IProductService<ProductModel> ProductsService { get; set; }
        [Inject] ILogger<HomeBase> Logger { get; set; }
        protected IEnumerable<ProductModel> Products { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                Products = ProductsService.FetchAll();
            }
            catch (Exception ex)
            {
                Logger.LogError("Fetch all products was unsuccessfull:");
                Logger.LogError(ex.ToString());
            }
        }

        protected void OnCustomizeFilterMenu(GridCustomizeFilterMenuEventArgs e)
        {
            if (e.DataColumn.FieldName == "Name")
            {
                e.DataItems.ForEach(di =>
                {
                    int? ProductId = Products.Where(c =>
                        c.Name == di.Value.ToString()).FirstOrDefault()?.Id;
                    di.DisplayText = di.DisplayText + " (ID " + ProductId + ")";
                });
            }
        }

        protected void OnEditModelSaving(GridEditModelSavingEventArgs e)
        {
            if (!e.IsNew)
            {
                try
                {
                    ProductsService.UpdateRecord((ProductModel)e.EditModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Record updating was unsuccessfull:");
                    Logger.LogError(ex.ToString());
                }

            }
            else if (e.IsNew)
            {
                var product = e.EditModel as ProductModel;
                product.DateAdded = DateTime.UtcNow;
                product.LastModified = DateTime.UtcNow;
                ProductsService.AddRecord(product);
            }
        }

        protected void OnDataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            try
            {
                var product = e.DataItem as ProductModel;
                ProductsService.DeleteRecord(product.Id);
            }
            catch (Exception ex)
            {
                Logger.LogError("Product deletion failed:");
                Logger.LogError(ex.ToString());
            }
        }

        protected void OnCustomizeEditModel(GridCustomizeEditModelEventArgs e)
        {

            if (e.IsNew)
            {
                var DateAdded = DateTimeUtils.GetRandomDate();
                var LastModified = DateTime.UtcNow.Date;
                var newProduct = (ProductModel)e.EditModel;
                newProduct.Name = "Enter product name";
                newProduct.Price = 0;
                newProduct.DateAdded = DateAdded;
                newProduct.LastModified = LastModified;
            }
        }
    }
}
