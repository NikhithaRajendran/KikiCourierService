using KikiCourierService.Models;

namespace KikiCourierService.Services.VehicleManager
{
    public interface IVehicleManager
    {
        List<Vehicle> GetVehicles(VehicleInfo? vehicleInfo);
        public void updateVehicleAvailability(Vehicle vehicle, decimal deliveryTime);
        public Vehicle GetNextAvailableVehicle(List<Vehicle> vehicles);

    }
}
