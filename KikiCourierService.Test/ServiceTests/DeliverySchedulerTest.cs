using KikiCourierService.Models;
using KikiCourierService.Services.DeliveryScheduler;
using KikiCourierService.Services.VehicleManager;
using Moq;

namespace KikiCourierService.Test.ServiceTests
{
    [TestFixture]
    public class DeliverySchedulerTest
    {
        private Mock<IVehicleManager> _vehicleManagerMock;
        private DeliveryScheduler _deliveryScheduler;

        [SetUp]
        public void SetUp()
        {
            _vehicleManagerMock = new Mock<IVehicleManager>();
            _deliveryScheduler = new DeliveryScheduler(_vehicleManagerMock.Object);
        }

        [Test]
        public void Should_GetPackagesForVehicle_ValidPackages_ReturnsSubset()
        {
            var packages = new List<Package>
            {
                new Package("PKG1", 10, 10),
                new Package("PKG2", 20, 20),
                new Package("PKG3", 30, 30)
            };
            decimal maxCarriableWeight = 25;

            var result = _deliveryScheduler.getPackagesForVehicle(packages, maxCarriableWeight);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_GetPackagesForVehicle_NoPackages_ReturnsEmptyList()
        {
            var packages = new List<Package>();
            decimal maxCarriableWeight = 25;

            var result = _deliveryScheduler.getPackagesForVehicle(packages, maxCarriableWeight);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Should_GetAllPackageSubsets_ValidPackages_ReturnsSubsets()
        {
            var packages = new List<Package>
            {
                new Package("PKG1", 10, 10),
                new Package("PKG2", 20, 20)
            };
            decimal maxCarriableWeight = 30;

            var result = _deliveryScheduler.GetAllPackageSubsets(packages, maxCarriableWeight);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_FindOptimumPackageSubset_ValidSubsets_ReturnsBestSubset()
        {
            var subset1 = new List<string> { "PKG1" };
            var subset2 = new List<string> { "PKG2" };
            var subset3 = new List<string> { "PKG1", "PKG2" };
            var subsets = new Dictionary<List<string>, decimal>
            {
                { subset1, 10 },
                { subset2, 20 },
                { subset3, 30 }
            };

            var result = _deliveryScheduler.FindOptimumPackageSubset(subsets);

            Assert.That(result, Is.EqualTo(subset3));
        }
    }
}
