using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using StackExchange.Redis;

namespace RedisDiagnostic.Tests;

public sealed class RedisServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInstrumentedRedis_WithMultiplexer_RegistersInstrumentedDatabase()
    {
        var registry = Metrics.NewCustomRegistry();
        var multiplexer = DispatchProxy.Create<IConnectionMultiplexer, ConnectionMultiplexerStub>();
        var services = new ServiceCollection();

        services.AddInstrumentedRedis(multiplexer, registry);

        using var provider = services.BuildServiceProvider();
        var database = provider.GetRequiredService<IDatabase>();
        var metrics = provider.GetRequiredService<IRedisMethodMetrics>();

        Assert.Equal("InstrumentedDatabase", database.GetType().Name);
        Assert.IsType<PrometheusRedisMethodMetrics>(metrics);
        Assert.Same(multiplexer, provider.GetRequiredService<IConnectionMultiplexer>());
    }

    [Fact]
    public void AddInstrumentedRedis_WhenConnectionStringMissing_Throws()
    {
        var services = new ServiceCollection();

        Assert.Throws<ArgumentException>(() => services.AddInstrumentedRedis(" "));
    }

    public class ConnectionMultiplexerStub : DispatchProxy
    {
        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            ArgumentNullException.ThrowIfNull(targetMethod);

            if (targetMethod.Name == nameof(IConnectionMultiplexer.GetDatabase))
            {
                return DispatchProxy.Create<IDatabase, DatabaseStub>();
            }

            return GetDefault(targetMethod.ReturnType);
        }
    }

    public class DatabaseStub : DispatchProxy
    {
        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            ArgumentNullException.ThrowIfNull(targetMethod);
            return GetDefault(targetMethod.ReturnType);
        }
    }

    private static object? GetDefault(Type type)
    {
        if (type == typeof(void))
        {
            return null;
        }

        if (type == typeof(Task))
        {
            return Task.CompletedTask;
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
        {
            var resultType = type.GetGenericArguments()[0];
            var result = resultType.IsValueType ? Activator.CreateInstance(resultType) : null;
            return typeof(Task)
                .GetMethod(nameof(Task.FromResult))!
                .MakeGenericMethod(resultType)
                .Invoke(null, [result]);
        }

        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}
