using KikiCourierService.Models;

namespace KikiCourierService.OderOrchestrator
{
    public interface IOrderOrchestrator
    {
        Task<DeliveryResponse?> ProcessPackageDeliveryAsync(string[] args);
    }
}
