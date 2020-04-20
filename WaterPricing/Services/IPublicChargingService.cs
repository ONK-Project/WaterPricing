using Models;
using System;
using System.Threading.Tasks;

namespace WaterPricing.Services
{
    public interface IPublicChargingService
    {
        Task<PriceAndTaxes> GetWaterPriceForDate(DateTime dateTime);
    }
}
