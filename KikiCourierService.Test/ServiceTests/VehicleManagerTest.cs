using KikiCourierService.Models;
using KikiCourierService.Services.VehicleManager;

namespace KikiCourierService.Test.ServiceTests
{
    [TestFixture]
    public class VehicleManagerTest
    {
        private VehicleManager _vehicleManager;

        [SetUp]
        public void SetUp()
        {
            _vehicleManager = new VehicleManager();
        }

        [Test]
        public void Should_GetVehicles_ValidVehicleInfo_ReturnsCorrectVehicles()
        {
            var vehicleInfo = new VehicleInfo(2, 60, 100);
            
            var vehicles = _vehicleManager.GetVehicles(vehicleInfo);

            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles.Count, Is.EqualTo(2));
            Assert.That(vehicles[0].MaxCarriableWeight, Is.EqualTo(100));
            Assert.That(vehicles[0].MaxSpeed, Is.EqualTo(60));
        }

        [Test]
        public void Should_GetVehicles_NullVehicleInfo_ReturnsEmptyList()
        {
            var vehicles = _vehicleManager.GetVehicles(null);

            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_UpdateVehicleAvailability_UpdatesAvailableAtCorrectly()
        {
            var vehicle = new Vehicle(1, 100, 60) { AvailableAt = 5 };
            decimal deliveryTime = 3;

            _vehicleManager.updateVehicleAvailability(vehicle, deliveryTime);

            Assert.That(vehicle.AvailableAt, Is.EqualTo(5 + 2 * 3));
        }

        [Test]
        public void Should_GetNextAvailableVehicle_ReturnsVehicleWithLowestAvailableAt()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle(1, 100, 60) { AvailableAt = 10 },
                new Vehicle(2, 100, 60) { AvailableAt = 5 },
                new Vehicle(3, 100, 60) { AvailableAt = 15 }
            };

            var nextVehicle = _vehicleManager.GetNextAvailableVehicle(vehicles);

            Assert.That(nextVehicle.Id, Is.EqualTo(2));
        }
    }
}
