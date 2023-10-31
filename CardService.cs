using CardsApi.Core.Client;
using CardsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CardsApi.Core.Services
{
  
    public class CardService : ICardService
    {
        private readonly IApiClient _client;
        private const string CONTENT_TYPE = "application/json";
        public CardService(IApiClient client)
        {
            _client = client;
        }

        /// <summary>
        /// This method is used to get card based the filters
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<CardsData> GetCards(string colors, string name, string type)
        {
            try
            {
                HttpResponseMessage response = null;
                string requestBody = JsonConvert.SerializeObject(RequestFormation(colors, name, type));
                string baseUrl = "https://api.magicthegathering.io/";// we can kept this base URL in config file.
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "v1/cards");
                request.Content = new StringContent(requestBody, Encoding.UTF8, CONTENT_TYPE);
                response =  await _client.ExecuteAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                CardsData cardsData = JsonConvert.DeserializeObject<CardsData>(responseContent);
                cardsData.Success = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode) {
                    cardsData.ErrorMsg = responseContent;
                }
                return cardsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This private is used to add the parameters into request
        /// </summary>
        /// <param name="color"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Dictionary<string, string> RequestFormation(string color, string name, string type) 
        {
            var dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(color))
            {
                dictionary.Add("colors", color);
            }
            if (!string.IsNullOrEmpty(name))
            {
                dictionary.Add("name", name);
            }
            if (!string.IsNullOrEmpty(type))
            {
                dictionary.Add("type", type);
            }
            if (string.IsNullOrEmpty(color) && string.IsNullOrEmpty(name) && string.IsNullOrEmpty(type))
            {
                return dictionary;
            }
            return dictionary;
        }
    }
}
