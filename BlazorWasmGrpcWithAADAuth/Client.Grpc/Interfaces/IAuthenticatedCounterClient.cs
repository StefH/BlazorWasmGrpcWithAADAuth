using System.Threading.Tasks;
using Count;
using Grpc.Core;

namespace Client.Grpc.Interfaces
{
    public interface IAuthenticatedCounterClient
    {
        Task<AsyncServerStreamingCall<CounterResponse>> StartAsync(int start);

        void Stop();
    }
}