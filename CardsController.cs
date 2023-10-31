using CardsApi.Core.Services;
using CardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CardsApi.Controllers
{
    [ApiController]
    [Route("~/api/[controller]/[action]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardService _iCardService;
        public CardsController(ILogger<CardsController> logger, ICardService iCardService)
        {
            _logger = logger;
            _iCardService = iCardService;
        }

        /// <summary>
        /// method to fetch all the card & based column name 
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CardsData>> GetCards(string colors, string name, string type)
        {
            try
            {
                return await _iCardService.GetCards(colors, name, type);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, "color" + colors + "name" + name + "type" + type );
                return BadRequest(exp.Message);
            }
        }
    }
}
