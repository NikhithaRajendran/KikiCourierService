using KikiCourierService.Models;
using KikiCourierService.Services.CostCalculator;
using Moq;

namespace KikiCourierService.Test.ServiceTests
{
    [TestFixture]
    public class CostCalculatorTests
    {
        private Mock<IDiscountCalculator> _discountCalculatorMock;
        private Mock<IOfferCalculator> _offerCalculatorMock;
        private CostCalculator _costCalculator;

        [SetUp]
        public void SetUp()
        {
            _discountCalculatorMock = new Mock<IDiscountCalculator>();
            _offerCalculatorMock = new Mock<IOfferCalculator>();
            _costCalculator = new CostCalculator(_discountCalculatorMock.Object, _offerCalculatorMock.Object);
        }

        [Test]
        public async Task Should_CalculateTotalDeliveryCost_ValidInputs_ReturnsCorrectTotal()
        {
            decimal baseCost = 100;
            decimal discount = 20;

            var result = await _costCalculator.CalculateTotalDeliveryCost(baseCost, discount);

            Assert.That(result, Is.EqualTo(80));
        }

        [Test]
        public void Should_CalculateBaseCost_ValidInputs_ReturnsCorrectBaseCost()
        {
            decimal baseDeliveryCost = 50;
            var package = new Package("1", 2, 5);

            var result = _costCalculator.CalculateBaseCost(baseDeliveryCost, package);

            Assert.That(result, Is.EqualTo(50 + (2 * 10) + (5 * 5)));
        }

        [Test]
        public void Should_CalculateBaseCost_Exception_ReturnsZero()
        {
            decimal baseDeliveryCost = 50;
            Package? package = null;

            var result = _costCalculator.CalculateBaseCost(baseDeliveryCost, package);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task Should_CalculateDiscount_EmptyOfferCode_ReturnsZero()
        {
            decimal totalCost = 100;
            var package = new Package("1",5,10,""); 

            var result = await _costCalculator.CalculateDiscount(totalCost, package);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task Should_CalculateDiscount_ValidOfferCode_ReturnsDiscount()
        {
            decimal totalCost = 100;
            var package = new Package("1", 5, 10, "OFFER1");
            List<Offer> offers = new List<Offer>();

            _offerCalculatorMock.Setup(x => x.LoadOffersAsync()).ReturnsAsync(offers);
            _discountCalculatorMock.Setup(x => x.CalculateDiscount(totalCost, package, offers)).ReturnsAsync(15);

            var result = await _costCalculator.CalculateDiscount(totalCost, package);

            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public async Task Should_CalculateDiscount_Exception_ReturnsZero()
        {
            decimal totalCost = 100;
            var package = new Package("1", 5, 10, "OFFER1");

            _offerCalculatorMock.Setup(x => x.LoadOffersAsync()).ThrowsAsync(new Exception("Test exception"));

            var result = await _costCalculator.CalculateDiscount(totalCost, package);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}