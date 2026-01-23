namespace KikiCourierService.Offers
{
    public class OFR002 : Offer
    {

        public override string OfferCode { get => "OFR002"; }
        public override decimal MinDistance { get => 50; }
        public override decimal MaxDistance { get => 150; }
        public override decimal MinWeight { get => 100; }
        public override decimal MaxWeight { get => 250; }
        public override decimal DiscountPercent { get => 7; }
    };

}
