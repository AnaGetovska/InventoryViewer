using Microsoft.AspNetCore.Components;
using InventoryViewer.Services;
using InventoryViewer.Models;
using DevExpress.Blazor;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.ComponentModel.DataAnnotations;

namespace InventoryViewer.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        [Inject] IService<ProductModel> ProductsService { get; set; }
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

        protected async Task OnEditModelSaving(GridEditModelSavingEventArgs e)
        {
            if (!e.IsNew)
            {
                try
                {
                    ProductsService.UpdateRecord((ProductModel)e.EditModel);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Record updateing was unsuccessfull:");
                    Logger.LogError(ex.ToString());
                }

            }
        }
    }
}
