using KikiCourierService.Models;
using Microsoft.Extensions.Configuration;

namespace KikiCourierService.Services.CostCalculator
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IDiscountCalculator _dicountCalculator;
        private readonly IOfferCalculator _offerCalculator;
        private readonly IConfiguration _configuration;
        public CostCalculator(IDiscountCalculator discountCalculator, IOfferCalculator offerCalculator, IConfiguration configuration)
        {
            _dicountCalculator = discountCalculator;
            _offerCalculator = offerCalculator;
            _configuration = configuration;
        }
        public async Task<decimal> CalculateTotalDeliveryCost(decimal baseCost, decimal discount)
        {
            return baseCost - discount;
        }
        public decimal CalculateBaseCost(decimal baseDeliveryCost, Package package)
        {
            try
            {
                decimal.TryParse(_configuration["Constants:WeightMultiplier"], out decimal weightMultiplier);
                decimal.TryParse(_configuration["Constants:DistanceMultiplier"], out decimal distanceMultiplier);
                return baseDeliveryCost + (package.Weight * weightMultiplier) + (package.Distance * distanceMultiplier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating base cost for package - {ex.ToString()}");
                return 0;
            }
        }
        public async Task<decimal> CalculateDiscount(decimal totalCost, Package package)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(package.OfferCode))
                    return 0;

                var offers = await _offerCalculator.LoadOffersAsync();
                return await _dicountCalculator.CalculateDiscount(totalCost, package, offers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating discount for package - {ex.ToString()}");
                return 0;
            }
        }
    }
}
