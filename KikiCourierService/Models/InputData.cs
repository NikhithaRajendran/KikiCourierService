namespace KikiCourierService.Models
{
    public class InputData
    {
        private decimal baseDeliveryCost;
        private int numberOfPackages;
        public List<Package> Packages { get; set; }
        public VehicleInfo? VehicleInfo { get; set; }
        public int NumberOfPackages
        {
            get => numberOfPackages;
            set => numberOfPackages = value >= 0 ? value : throw new ArgumentException ("Please provide a valid number of packages");
        }
        public decimal BaseDeliveryCost
        {
            get => baseDeliveryCost;
            set => baseDeliveryCost = value >= 0 ? value : throw new ArgumentException("Please provide a valid base delivery cost");
        }
        public InputData()
        {
            Packages = new List<Package>();
        }
        
    }
}
