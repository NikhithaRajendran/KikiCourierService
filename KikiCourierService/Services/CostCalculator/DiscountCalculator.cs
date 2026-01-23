using KikiCourierService.Models;
using KikiCourierService.Offers;

namespace KikiCourierService.Services.CostCalculator
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IDictionary<string, Offer> _offers;

        public DiscountCalculator(IEnumerable<Offer> offers)
        {
            _offers = offers.ToDictionary(o => o.OfferCode, o => o);

        }
        public decimal CalculateDiscount(decimal TotalCost, Package package)
        {
            try
            {
                var offer = GetOffer(package.OfferCode);
                if (offer != null && offer.IsOfferApplication(package))
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
        public Offer? GetOffer(string offerCode)
        {
            return _offers.TryGetValue(offerCode, out var offer) ? offer : null;
        }
    }
}
