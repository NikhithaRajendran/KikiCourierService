using KikiCourierService.Models;

namespace KikiCourierService.Services.VehicleManager
{
    public class VehicleManager : IVehicleManager
    {
        public List<Vehicle> GetVehicles(VehicleInfo vehicleInfo)
        {
            var vehicles = new List<Vehicle>();
            for (int i = 1; i <= vehicleInfo.NumberOfVehicles; i++)
            {
                vehicles.Add(new Vehicle(i, vehicleInfo.MaxCarriableWeight, vehicleInfo.MaxSpeed));
            }
            return vehicles;
        }

        public void updateVehicleAvailability(Vehicle vehicle, decimal deliveryTime)
        {
            vehicle.AvailableAt += 2 * deliveryTime;
        }
        public Vehicle GetNextAvailableVehicle(List<Vehicle> vehicles)
        {
            return vehicles.OrderBy(v => v.AvailableAt).First();
        }

    }
}
