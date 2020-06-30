using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorWasmGrpcWithAADAuth.Client.Services
{
    public static class GrpcBearerTokenProviderExtensions
    {
        public static IServiceCollection AddGrpcBearerTokenProvider(this IServiceCollection services)
        {
            services.TryAddScoped<GrpcBearerTokenProvider>();
            return services;
        }
    }

    public class GrpcBearerTokenProvider
    {
        private const int CacheInMinutes = 5;

        private readonly IAccessTokenProvider _provider;
        private readonly NavigationManager _navigation;
        private AccessToken _lastToken;
        private string _cachedToken;

        public GrpcBearerTokenProvider(IAccessTokenProvider provider, NavigationManager navigation)
        {
            _provider = provider;
            _navigation = navigation;
        }

        public async Task<string> GetTokenAsync(params string[] scopes)
        {
            var now = DateTimeOffset.Now;

            if (_lastToken == null || now >= _lastToken.Expires.AddMinutes(-CacheInMinutes))
            {
                var tokenResult = scopes?.Count() > 0 ?
                    await _provider.RequestAccessToken(new AccessTokenRequestOptions { Scopes = scopes }) :
                    await _provider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    _lastToken = token;
                    _cachedToken = _lastToken.Value;
                }
                else
                {
                    throw new AccessTokenNotAvailableException(_navigation, tokenResult, scopes);
                }
            }

            return _cachedToken;
        }
    }
}