using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IDiscountCalculator _dicountCalculator;
        private readonly IOfferCalculator _offerCalculator;
        public CostCalculator(IDiscountCalculator discountCalculator, IOfferCalculator offerCalculator)
        {
            _dicountCalculator = discountCalculator;
            _offerCalculator = offerCalculator;
        }
        public async Task<decimal> CalculateTotalDeliveryCost(decimal baseCost, decimal discount)
        {
            return baseCost - discount;
        }
        public decimal CalculateBaseCost(decimal baseDeliveryCost, Package package)
        {
            try
            {
                return baseDeliveryCost + (package.Weight * 10) + (package.Distance * 5);
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
