using InventoryViewer.Components;
using InventoryViewer.Models;
using InventoryViewer.Repositories;
using InventoryViewer.Services;
using InventoryViewer.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});
builder.Services.AddSingleton<DatabaseSeeder>();
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<IProductService<ProductModel>, ProductService>();


// Configure the HTTP request pipeline.
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var seeder = app.Services.GetRequiredService<DatabaseSeeder>();
seeder.EnsureDatabaseExists();

app.Run();
