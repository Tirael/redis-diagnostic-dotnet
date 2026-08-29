namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class GeoTests : RedisTestBase
{
    public GeoTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoAdd))]
    public void Given_redis_When_GeoAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = session.Database.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.GeoAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoAddAsync))]
    public async Task Given_redis_When_GeoAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = await session.Database.GeoAddAsync(key, 13.361389, 38.115556, "Palermo");
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.GeoAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoRemove))]
    public void Given_redis_When_GeoRemove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var removed = session.Database.GeoRemove(key, "Palermo");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.GeoRemove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoRemoveAsync))]
    public async Task Given_redis_When_GeoRemoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var removed = await session.Database.GeoRemoveAsync(key, "Palermo");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.GeoRemoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoDistance))]
    public void Given_redis_When_GeoDistance_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        session.Raw.GeoAdd(key, 15.087269, 37.502669, "Catania");
        var distance = session.Database.GeoDistance(key, "Palermo", "Catania");
        distance.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.GeoDistance));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoDistanceAsync))]
    public async Task Given_redis_When_GeoDistanceAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        session.Raw.GeoAdd(key, 15.087269, 37.502669, "Catania");
        var distance = await session.Database.GeoDistanceAsync(key, "Palermo", "Catania");
        distance.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.GeoDistanceAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoHash))]
    public void Given_redis_When_GeoHash_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var hash = session.Database.GeoHash(key, "Palermo");
        hash.ShouldNotBeNullOrWhiteSpace();
        session.AssertObserved(nameof(IDatabase.GeoHash));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoHashAsync))]
    public async Task Given_redis_When_GeoHashAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var hash = await session.Database.GeoHashAsync(key, "Palermo");
        hash.ShouldNotBeNullOrWhiteSpace();
        session.AssertObserved(nameof(IDatabase.GeoHashAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoPosition))]
    public void Given_redis_When_GeoPosition_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var position = session.Database.GeoPosition(key, "Palermo");
        position.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.GeoPosition));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoPositionAsync))]
    public async Task Given_redis_When_GeoPositionAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var position = await session.Database.GeoPositionAsync(key, "Palermo");
        position.ShouldNotBeNull();
        session.AssertObserved(nameof(IDatabase.GeoPositionAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoRadius))]
    public void Given_redis_When_GeoRadius_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var results = session.Database.GeoRadius(key, 13.361389, 38.115556, 100, GeoUnit.Kilometers);
        results.Should().NotBeEmpty();
        session.AssertObserved(nameof(IDatabase.GeoRadius));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoRadiusAsync))]
    public async Task Given_redis_When_GeoRadiusAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var results = await session.Database.GeoRadiusAsync(key, 13.361389, 38.115556, 100, GeoUnit.Kilometers);
        results.Should().NotBeEmpty();
        session.AssertObserved(nameof(IDatabase.GeoRadiusAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoSearch))]
    public void Given_redis_When_GeoSearch_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var results = session.Database.GeoSearch(key, 13.361389, 38.115556, new GeoSearchCircle(100, GeoUnit.Kilometers));
        results.Should().NotBeEmpty();
        session.AssertObserved(nameof(IDatabase.GeoSearch));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoSearchAsync))]
    public async Task Given_redis_When_GeoSearchAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var results = await session.Database.GeoSearchAsync(key, 13.361389, 38.115556, new GeoSearchCircle(100, GeoUnit.Kilometers));
        results.Should().NotBeEmpty();
        session.AssertObserved(nameof(IDatabase.GeoSearchAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoSearchAndStore))]
    public void Given_redis_When_GeoSearchAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("gd");
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var stored = session.Database.GeoSearchAndStore(key, dest, 13.361389, 38.115556, new GeoSearchCircle(100, GeoUnit.Kilometers));
        stored.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.GeoSearchAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.GeoSearchAndStoreAsync))]
    public async Task Given_redis_When_GeoSearchAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("gd");
        session.Raw.GeoAdd(key, 13.361389, 38.115556, "Palermo");
        var stored = await session.Database.GeoSearchAndStoreAsync(key, dest, 13.361389, 38.115556, new GeoSearchCircle(100, GeoUnit.Kilometers));
        stored.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.GeoSearchAndStoreAsync));
    }
}
