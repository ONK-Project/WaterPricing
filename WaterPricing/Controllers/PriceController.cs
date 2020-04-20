using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Threading.Tasks;
using WaterPricing.Services;

namespace WaterPricing.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PriceController : ControllerBase
    {


        private readonly ILogger<PriceController> _logger;
        private readonly IPublicChargingService _publicChargingService;

        public PriceController(
            ILogger<PriceController> logger,
            IPublicChargingService publicChargingService)
        {
            _logger = logger;
            _publicChargingService = publicChargingService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime dateTime,
            [FromQuery] double ressourceUsage,
            [FromQuery] string unitOfMeassure)
        {
            var priceRequest = new PriceRequest()
            {
                DateTime = dateTime,
                RessourceUsage = ressourceUsage,
                UnitOfMeassure = unitOfMeassure
            };

            var totalPrice = await CalculateSubmissionPrice(priceRequest);

            return Ok(totalPrice);
        }

        private async Task<SubmissionPrice> CalculateSubmissionPrice(PriceRequest priceRequest)
        {
            var waterPriceInfo = await _publicChargingService.GetWaterPriceForDate(priceRequest.DateTime);
            var price = priceRequest.RessourceUsage * (waterPriceInfo.Price + waterPriceInfo.Tax);

            var submissionPrice = new SubmissionPrice()
            {
                Currency = waterPriceInfo.Currency,
                TotalCost = price,
            };

            return submissionPrice;
        }
    }
}
