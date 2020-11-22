using System.Net.Http;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Client.Grpc.Clients;
using Client.Grpc.Interfaces;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGrpc(this IServiceCollection services, string baseAddress)
        {
            services.AddScoped<IAuthenticatedCounterClient, AuthenticatedCounterClient>();

            services.AddScoped(services =>
            {
                // Create a channel with a GrpcWebHandler that is addressed to the backend server. GrpcWebText is used because server streaming requires it.
                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
                return GrpcChannel.ForAddress(baseAddress, new GrpcChannelOptions { HttpHandler = httpHandler });
            });

            return services;
        }
    }
}