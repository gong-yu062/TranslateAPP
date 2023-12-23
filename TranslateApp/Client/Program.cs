using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace TranslateApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<TranslationService>();
        builder.Services.AddScoped<TranslationResponse>();
        builder.Services.AddScoped<TranslationInputModel>();
        builder.Services.AddScoped<TranslationResult>();
        builder.Services.AddScoped<Data>();
        builder.Services.AddScoped<Translation>();
        await builder.Build().RunAsync();
    }
}