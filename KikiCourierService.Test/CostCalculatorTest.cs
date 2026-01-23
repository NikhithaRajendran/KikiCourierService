using KikiCourierService.Models;
using KikiCourierService.Services.CostCalculator;
using Moq;

namespace KikiCourierService.Test
{
    public class Tests
    {
        private Mock<IDiscountCalculator> _mockDiscountCalculator;
        private CostCalculator _costCalculator;

        [SetUp]
        public void Setup()
        {
            _mockDiscountCalculator = new Mock<IDiscountCalculator>();
            _costCalculator = new CostCalculator(_mockDiscountCalculator.Object);
        }

        [Test]
        public void CalculateBaseCost_WithValidInputs_ReturnsCorrectBaseCost()
        {
            decimal baseDeliveryCost = 100;
            var package = new Package("1", 10, 20);
            decimal expected = 100 + (10 * 10) + (20 * 5);

            var result = _costCalculator.CalculateBaseCost(baseDeliveryCost, package);


            Assert.Equals(expected, result);
        }
    }
}
