using KikiCourierService.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace KikiCourierService.Services.CostCalculator
{
    public class OfferCalculator : IOfferCalculator
    {
        private readonly IConfiguration _configuration;
        private readonly string DefaultOffersFilePath = "offers.json";
        public OfferCalculator(IConfiguration configuration) { 
            _configuration = configuration;
            DefaultOffersFilePath = _configuration["Constants:DefaultOffersFilePath"] ??string.Empty;
        }
       

        public async Task<List<Offer>?> LoadOffersAsync()
        {
            return await LoadOffersAsync(DefaultOffersFilePath);
        }

        public async Task<List<Offer>?> LoadOffersAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Offers file not found at {filePath}.");
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var offerConfiguration = JsonSerializer.Deserialize<OfferConfiguration>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var offers = offerConfiguration?.Offers.Where(o => o.IsActive).Select(CreateOfferFromData).ToList();

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
