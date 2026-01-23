namespace KikiCourierService.Models
{
    public class DeliveryResponse
    {
        public List<DeliveryEstimate> DeliveryEstimates { get; set; }
        public DeliveryResponse ()
        {
            DeliveryEstimates = new List<DeliveryEstimate> ();
        }
    }
}
