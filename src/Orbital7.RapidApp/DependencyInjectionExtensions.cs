namespace Orbital7.RapidApp;

public static class DependencyInjectionExtensions
{
    // TODO: Move to Orbital7.Extensions.
    public static IServiceCollection AddDisplayValueOptions(
        this IServiceCollection services,
        Action<DisplayValueOptionsBuilder>? configureBuilder = null)
    {
        services.AddTransient(provider =>
        {
            var builder = new DisplayValueOptionsBuilder();
            configureBuilder?.Invoke(builder);
            return builder;
        });

        services.AddTransient(provider =>
        {
            var builder = provider.GetRequiredService<DisplayValueOptionsBuilder>();
            return builder.Build();
        });

        return services;
    }

    public static IServiceCollection AddRapidApp(
        this IServiceCollection services,
        Action<DisplayValueOptionsBuilder>? configureDisplayValueOptionsBuilder = null)
    {
        services.AddDisplayValueOptions(configureDisplayValueOptionsBuilder);
        services.AddScoped<TimeConverter, TimeConverter>();
        services.AddScoped<IMessenger, WeakReferenceMessenger>();
        services.AddBlazoredModal();

        return services;
    }
}
