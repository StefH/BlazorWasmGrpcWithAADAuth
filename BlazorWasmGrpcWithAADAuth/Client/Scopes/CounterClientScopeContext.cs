using Client.Authentication.Interfaces;
using Client.Grpc.Interfaces;

namespace Client.Scopes
{
    internal class CounterClientScopeContext : IScopeContext<IAuthenticatedCounterClient>
    {
        public string[] GetScopes()
        {
            return Program.Scopes;
        }
    }
}