using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AW.Client;
using SharedLibrary.Components.Modal.Interfaces;
using SharedLibrary.Components.Modal.Models;
using SharedLibrary.Components.Modal.Services;

namespace AW.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddScoped<IModalService, ModalService>();
        builder.Services.AddTransient<IAnimationStrategy, DefaultAnimationStrategy>();

        await builder.Build().RunAsync();
    }
}
