namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class SortTests : RedisTestBase
{
    public SortTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Sort))]
    public void Given_redis_When_Sort_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["2", "1"]);
        var values = session.Database.Sort(key);
        ((string?)values[0]).ShouldBe("1");
        session.AssertObserved(nameof(IDatabase.Sort));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAsync))]
    public async Task Given_redis_When_SortAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["2", "1"]);
        var values = await session.Database.SortAsync(key);
        ((string?)values[0]).ShouldBe("1");
        session.AssertObserved(nameof(IDatabase.SortAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAndStore))]
    public void Given_redis_When_SortAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("so");
        session.Raw.ListRightPush(key, ["2", "1"]);
        var length = session.Database.SortAndStore(dest, key);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAndStoreAsync))]
    public async Task Given_redis_When_SortAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("so");
        session.Raw.ListRightPush(key, ["2", "1"]);
        var length = await session.Database.SortAndStoreAsync(dest, key);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortAndStoreAsync));
    }
}
