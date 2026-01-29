namespace KikiCourierService.Models
{
    public class OfferConfiguration
    {
        public List<OfferData> Offers { get; set; } = new List<OfferData>();
    }

    public class OfferData
    {
        public string OfferCode { get; set; } = String.Empty;
        public int MinDistance { get; set; }
        public int MaxDistance { get; set; }
        public int MinWeight { get; set; }
        public int MaxWeight { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
