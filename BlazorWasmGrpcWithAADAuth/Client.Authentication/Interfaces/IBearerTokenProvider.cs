using System.Threading.Tasks;

namespace Client.Authentication.Interfaces
{
    public interface IBearerTokenProvider
    {
        Task<string> GetTokenAsync(params string[] scopes);
    }
}