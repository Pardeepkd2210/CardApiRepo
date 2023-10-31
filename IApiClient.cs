
using System.Net.Http;
using System.Threading.Tasks;

namespace CardsApi.Core.Client
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request);
    }
}
