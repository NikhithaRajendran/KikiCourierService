using KikiCourierService.Models;

namespace KikiCourierService.Services.CostCalculator
{
    public class DiscountCalculator : IDiscountCalculator
    {
        public async Task<decimal> CalculateDiscount(decimal TotalCost, Package package, List<Offer> offers)
        {
            try
            {
                var offer = GetOffer(package.OfferCode, offers);
                if (offer != null && offer.IsOfferApplicable(package))
                {
                    return offer.DiscountPercent * TotalCost / 100;
                }
                return 0;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error occured while calculating discount price for package {package.Id} - {ex.ToString()}");
                return 0;
            }
           
        }
        public Offer? GetOffer(string offerCode, List<Offer> offers)
        {
            return offers.FirstOrDefault(o => o.OfferCode == offerCode);
        }
    }
}
