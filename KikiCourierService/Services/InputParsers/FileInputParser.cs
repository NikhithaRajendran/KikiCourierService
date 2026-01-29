using KikiCourierService.Models;

namespace KikiCourierService.Services.InputParsers
{
    public class FileInputParser : IInputParser
    {
        public async Task<InputData?> ParseAsync(string filePath)
        {
            var fileInputLines = await GetContent(filePath);            
            if (fileInputLines==null ||fileInputLines.Length == 0)
            {
                Console.WriteLine("Please provide valid package details");
                return null;
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
                    return null;
                }
                if (int.TryParse(headerParts[1], out var packageCount))
                {
                    inputData.NumberOfPackages = packageCount;
                }
                else
                {
                    Console.WriteLine("Please provide a valid package count");
                    return null;
                }

                for (int i = 1; i <= inputData.NumberOfPackages; i++)
                {
                    var package = GetPackageData(fileInputLines[i]);
                    if (package == null)
                    {
                        Console.WriteLine("Invalid Package Details");
                        return null;
                    }
                    inputData.Packages.Add(package);
                }
                if (fileInputLines.Length > packageCount + 1)
                {
                    var vehicleInfo = GetVehicleInfo(fileInputLines[packageCount + 1]);
                    if (vehicleInfo == null)
                    {
                        Console.WriteLine("Invalid Vehicle Information");
                        return null;
                    }
                    inputData.VehicleInfo = vehicleInfo;
                }
                return inputData;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while parsing input {ex.ToString()}");
                return null;
            }


        }
        public async Task<string[]?> GetContent(string fileName)
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
                return null;
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
            if (decimal.TryParse(packageInfo[1], out var packageWeight))
            {
                if(decimal.TryParse(packageInfo[2], out var packageDistance))
                {
                    Package package = new Package(packageInfo[0], packageWeight, packageDistance);
                    if (packageInfo.Length > 3)
                        package.OfferCode = packageInfo[3];
                    return package;

                }
                else
                {
                    Console.WriteLine("Please provide a valid package distance");
                    return null;
                }
            } 
            else
            {
                Console.WriteLine("Please provide a valid package weigtht");
                return null;
            }             
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
