using KikiCourierService.Models;
using KikiCourierService.Services.CostCalculator;

namespace KikiCourierService.Test.ServiceTests
{
    [TestFixture]
    public class DiscountCalculatorTests
    {
        private DiscountCalculator _discountCalculator;

        [SetUp]
        public void SetUp()
        {
            _discountCalculator = new DiscountCalculator();
        }

        [Test]
        public async Task Should_CalculateDiscount_ValidOfferAndApplicable_ReturnsDiscount()
        {
            decimal totalCost = 200;
            var package = new Package("1", 5, 10, "OFFER1");
            var offer = new Offer("OFFER1", 0, 200, 5, 200, 10);

            var offers = new List<Offer> { offer };

            var result = await _discountCalculator.CalculateDiscount(totalCost, package, offers);

            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public async Task Should_CalculateDiscount_OfferNotApplicable_ReturnsZero()
        {
            decimal totalCost = 200;
            var package = new Package("1", 1, 1, "OFFER1");
            var offer = new Offer("OFFER1", 0, 200, 5, 200, 10);

            var offers = new List<Offer> { offer };

            var result = await _discountCalculator.CalculateDiscount(totalCost, package, offers);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task Should_CalculateDiscount_OfferNotFound_ReturnsZero()
        {
            decimal totalCost = 200;
            var package = new Package("1", 5, 10, "OFFER2");
            var offer = new Offer("OFFER1", 0, 200, 5, 200, 10);
            var offers = new List<Offer> { offer };

            var result = await _discountCalculator.CalculateDiscount(totalCost, package, offers);

            Assert.That(result, Is.EqualTo(0));
        }

        
        [Test]
        public void Should_GetOffer_ValidOfferCode_ReturnsOffer()
        {
            var offer = new Offer("OFFER1", 0, 200, 5, 200, 10);
            var offers = new List<Offer> { offer };

            var result = _discountCalculator.GetOffer("OFFER1", offers);

            Assert.That(result, Is.EqualTo(offer));
        }

        [Test]
        public void Should_GetOffer_InvalidOfferCode_ReturnsNull()
        {
            var offer = new Offer("OFFER1", 0, 200, 5, 200, 10);
            var offers = new List<Offer> { offer };

            var result = _discountCalculator.GetOffer("OFFER2", offers);

            Assert.That(result, Is.Null);
        }

    }
}
