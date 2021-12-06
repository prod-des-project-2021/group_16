using Blazored.LocalStorage;
using ChessWebClient.Authentication;
using ChessWebClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            /*builder.Services.AddHttpClient<IGameService, GameService>(client =>
            {
                client.BaseAddress = new Uri(("https://localhost:44363"));
            });

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
            {
                client.BaseAddress = new Uri(("https://localhost:44363"));
            });*/

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44363") });


            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();

            await builder.Build().RunAsync();
        }
    }
}
