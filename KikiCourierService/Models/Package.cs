using KikiCourierService.Helpers;
namespace KikiCourierService.Models
{
    public class Package
    {
        public string Id { get; }
        public decimal Weight { get; }
        public decimal Distance { get; }
        public string OfferCode { get; set; }

        public Package(string id, decimal weight, decimal distance, string offerCode = "")
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Please provide a valid Package Id");
            if (weight <= 0)
                throw new ArgumentException("Please provide a valid Package Weight");
            if (distance <= 0)
                throw new ArgumentException("Please provide a valid Package delivery Distance");

            Id = id;
            Weight = RoundOffHelper.roundOff(weight);
            Distance = RoundOffHelper.roundOff(distance); ;
            OfferCode = offerCode;
        }
    }

}
