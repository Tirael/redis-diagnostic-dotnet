using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using StackExchange.Redis;

namespace RedisDiagnostic;

/// <summary>
/// Registers an instrumented <see cref="IDatabase"/> in the service collection.
/// </summary>
public static class RedisServiceCollectionExtensions
{
    /// <summary>
    /// Connects to Redis and registers <see cref="IDatabase"/> as generated <c>InstrumentedDatabase</c>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">StackExchange.Redis connection string.</param>
    /// <param name="registry">Optional registry; defaults to <see cref="Metrics.DefaultRegistry"/>.</param>
    /// <param name="timeProvider">Optional clock; defaults to <see cref="TimeProvider.System"/>.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddInstrumentedRedis(
        this IServiceCollection services,
        string connectionString,
        CollectorRegistry? registry = null,
        TimeProvider? timeProvider = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(connectionString));
        return AddInstrumentedRedisCore(services, registry, timeProvider);
    }

    /// <summary>
    /// Registers an instrumented <see cref="IDatabase"/> around an existing multiplexer.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="multiplexer">An already-connected multiplexer.</param>
    /// <param name="registry">Optional registry; defaults to <see cref="Metrics.DefaultRegistry"/>.</param>
    /// <param name="timeProvider">Optional clock; defaults to <see cref="TimeProvider.System"/>.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddInstrumentedRedis(
        this IServiceCollection services,
        IConnectionMultiplexer multiplexer,
        CollectorRegistry? registry = null,
        TimeProvider? timeProvider = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(multiplexer);

        services.AddSingleton(multiplexer);
        return AddInstrumentedRedisCore(services, registry, timeProvider);
    }

    private static IServiceCollection AddInstrumentedRedisCore(
        IServiceCollection services,
        CollectorRegistry? registry,
        TimeProvider? timeProvider)
    {
        var collectorRegistry = registry ?? Metrics.DefaultRegistry;
        var clock = timeProvider ?? TimeProvider.System;

        services.AddSingleton<IRedisMethodMetrics>(_ => new PrometheusRedisMethodMetrics(collectorRegistry, clock));
        services.AddSingleton<IDatabase>(sp =>
        {
            var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
            var metrics = sp.GetRequiredService<IRedisMethodMetrics>();
            return new InstrumentedDatabase(multiplexer.GetDatabase(), metrics);
        });

        return services;
    }
}
