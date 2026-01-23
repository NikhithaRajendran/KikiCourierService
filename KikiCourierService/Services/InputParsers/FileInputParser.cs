using KikiCourierService.Models;

namespace KikiCourierService.Services.InputParsers
{
    public class FileInputParser : IInputParser
    {
        public async Task<InputData?> ParseAsync(string filePath)
        {
            var fileInputLines = await GetContent(filePath);            
            if (fileInputLines.Length == 0)
            {
                throw new ArgumentException("Please provide valid package details");
            }
            try
            {
                InputData inputData = new InputData();
                var headerParts = fileInputLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (decimal.TryParse(headerParts[0], out var baseCost))
                {
                    inputData.BaseDeliveryCost = baseCost;
                }
                else
                {
                    Console.WriteLine("Please provide a valid base cost");
                    throw new ArgumentException("Invalid Base Cost");
                }
                if (int.TryParse(headerParts[1], out var packageCount))
                {
                    inputData.NumberOfPackages = packageCount;
                }
                else
                {
                    Console.WriteLine("Please provide a valid package count");
                    throw new ArgumentException("Invalid Package Count");
                }

                for (int i = 1; i <= inputData.NumberOfPackages; i++)
                {
                    var package = GetPackageData(fileInputLines[i]);
                    if (package == null)
                    {
                        throw new ArgumentException("Invalid Package Details");
                    }
                    inputData.Packages.Add(package);
                }
                if (fileInputLines.Length > packageCount + 1)
                {
                    var vehicleInfo = GetVehicleInfo(fileInputLines[packageCount + 1]);
                    if (vehicleInfo == null)
                    {
                        throw new ArgumentException("Invalid Vehicle Information");
                    }
                    inputData.VehicleInfo = vehicleInfo;
                }
                return inputData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while parsing input {ex.ToString()}");
                throw;
            }


        }
        public async Task<string[]> GetContent(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = await File.ReadAllLinesAsync(fileName);
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
                return lines;

            }
            else
            {
                Console.WriteLine("Please provide a valid filePath");
                throw new ArgumentException("Invalid File Path");
            }
        }
        public Package? GetPackageData(string packageData)
        {
            var packageInfo = packageData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (packageInfo.Length == 0)
            {
                Console.WriteLine("Please provide Package Details");
                return null;
            }
            Package package = new Package(packageInfo[0],decimal.Parse(packageInfo[1]),
                decimal.Parse(packageInfo[2]));

            if (packageInfo.Length > 3)
                package.OfferCode = packageInfo[3];
            return package;
        }
        public VehicleInfo? GetVehicleInfo(string vehicleData)
        {
            var vehicleInfo = vehicleData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var vehicle = new VehicleInfo(int.Parse(vehicleInfo[0]), decimal.Parse(vehicleInfo[1]), 
                decimal.Parse(vehicleInfo[2]));            
            return vehicle;

        }
    }
}
