using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public interface IDiscountCalculator
    {
        Task<decimal> CalculateDiscount(decimal TotalCost, Package package, List<Offer> offers);
        Offer? GetOffer(string offerCode, List<Offer> offers);
    }
}
