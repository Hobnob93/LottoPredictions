using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LottoPredictions.Core.Interfaces;
using LottoPredictions.Core.Services;

IHost? host = null;

try
{
    Console.WriteLine("Initialising host...");
    host = Host.CreateDefaultBuilder(args)
    .UseConsoleLifetime()
    .ConfigureServices((_, services) =>
        services.AddSingleton<IService, Service>())
    .Build();

    Console.WriteLine("Starting host...");
    await host.StartAsync();

    Console.WriteLine("Retrieving core service...");
    var service = host.Services.GetService<IService>()
        ?? throw new InvalidOperationException("IService could not be retrieved.");

    Console.WriteLine("Ready!\n\n");
    service.Run();

    Console.WriteLine("\n\nPress Ctrl+C to end application.");
    await host.WaitForShutdownAsync();

    Console.WriteLine("Terminated.");
}
finally
{
    if (host is IAsyncDisposable d)
        await d.DisposeAsync();
}
