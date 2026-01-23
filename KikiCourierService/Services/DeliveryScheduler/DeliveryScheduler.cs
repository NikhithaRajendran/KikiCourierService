using KikiCourierService.Helpers;
using KikiCourierService.Models;
using KikiCourierService.Services.VehicleManager;

namespace KikiCourierService.Services.DeliveryScheduler
{
    public class DeliveryScheduler : IDeliveryScheduler
    {
        private readonly IVehicleManager _vehicleManager;
        public DeliveryScheduler(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }
        public void EstimateDelivery(InputData inputData, DeliveryResponse outputData)
        {
            try
            {
                List<Vehicle> vehicles = _vehicleManager.GetVehicles(inputData.VehicleInfo);
                var remainingPackages = inputData.Packages.ToList();
                while (remainingPackages.Count > 0)
                {
                    var vehicle = _vehicleManager.GetNextAvailableVehicle(vehicles);
                    var packagesToDeliver = getPackagesForVehicle(remainingPackages, vehicle.MaxCarriableWeight);
                    if (!packagesToDeliver.Any()) 
                        break;
                    decimal maxDeliveryTime = 0;
                    foreach (var package in packagesToDeliver)
                    {
                        var pack = remainingPackages.FirstOrDefault(p => p.Id == package);
                        var deliveryTime = RoundOffHelper.roundOff(TimeHelper.getTime(vehicle.MaxSpeed, pack.Distance));
                        outputData.DeliveryEstimates.FirstOrDefault(p => p.PackageId == package).EstimatedDeliveryTime = deliveryTime + vehicle.AvailableAt;
                        maxDeliveryTime = deliveryTime > maxDeliveryTime ? deliveryTime : maxDeliveryTime;
                        remainingPackages.Remove(pack);
                    }
                    _vehicleManager.updateVehicleAvailability(vehicle, RoundOffHelper.roundOff(maxDeliveryTime));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while estimating delivery time for packages - {ex.ToString()}");
            }

        }
        public List<string> getPackagesForVehicle(List<Package> remainingPackages, decimal maxCarriableWeight)
        {
            try
            {
                Dictionary<List<string>, decimal> packagesSubset = GetAllPackageSubsets(remainingPackages, maxCarriableWeight);
                return FindOptimumPackageSubset(packagesSubset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while finding packages for vehicle - {ex.ToString()}");
                return new List<string>();
            }
        }
        public Dictionary<List<string>, decimal> GetAllPackageSubsets(List<Package> remainingPackages, decimal maxCarriableWeight)
        {
            Dictionary<List<string>, decimal> packageSubsets = new Dictionary<List<string>, decimal>();
            decimal weightSum = 0;
            for (int i = 0; i < remainingPackages.Count; i++)
            {
                List<string> subsetList = new List<string>();

                weightSum = 0;
                for (int j = i; j < remainingPackages.Count; j++)
                {

                    weightSum = weightSum + remainingPackages[j].Weight;
                    if (weightSum > maxCarriableWeight)
                    {
                        weightSum = weightSum - remainingPackages[j].Weight;
                        continue;
                    }
                    if (maxCarriableWeight > weightSum)
                    {
                        subsetList.Add(remainingPackages[j].Id);
                    }

                }
                packageSubsets.Add(subsetList, weightSum);
            }
            return packageSubsets;
        }
        public List<string> FindOptimumPackageSubset(Dictionary<List<string>, decimal> packagesSubset)
        {

            var maxSubsetCount = packagesSubset.Keys.Max(p => p.Count);
            List<string> packages = new List<string>();
            if (maxSubsetCount > 0)
            {

                var maxsum = packagesSubset.Values.Max();

                packages = packagesSubset.Where(p => p.Value == maxsum).FirstOrDefault().Key;
            }
            else
            {
                packages = packagesSubset.Where(p => p.Key.Count == maxSubsetCount).FirstOrDefault().Key;

            }
            return packages;
        }
    }
}

