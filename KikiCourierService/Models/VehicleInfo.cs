using KikiCourierService.Helpers;

namespace KikiCourierService.Models
{
    public class VehicleInfo
    {
        public int NumberOfVehicles { get; }
        public decimal MaxSpeed { get; }
        public decimal MaxCarriableWeight { get; }

        public VehicleInfo(int numberOfVehicles, decimal maxSpeed, decimal maxCarriableWeight)
        {
            if (numberOfVehicles <= 0)
                throw new ArgumentException("Please provide a input for number of vehicles");
            if (maxSpeed <= 0)
                throw new ArgumentException("Please provide a valid vehicle speed");
            if (maxCarriableWeight <= 0)
                throw new ArgumentException("Please provide a weight capacity vehicles");

            NumberOfVehicles = numberOfVehicles;
            MaxSpeed = RoundOffHelper.roundOff(maxSpeed);
            MaxCarriableWeight = RoundOffHelper.roundOff(maxCarriableWeight); ;
        }
    }
}
