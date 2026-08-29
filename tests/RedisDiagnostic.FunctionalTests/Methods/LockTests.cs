namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class LockTests : RedisTestBase
{
    public LockTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockTake))]
    public void Given_redis_When_LockTake_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var taken = session.Database.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        taken.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockTake));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockTakeAsync))]
    public async Task Given_redis_When_LockTakeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var taken = await session.Database.LockTakeAsync(key, "tok", TimeSpan.FromSeconds(10));
        taken.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockTakeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockQuery))]
    public void Given_redis_When_LockQuery_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var token = session.Database.LockQuery(key);
        ((string?)token).ShouldBe("tok");
        session.AssertObserved(nameof(IDatabase.LockQuery));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockQueryAsync))]
    public async Task Given_redis_When_LockQueryAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var token = await session.Database.LockQueryAsync(key);
        ((string?)token).ShouldBe("tok");
        session.AssertObserved(nameof(IDatabase.LockQueryAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockExtend))]
    public void Given_redis_When_LockExtend_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var extended = session.Database.LockExtend(key, "tok", TimeSpan.FromSeconds(20));
        extended.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockExtend));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockExtendAsync))]
    public async Task Given_redis_When_LockExtendAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var extended = await session.Database.LockExtendAsync(key, "tok", TimeSpan.FromSeconds(20));
        extended.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockExtendAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockRelease))]
    public void Given_redis_When_LockRelease_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var released = session.Database.LockRelease(key, "tok");
        released.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockRelease));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockReleaseAsync))]
    public async Task Given_redis_When_LockReleaseAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var released = await session.Database.LockReleaseAsync(key, "tok");
        released.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.LockReleaseAsync));
    }
}
