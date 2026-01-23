using KikiCourierService.Models;
using KikiCourierService.Services.CostCalculator;
using KikiCourierService.Services.DeliveryScheduler;
using KikiCourierService.Services.InputParsers;

namespace KikiCourierService.OderOrchestrator
{
    public class TomboOrderOrchestrator : IOrderOrchestrator
    {
        private readonly IInputParser _inputParser;
        private readonly ICostCalculator _costCalculator;
        private readonly IDeliveryScheduler _deliveryScheduler;
        public TomboOrderOrchestrator(IInputParser inputParser, ICostCalculator costCalculator, IDeliveryScheduler deliveryScheduler)
        {
            _inputParser = inputParser;
            _costCalculator = costCalculator;
            _deliveryScheduler = deliveryScheduler;

        }

        public async Task<DeliveryResponse?> ProcessPackageDeliveryAsync(string[] args)
        {
            try
            {
                InputData? inputData = await _inputParser.ParseAsync(args[0]);
                if (inputData == null)
                {
                    return null;
                }
                DeliveryResponse outputData = new DeliveryResponse();
                foreach (var package in inputData.Packages)
                {
                    DeliveryEstimate deliveryEstimate = new DeliveryEstimate();
                    deliveryEstimate.PackageId = package.Id;
                    deliveryEstimate.TotalCost = _costCalculator.CalculateTotalDeliveryCost(inputData.BaseDeliveryCost, package, out decimal discount);
                    deliveryEstimate.Discount = discount;
                    outputData.DeliveryEstimates.Add(deliveryEstimate);
                }
                if (inputData.VehicleInfo != null)
                {
                    _deliveryScheduler.EstimateDelivery(inputData, outputData);

                }
                foreach (var estimate in outputData.DeliveryEstimates)
                {
                    Console.WriteLine($"{estimate.PackageId} {estimate.Discount} {estimate.TotalCost} {estimate.EstimatedDeliveryTime}");

                }
                return outputData;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error occured while estimating delivery for packages - {ex.ToString()}");
                return null;
            }
            
        }
    } }
