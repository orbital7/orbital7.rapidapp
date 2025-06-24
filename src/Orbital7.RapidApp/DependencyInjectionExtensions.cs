namespace Orbital7.RapidApp;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRapidApp(
        this IServiceCollection services)
    {
        services.AddScoped<TimeConverter, TimeConverter>();
        services.AddScoped<IMessenger, WeakReferenceMessenger>();
        services.AddBlazoredModal();

        return services;
    }
}
