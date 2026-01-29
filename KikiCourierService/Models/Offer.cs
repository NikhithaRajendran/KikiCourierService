namespace KikiCourierService.Models
{
    public class Offer
    {
        public string OfferCode { get; }
        public decimal MaxDistance { get; }
        public decimal MinDistance { get; }
        public decimal MinWeight { get; }
        public decimal MaxWeight { get; }
        public decimal DiscountPercent { get; } 

        public bool IsOfferApplicable(Package package)
        {
            return package.Distance >= MinDistance && package.Distance <= MaxDistance &&
                package.Weight >= MinWeight && package.Weight <= MaxWeight;
        }
        public Offer(string offerCode, int minDistance, int maxDistance,
                      int minWeight, int maxWeight, int discountPercent)
        {
            OfferCode = offerCode;
            MinDistance = minDistance;
            MaxDistance = maxDistance;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            DiscountPercent = discountPercent;
        }
    }
}
