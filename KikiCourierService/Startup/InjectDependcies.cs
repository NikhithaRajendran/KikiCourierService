using KikiCourierService.Models;
using KikiCourierService.OderOrchestrator;
using KikiCourierService.Offers;
using KikiCourierService.Services.CostCalculator;
using KikiCourierService.Services.DeliveryScheduler;
using KikiCourierService.Services.InputParsers;
using KikiCourierService.Services.VehicleManager;
using Microsoft.Extensions.DependencyInjection;

namespace KikiCourierService.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IOrderOrchestrator, TomboOrderOrchestrator>();
            services.AddSingleton<Offer, OFR001>();
            services.AddSingleton<Offer, OFR002>();
            services.AddSingleton<Offer, OFR003>();
            services.AddSingleton<IInputParser, FileInputParser>();
            services.AddSingleton<IDiscountCalculator, DiscountCalculator>();
            services.AddSingleton<ICostCalculator, CostCalculator>();
            services.AddSingleton<IDeliveryScheduler, DeliveryScheduler>();
            services.AddSingleton<IVehicleManager, VehicleManager>();

            return services;
        }
    }

}
