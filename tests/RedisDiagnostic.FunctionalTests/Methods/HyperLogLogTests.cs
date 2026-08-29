namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class HyperLogLogTests : RedisTestBase
{
    public HyperLogLogTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogAdd))]
    public void Given_redis_When_HyperLogLogAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = session.Database.HyperLogLogAdd(key, "a");
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.HyperLogLogAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogAddAsync))]
    public async Task Given_redis_When_HyperLogLogAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = await session.Database.HyperLogLogAddAsync(key, "a");
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.HyperLogLogAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogLength))]
    public void Given_redis_When_HyperLogLogLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.HyperLogLogAdd(key, "a");
        var length = session.Database.HyperLogLogLength(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.HyperLogLogLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogLengthAsync))]
    public async Task Given_redis_When_HyperLogLogLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.HyperLogLogAdd(key, "a");
        var length = await session.Database.HyperLogLogLengthAsync(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.HyperLogLogLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogMerge))]
    public void Given_redis_When_HyperLogLogMerge_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var first = NewKey("h1");
        var second = NewKey("h2");
        session.Raw.HyperLogLogAdd(first, "a");
        session.Raw.HyperLogLogAdd(second, "b");
        session.Database.HyperLogLogMerge(key, first, second);
        session.Raw.HyperLogLogLength(key).ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.HyperLogLogMerge));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogMergeAsync))]
    public async Task Given_redis_When_HyperLogLogMergeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var first = NewKey("h1");
        var second = NewKey("h2");
        session.Raw.HyperLogLogAdd(first, "a");
        session.Raw.HyperLogLogAdd(second, "b");
        await session.Database.HyperLogLogMergeAsync(key, first, second);
        session.Raw.HyperLogLogLength(key).ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.HyperLogLogMergeAsync));
    }
}
