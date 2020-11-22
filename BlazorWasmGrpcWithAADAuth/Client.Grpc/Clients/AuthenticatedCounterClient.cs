using System;
using System.Threading;
using System.Threading.Tasks;
using Count;
using Grpc.Core;
using Grpc.Net.Client;
using Client.Authentication.Interfaces;
using Client.Grpc.Builders;
using Client.Grpc.Interfaces;

namespace Client.Grpc.Clients
{
    internal class AuthenticatedCounterClient : IAuthenticatedCounterClient
    {
        private readonly IScopeContext<IAuthenticatedCounterClient> _context;
        private readonly IBearerTokenProvider _tokenProvider;
        private readonly Counter.CounterClient _client;

        private CancellationTokenSource? cts;

        public AuthenticatedCounterClient(GrpcChannel channel, IScopeContext<IAuthenticatedCounterClient> context, IBearerTokenProvider tokenProvider)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));

            _client = new Counter.CounterClient(channel);
        }

        public async Task<AsyncServerStreamingCall<CounterResponse>> StartAsync(int start)
        {
            cts = new CancellationTokenSource();

            var scopes = _context.GetScopes();

            string token = await _tokenProvider.GetTokenAsync(scopes);

            var metadataHeaders = new MetadataBuilder().WithToken(token).Build();

            return _client.StartCounter(new CounterRequest { Start = start }, metadataHeaders, cancellationToken: cts.Token);
        }

        public void Stop()
        {
            cts?.Cancel();
            cts?.Dispose();
        }
    }
}