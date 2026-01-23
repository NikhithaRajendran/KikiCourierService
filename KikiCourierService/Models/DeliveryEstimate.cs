namespace KikiCourierService.Models
{
    public class DeliveryEstimate
    {
        public string PackageId { get; set; } = string.Empty;
        public decimal Discount { get; set; } = 0; 
        public decimal TotalCost { get; set; } = 0;

        private decimal _estimatedDeliveryTime;
        public decimal EstimatedDeliveryTime
        {
            get => _estimatedDeliveryTime;
            set => _estimatedDeliveryTime = Math.Round(value, 2, MidpointRounding.ToNegativeInfinity);
        }
    }
}
