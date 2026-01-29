using KikiCourierService.Models;
using System.Text.Json;

namespace KikiCourierService.Services.CostCalculator
{
    public class OfferCalculator : IOfferCalculator
    {
        private const string DefaultOffersFilePath = "offers.json";

        public async Task<List<Offer>> LoadOffersAsync()
        {
            return await LoadOffersAsync(DefaultOffersFilePath);
        }

        public async Task<List<Offer>> LoadOffersAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Offers file not found at {filePath}. Using default offers.");
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var offerConfiguration = JsonSerializer.Deserialize<OfferConfiguration>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }); 
                var offers = offerConfiguration.Offers.Where(o => o.IsActive).Select(CreateOfferFromData).ToList();

                return offers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading offers from {filePath}. Using default offers - {ex.ToString()}");
                return new List<Offer>();
            }
        }

        private Offer CreateOfferFromData(OfferData offerData)
        {
            return new Offer(
                offerData.OfferCode,
                offerData.MinDistance,
                offerData.MaxDistance,
                offerData.MinWeight,
                offerData.MaxWeight,
                offerData.DiscountPercent
            );
        }
    }
}
