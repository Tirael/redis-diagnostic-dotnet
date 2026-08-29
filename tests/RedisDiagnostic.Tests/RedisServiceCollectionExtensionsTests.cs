namespace RedisDiagnostic.Tests;

public sealed class RedisServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInstrumentedRedis_WithMultiplexer_RegistersInstrumentedDatabase()
    {
        var registry = Metrics.NewCustomRegistry();
        var multiplexer = DispatchProxy.Create<IConnectionMultiplexer, ConnectionMultiplexerStub>();
        ServiceCollection services = new();

        services.AddInstrumentedRedis(multiplexer, registry);

        using var provider = services.BuildServiceProvider();
        var database = provider.GetRequiredService<IDatabase>();
        var metrics = provider.GetRequiredService<IRedisMethodMetrics>();

        database.GetType().Name.ShouldBe("InstrumentedDatabase");
        metrics.Should().BeOfType<PrometheusRedisMethodMetrics>();
        provider.GetRequiredService<IConnectionMultiplexer>().Should().BeSameAs(multiplexer);
    }

    [Fact]
    public void AddInstrumentedRedis_WhenConnectionStringMissing_Throws()
    {
        ServiceCollection services = new();

        var register = () => services.AddInstrumentedRedis(" ");
        register.Should().Throw<ArgumentException>();
    }
}
