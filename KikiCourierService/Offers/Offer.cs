using KikiCourierService.Models;

namespace KikiCourierService.Offers
{
    public abstract class Offer
    {
        public abstract string OfferCode { get; }
        public abstract decimal MaxDistance { get; }
        public abstract decimal MinDistance { get; }
        public abstract decimal MinWeight { get; }
        public abstract decimal MaxWeight { get; }
        public abstract decimal DiscountPercent { get; } 

        public bool IsOfferApplication(Package package)
        {
            return package.Distance >= MinDistance && package.Distance <= MaxDistance &&
                package.Weight >= MinWeight && package.Weight <= MaxWeight;
        }
    }
}
