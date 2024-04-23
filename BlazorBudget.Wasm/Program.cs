using BlazorBudget.Wasm;
using BlazorBudget.Wasm.Services;
using BlazorBudget.Wasm.Store;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<IBudgetService, BudgetServiceLocalStorage>();
builder.Services.AddScoped<ICategoryService, CategoryServiceLocalStorage>();
builder.Services.AddScoped<BudgetEffects>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools();
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
