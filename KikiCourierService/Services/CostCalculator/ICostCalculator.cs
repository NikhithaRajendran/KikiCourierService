using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public interface ICostCalculator
    {
        decimal CalculateBaseCost(decimal baseDeliveryCost, Package package);
        Task<decimal> CalculateDiscount(decimal totalCost, Package package);
        Task<decimal> CalculateTotalDeliveryCost(decimal baseCost, decimal discount);
    }
}
