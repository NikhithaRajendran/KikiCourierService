using KikiCourierService.OderOrchestrator;
using KikiCourierService.Startup;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Welcome to Kiki Courier Service");
var services = new ServiceCollection();
services.InjectDependencies();
var serviceProvider = services.BuildServiceProvider();
try
{
    if (args.Length == 0)
    {
        Console.WriteLine("Please provide a valid input file");
        return;
    }

    var orchestrationService = serviceProvider.GetRequiredService<IOrderOrchestrator>();
    var response = await orchestrationService.ProcessPackageDeliveryAsync(args);
    if (response != null)
        Console.WriteLine("Kiki Courier Service Delivery Estimate Calculation Completed");
    else
    {
        Console.WriteLine("Delivery Estimation Failed");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"{ex.Message}");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Error occurred while processing the orders - {ex.ToString()}");
    return;
}