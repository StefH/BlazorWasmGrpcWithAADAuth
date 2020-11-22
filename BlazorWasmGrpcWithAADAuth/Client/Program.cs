using System;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Authentication.Interfaces;
using Client.Grpc.Interfaces;
using Client.Scopes;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    public class Program
    {
#if USE_IDENTITY
        internal static string[] Scopes = { "821eb724-edb8-4dba-b425-3f953250c0ae/API.Access" };
#else
        internal static string[] Scopes = { "api://821eb724-edb8-4dba-b425-3f953250c0ae/API.Access" };
#endif

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

                // Add Scopes
                foreach (var scope in Scopes)
                {
                    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
                }
            });

            builder.Services.AddClientAuthentication();
            builder.Services.AddGrpc(builder.HostEnvironment.BaseAddress);

            builder.Services.AddSingleton<IScopeContext<IAuthenticatedCounterClient>, CounterClientScopeContext>();

            //builder.Services.AddGrpcBearerTokenProvider();

            //builder.Services.AddScoped(services =>
            //{
            //    // Create a channel with a GrpcWebHandler that is addressed to the backend server. GrpcWebText is used because server streaming requires it.
            //    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
            //    return GrpcChannel.ForAddress(builder.HostEnvironment.BaseAddress, new GrpcChannelOptions { HttpHandler = httpHandler });
            //});

            await builder.Build().RunAsync();
        }
    }
}
