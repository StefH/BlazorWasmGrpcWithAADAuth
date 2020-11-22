using System;
using System.Threading.Tasks;
using Client.Grpc.Interfaces;
using Count;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Client.Pages
{
    public partial class Counter
    {
        private bool IsStarted;

        private int currentCount = 0;

        private string Error;

        [Inject]
        public IAuthenticatedCounterClient AuthenticatedCounterClient { get; set; }

        private async Task IncrementCount()
        {
            AsyncServerStreamingCall<CounterResponse> call;

            try
            {
                call = await AuthenticatedCounterClient.StartAsync(currentCount);
                IsStarted = true;
            }
            catch (AccessTokenNotAvailableException a)
            {
                a.Redirect();
                return;
            }

            try
            {
                Error = string.Empty;
                await foreach (var message in call.ResponseStream.ReadAllAsync())
                {
                    currentCount = message.Count;
                    StateHasChanged();
                }
            }
            catch (RpcException rpcException) when (rpcException.StatusCode == StatusCode.Cancelled)
            {
                Error = rpcException.Message;
            }
            catch (RpcException rpcException) when (rpcException.StatusCode == StatusCode.Unauthenticated)
            {
                Error = rpcException.Message;
            }
            catch (Exception exception)
            {
                Error = exception.Message;
            }
        }

        private void StopCount()
        {
            AuthenticatedCounterClient.Stop();
            IsStarted = false;
        }
    }
}