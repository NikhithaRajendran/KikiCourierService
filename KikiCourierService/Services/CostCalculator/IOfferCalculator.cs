using KikiCourierService.Models;
using System.Text.Json;

namespace KikiCourierService.Services.CostCalculator
{
    public interface IOfferCalculator
    {
        Task<List<Offer>> LoadOffersAsync();
        Task<List<Offer>> LoadOffersAsync(string filePath);
    }
   
}
