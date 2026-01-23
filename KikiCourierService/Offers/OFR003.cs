namespace KikiCourierService.Offers
{
    public class OFR003 : Offer
    {

        public override string OfferCode { get => "OFR003"; }
        public override decimal MinDistance { get => 50; }
        public override decimal MaxDistance { get => 250; }
        public override decimal MinWeight { get => 10; }
        public override decimal MaxWeight { get => 150; }
        public override decimal DiscountPercent { get => 5; }
    };

}
