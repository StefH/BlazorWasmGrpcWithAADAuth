using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Client.Authentication.Interfaces;

namespace Client.Authentication.Services
{
    internal class BearerTokenProvider : IBearerTokenProvider
    {
        private readonly IAccessTokenProvider _provider;
        private readonly NavigationManager _navigation;

        private AccessToken? _lastToken = null;

        public BearerTokenProvider(IAccessTokenProvider provider, NavigationManager navigation)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<string> GetTokenAsync(params string[] scopes)
        {
            var now = DateTimeOffset.Now;

            if (_lastToken == null || now >= _lastToken.Expires.AddMinutes(-5))
            {
                var tokenResult = scopes?.Length > 0 ?
                    await _provider.RequestAccessToken(new AccessTokenRequestOptions { Scopes = scopes }) :
                    await _provider.RequestAccessToken();

                if (tokenResult.TryGetToken(out _lastToken))
                {
                    return _lastToken.Value;
                }

                throw new AccessTokenNotAvailableException(_navigation, tokenResult, scopes);
            }

            return _lastToken.Value;
        }
    }
}