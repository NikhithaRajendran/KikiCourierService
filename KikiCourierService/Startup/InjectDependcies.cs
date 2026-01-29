using KikiCourierService.Models;
using KikiCourierService.OderOrchestrator;
using KikiCourierService.Services.CostCalculator;
using KikiCourierService.Services.DeliveryScheduler;
using KikiCourierService.Services.InputParsers;
using KikiCourierService.Services.VehicleManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KikiCourierService.Startup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IOrderOrchestrator, TomboOrderOrchestrator>();
            services.AddSingleton<IOfferCalculator, OfferCalculator>();
            services.AddSingleton<IInputParser, FileInputParser>();
            services.AddSingleton<IDiscountCalculator, DiscountCalculator>();
            services.AddSingleton<ICostCalculator, CostCalculator>();
            services.AddSingleton<IDeliveryScheduler, DeliveryScheduler>();
            services.AddSingleton<IVehicleManager, VehicleManager>();

            return services;
        }
    }

}
