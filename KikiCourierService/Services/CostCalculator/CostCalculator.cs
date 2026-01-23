using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IDiscountCalculator _dicountCalculator;
        public CostCalculator(IDiscountCalculator discountCalculator)
        {
            _dicountCalculator = discountCalculator;
        }
        public decimal CalculateTotalDeliveryCost(decimal baseDeliveryCost, Package package, out decimal discount)
        {
            try
            {
                var baseCost = CalculateBaseCost(baseDeliveryCost, package);
                discount = CalculateDiscount(baseCost, package);
                return baseCost - discount;               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while calculating delivery price {ex.ToString()}");
                throw;
            }

        }
        public decimal CalculateBaseCost(decimal baseDeliveryCost, Package package)
        {
            try
            {
                return baseDeliveryCost + (package.Weight * 10) + (package.Distance * 5); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating base cost for package {package.Id} - {ex.ToString()}");
                throw;
            }
        }
        public decimal CalculateDiscount(decimal totalCost, Package package)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(package.OfferCode))
                    return 0;

                return _dicountCalculator.CalculateDiscount(totalCost, package);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating discount for package {package.Id} - {ex.ToString()}");
                return 0;
            }
        }
    }
}
