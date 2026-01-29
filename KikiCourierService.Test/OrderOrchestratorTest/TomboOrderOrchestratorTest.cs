using KikiCourierService.Models;
using KikiCourierService.OderOrchestrator;
using KikiCourierService.Services.CostCalculator;
using KikiCourierService.Services.DeliveryScheduler;
using KikiCourierService.Services.InputParsers;
using Moq;

namespace KikiCourierService.Test.OrderOrchestratorTest
{
    [TestFixture]
    public class TomboOrderOrchestratorTest
    {
        private Mock<IInputParser> _inputParserMock;
        private Mock<ICostCalculator> _costCalculatorMock;
        private Mock<IDeliveryScheduler> _deliverySchedulerMock;
        private TomboOrderOrchestrator _orchestrator;

        [SetUp]
        public void SetUp()
        {
            _inputParserMock = new Mock<IInputParser>();
            _costCalculatorMock = new Mock<ICostCalculator>();
            _deliverySchedulerMock = new Mock<IDeliveryScheduler>();
            _orchestrator = new TomboOrderOrchestrator(
                _inputParserMock.Object,
                _costCalculatorMock.Object,
                _deliverySchedulerMock.Object
            );
        }

        [Test]
        public async Task Should_ProcessPackageDeliveryAsync_ValidInput_ReturnsDeliveryResponse()
        {
            var args = new[] { "input.txt" };
            var package = new Package("PKG1", 10, 10, "OFR001");
            var inputData = new InputData
            {
                BaseDeliveryCost = 100,
                Packages = new List<Package> { package },
                VehicleInfo = new VehicleInfo(1,60,100)
            };

            _inputParserMock.Setup(x => x.ParseAsync(args[0])).ReturnsAsync(inputData);
            _costCalculatorMock.Setup(x => x.CalculateBaseCost(100, package)).Returns(150);
            _costCalculatorMock.Setup(x => x.CalculateDiscount(150, package)).ReturnsAsync(10);
            _costCalculatorMock.Setup(x => x.CalculateTotalDeliveryCost(150, 10)).ReturnsAsync(140);

            var result = await _orchestrator.ProcessPackageDeliveryAsync(args);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.DeliveryEstimates.Count, Is.EqualTo(1));
            Assert.That(result.DeliveryEstimates[0].PackageId, Is.EqualTo("PKG1"));
            Assert.That(result.DeliveryEstimates[0].Discount, Is.EqualTo(10));
            Assert.That(result.DeliveryEstimates[0].TotalCost, Is.EqualTo(140));
            _deliverySchedulerMock.Verify(x => x.EstimateDelivery(inputData, result), Times.Once);
        }

        [Test]
        public async Task Should_ProcessPackageDeliveryAsync_NullInputData_ReturnsNull()
        {
            var args = new[] { "input.txt" };
            _inputParserMock.Setup(x => x.ParseAsync(args[0])).ReturnsAsync((InputData)null);

            var result = await _orchestrator.ProcessPackageDeliveryAsync(args);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Should_ProcessPackageDeliveryAsync_Exception_ReturnsNull()
        {
            var args = new[] { "input.txt" };
            _inputParserMock.Setup(x => x.ParseAsync(args[0])).ThrowsAsync(new System.Exception("Test exception"));

            var result = await _orchestrator.ProcessPackageDeliveryAsync(args);

            Assert.That(result, Is.Null);
        }
    }
}
