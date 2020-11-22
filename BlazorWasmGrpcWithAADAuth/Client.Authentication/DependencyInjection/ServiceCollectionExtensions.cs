using Client.Authentication.Interfaces;
using Client.Authentication.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClientAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IBearerTokenProvider, BearerTokenProvider>();

            return services;
        }
    }
}