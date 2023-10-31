using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CardsApi.Core.Client
{
    
    public class ApiClient : IApiClient
    {
        private readonly HttpClient client;
        public ApiClient()
        {
            client = new HttpClient();
        }
        public async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request)
        {
            ValidateInputs(request);
            HttpResponseMessage response = null;
            try
            {
                response = await client.SendAsync(request);
                if (response == null)
                {
                    throw new OperationCanceledException($"No response from service at {request.RequestUri}, (response is null).");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong!! Please try again latter.");
                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
            return response;
        }

        private void ValidateInputs(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentException("request must be non-null.");
            }
            if (request.RequestUri == null || request.Method == null)
            {
                throw new ArgumentException("request.RequestUri and request.Method must both be specified.");
            }
        }
    }
}
