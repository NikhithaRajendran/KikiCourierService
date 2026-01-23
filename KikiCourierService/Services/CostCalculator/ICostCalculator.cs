using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public interface ICostCalculator
    {
        decimal CalculateBaseCost(decimal baseDeliveryCost, Package package);
        decimal CalculateDiscount(decimal totalCost, Package package);
        decimal CalculateTotalDeliveryCost(decimal baseDeliveryCost, Package package, out decimal discount);
    }
}
