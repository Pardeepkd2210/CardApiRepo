using CardsApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardsApi.Core.Services
{
    public interface ICardService
    {
        public Task<CardsData> GetCards(string color, string name, string type);
    }
}
