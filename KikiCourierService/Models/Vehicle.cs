using KikiCourierService.Helpers;

namespace KikiCourierService.Models
{
    public class Vehicle
    {
        public int Id { get; }
        public decimal MaxCarriableWeight { get; }
        public decimal MaxSpeed { get; }
        public decimal AvailableAt { get; set; } = 0;

        public Vehicle(int id, decimal maxCarriableWeight, decimal maxSpeed)
        {
            Id = id;
            MaxCarriableWeight = RoundOffHelper.roundOff(maxCarriableWeight);
            MaxSpeed = RoundOffHelper.roundOff(maxSpeed); 
            AvailableAt = 0;
        }
    }
}
