using KikiCourierService.Models;

namespace KikiCourierService.Services.DeliveryScheduler
{
    public interface IDeliveryScheduler
    {
        void EstimateDelivery(InputData inputData, DeliveryResponse outputData);
        List<string>? getPackagesForVehicle(List<Package> remainingPackages, decimal MaxCarriableWeight);
        Dictionary<List<string>, decimal> GetAllPackageSubsets(List<Package> remainingPackages, decimal maxCarriableWeight);
        List<string> FindOptimumPackageSubset(Dictionary<List<string>, decimal> packagesSubset);

    }

}
