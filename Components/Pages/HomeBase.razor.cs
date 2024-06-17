using Microsoft.AspNetCore.Components;
using InventoryViewer.Services;
using InventoryViewer.Models;
using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;

namespace InventoryViewer.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject] IProductService ProductsService { get; set; }
        [Inject] ILogger<HomeBase> Logger { get; set; }

        protected IEnumerable<ProductModel> Products { get; set; }

        protected override void OnInitialized()
        {
            try {
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
                e.DataItems.ForEach(di => {
                    int? ProductId = Products.Where(c =>
                        c.Name == di.Value.ToString()).FirstOrDefault()?.Id;
                    di.DisplayText = di.DisplayText + " (ID " + ProductId + ")";
                });
            }
        }

        protected void OnDataItemDeleting(GridDataItemDeletingEventArgs e)
        {
            Console.WriteLine(e);
            //Data.Remove(e.DataItem as WeatherForecast);
        }

        //void OnEditModelSaving(GridEditModelSavingEventArgs e)
        //{
        //    var editModel = (WeatherForecast)e.EditModel;
        //    var dataItem = e.IsNew ? new WeatherForecast() : (WeatherForecast)e.DataItem;

        //    dataItem.Date = editModel.Date;
        //    dataItem.TemperatureC = editModel.TemperatureC;
        //    dataItem.CloudCover = editModel.CloudCover;

        //    if (e.IsNew)
        //        Data.Add(dataItem as WeatherForecast);
        //}
    }
}
