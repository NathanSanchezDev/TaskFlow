using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TaskFlow.Client.Services;  // Added using directive
using Microsoft.AspNetCore.Components.Web;

namespace TaskFlow.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient with the base address of your API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5212/") });
            
            // Register TaskItemService
            builder.Services.AddScoped<TaskItemService>();

            await builder.Build().RunAsync();
        }
    }
}