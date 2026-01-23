using KikiCourierService.Models;
using KikiCourierService.Offers;

namespace KikiCourierService.Services.CostCalculator
{
    public interface IDiscountCalculator
    {
        decimal CalculateDiscount(decimal TotalCost, Package package);
        Offer? GetOffer(string offerCode);
    }
}
