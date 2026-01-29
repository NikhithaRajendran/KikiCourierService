using KikiCourierService.Models;

namespace KikiCourierService.Services.InputParsers
{
    public interface IInputParser
    {
        Task<string[]?> GetContent(string fileName);
        Task<InputData?> ParseAsync(string filePath);
        Package? GetPackageData(string packageData);
        VehicleInfo? GetVehicleInfo(string vehicleData);
    }
}
