using Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaterPricing.Services
{
    public class MockPublicChargingService : IPublicChargingService
    {
        public Task<PriceAndTaxes> GetWaterPriceForDate(DateTime dateTime)
        {
            return Task.FromResult(new PriceAndTaxes()
            {
                Price = 1.1,
                Currency = "DKK",
                Tax = 1.2,
                UnitOfMeassure = "M3"
            });
        }
    }
}
