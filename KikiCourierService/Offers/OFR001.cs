namespace KikiCourierService.Offers
{
    public class OFR001 : Offer
    {

        public override string OfferCode { get => "OFR001"; }
        public override decimal MinDistance { get => 0; }
        public override decimal MaxDistance { get => 200; }
        public override decimal MinWeight { get => 70; }
        public override decimal MaxWeight { get => 200; }
        public override decimal DiscountPercent { get => 10; }
    };

}
