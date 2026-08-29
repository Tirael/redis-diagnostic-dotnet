#pragma warning disable SER308 // These tests intentionally exercise the blocking Wait APIs.

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class ConnectionTests : RedisTestBase
{
    public ConnectionTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.CreateBatch))]
    public void Given_redis_When_CreateBatch_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var batch = session.Database.CreateBatch();

        batch.ShouldNotBeNull();
        session.AssertNotObserved(nameof(IDatabase.CreateBatch));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.CreateTransaction))]
    public void Given_redis_When_CreateTransaction_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var transaction = session.Database.CreateTransaction();

        transaction.ShouldNotBeNull();
        session.AssertNotObserved(nameof(IDatabase.CreateTransaction));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.IsConnected))]
    public void Given_redis_When_IsConnected_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var connected = session.Database.IsConnected(key);
        connected.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.IsConnected));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.IdentifyEndpoint))]
    public void Given_redis_When_IdentifyEndpoint_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var endpoint = session.Database.IdentifyEndpoint();
        endpoint.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.IdentifyEndpoint));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.IdentifyEndpointAsync))]
    public async Task Given_redis_When_IdentifyEndpointAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var endpoint = await session.Database.IdentifyEndpointAsync();
        endpoint.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.IdentifyEndpointAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Ping))]
    public void Given_redis_When_Ping_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var latency = session.Database.Ping();
        latency.ShouldBeGreaterThanOrEqualTo(TimeSpan.Zero);
        session.AssertObserved(nameof(IDatabase.Ping));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.PingAsync))]
    public async Task Given_redis_When_PingAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var latency = await session.Database.PingAsync();
        latency.ShouldBeGreaterThanOrEqualTo(TimeSpan.Zero);
        session.AssertObserved(nameof(IDatabase.PingAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.TryWait))]
    public void Given_redis_When_TryWait_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var completed = session.Database.TryWait(Task.CompletedTask);
        completed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.TryWait));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Wait))]
    public void Given_redis_When_Wait_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        session.Database.Wait(Task.CompletedTask);
        session.AssertObserved(nameof(IDatabase.Wait));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.WaitAll))]
    public void Given_redis_When_WaitAll_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        session.Database.WaitAll(Task.CompletedTask);
        session.AssertObserved(nameof(IDatabase.WaitAll));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Publish))]
    public void Given_redis_When_Publish_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var receivers = session.Database.Publish(RedisChannel.Literal(NewKey("ch")), "m");
        receivers.ShouldBeGreaterThanOrEqualTo(0);
        session.AssertObserved(nameof(IDatabase.Publish));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.PublishAsync))]
    public async Task Given_redis_When_PublishAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var receivers = await session.Database.PublishAsync(RedisChannel.Literal(NewKey("ch")), "m");
        receivers.ShouldBeGreaterThanOrEqualTo(0);
        session.AssertObserved(nameof(IDatabase.PublishAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Execute))]
    public void Given_redis_When_Execute_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = session.Database.Execute("PING");
        result.ToString().ShouldBe("PONG");
        session.AssertObserved(nameof(IDatabase.Execute));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ExecuteAsync))]
    public async Task Given_redis_When_ExecuteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = await session.Database.ExecuteAsync("PING");
        result.ToString().ShouldBe("PONG");
        session.AssertObserved(nameof(IDatabase.ExecuteAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.DebugObject))]
    public void Given_redis_When_DebugObject_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var debug = session.Database.DebugObject(key);
        debug.ToString().ShouldNotBeNullOrWhiteSpace();
        session.AssertObserved(nameof(IDatabase.DebugObject));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.DebugObjectAsync))]
    public async Task Given_redis_When_DebugObjectAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var debug = await session.Database.DebugObjectAsync(key);
        debug.ToString().ShouldNotBeNullOrWhiteSpace();
        session.AssertObserved(nameof(IDatabase.DebugObjectAsync));
    }
}
