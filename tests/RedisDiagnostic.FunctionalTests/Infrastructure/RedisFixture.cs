using Prometheus;
using StackExchange.Redis;
using Testcontainers.Redis;

namespace RedisDiagnostic.FunctionalTests.Infrastructure;

public sealed class RedisFixture : IAsyncLifetime
{
    private readonly RedisContainer _container = new RedisBuilder("redis:7.4-alpine")
        .WithCommand("redis-server", "--enable-debug-command", "yes")
        .Build();

    private IConnectionMultiplexer _multiplexer = null!;

    public IConnectionMultiplexer Multiplexer => _multiplexer;

    public IDatabase Raw => _multiplexer.GetDatabase();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        var configuration = ConfigurationOptions.Parse(_container.GetConnectionString());
        configuration.AllowAdmin = true;
        _multiplexer = await ConnectionMultiplexer.ConnectAsync(configuration);
    }

    public async Task DisposeAsync()
    {
        if (_multiplexer is not null)
        {
            await _multiplexer.CloseAsync();
            _multiplexer.Dispose();
        }

        await _container.DisposeAsync();
    }

    public string NewKey(string suffix = "k") => $"t:{Guid.NewGuid():N}:{suffix}";

    public RedisSession CreateSession()
    {
        var registry = Metrics.NewCustomRegistry();
        var metrics = new PrometheusRedisMethodMetrics(registry, TimeProvider.System);
        var database = new InstrumentedDatabase(_multiplexer.GetDatabase(), metrics);
        return new RedisSession(database, Raw, registry);
    }
}
