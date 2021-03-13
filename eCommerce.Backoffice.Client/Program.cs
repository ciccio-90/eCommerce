using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using eCommerce.Backoffice.Client.Services.Interfaces;
using eCommerce.Backoffice.Client.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace eCommerce.Backoffice.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
            builder.Services.AddScoped<ILoginService, CustomAuthStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
            builder.Services.AddOptions();
            await builder.Build().RunAsync();
        }
    }
}
