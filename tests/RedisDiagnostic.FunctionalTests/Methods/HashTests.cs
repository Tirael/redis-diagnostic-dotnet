using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class HashTests : RedisTestBase
{
    public HashTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashSet))]
    public void Given_redis_When_HashSet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var created = session.Database.HashSet(key, "f", "v");
        Assert.True(created);
        Assert.Equal("v", (string?)session.Raw.HashGet(key, "f"));
        session.AssertObserved(nameof(IDatabase.HashSet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashSetAsync))]
    public async Task Given_redis_When_HashSetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var created = await session.Database.HashSetAsync(key, "f", "v");
        Assert.True(created);
        Assert.Equal("v", (string?)session.Raw.HashGet(key, "f"));
        session.AssertObserved(nameof(IDatabase.HashSetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGet))]
    public void Given_redis_When_HashGet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var value = session.Database.HashGet(key, "f");
        Assert.Equal("v", (string?)value);
        session.AssertObserved(nameof(IDatabase.HashGet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGetAsync))]
    public async Task Given_redis_When_HashGetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var value = await session.Database.HashGetAsync(key, "f");
        Assert.Equal("v", (string?)value);
        session.AssertObserved(nameof(IDatabase.HashGetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGetAll))]
    public void Given_redis_When_HashGetAll_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = session.Database.HashGetAll(key);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashGetAll));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGetAllAsync))]
    public async Task Given_redis_When_HashGetAllAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = await session.Database.HashGetAllAsync(key);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashGetAllAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashDelete))]
    public void Given_redis_When_HashDelete_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var deleted = session.Database.HashDelete(key, "f");
        Assert.True(deleted);
        Assert.False(session.Raw.HashExists(key, "f"));
        session.AssertObserved(nameof(IDatabase.HashDelete));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashDeleteAsync))]
    public async Task Given_redis_When_HashDeleteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var deleted = await session.Database.HashDeleteAsync(key, "f");
        Assert.True(deleted);
        Assert.False(session.Raw.HashExists(key, "f"));
        session.AssertObserved(nameof(IDatabase.HashDeleteAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashExists))]
    public void Given_redis_When_HashExists_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var exists = session.Database.HashExists(key, "f");
        Assert.True(exists);
        session.AssertObserved(nameof(IDatabase.HashExists));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashExistsAsync))]
    public async Task Given_redis_When_HashExistsAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var exists = await session.Database.HashExistsAsync(key, "f");
        Assert.True(exists);
        session.AssertObserved(nameof(IDatabase.HashExistsAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashIncrement))]
    public void Given_redis_When_HashIncrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var value = session.Database.HashIncrement(key, "n");
        Assert.Equal(1, value);
        session.AssertObserved(nameof(IDatabase.HashIncrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashIncrementAsync))]
    public async Task Given_redis_When_HashIncrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var value = await session.Database.HashIncrementAsync(key, "n");
        Assert.Equal(1, value);
        session.AssertObserved(nameof(IDatabase.HashIncrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashDecrement))]
    public void Given_redis_When_HashDecrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "n", "5");
        var value = session.Database.HashDecrement(key, "n");
        Assert.Equal(4, value);
        session.AssertObserved(nameof(IDatabase.HashDecrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashDecrementAsync))]
    public async Task Given_redis_When_HashDecrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "n", "5");
        var value = await session.Database.HashDecrementAsync(key, "n");
        Assert.Equal(4, value);
        session.AssertObserved(nameof(IDatabase.HashDecrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashKeys))]
    public void Given_redis_When_HashKeys_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var keys = session.Database.HashKeys(key);
        Assert.Equal("f", (string?)keys[0]);
        session.AssertObserved(nameof(IDatabase.HashKeys));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashKeysAsync))]
    public async Task Given_redis_When_HashKeysAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var keys = await session.Database.HashKeysAsync(key);
        Assert.Equal("f", (string?)keys[0]);
        session.AssertObserved(nameof(IDatabase.HashKeysAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashValues))]
    public void Given_redis_When_HashValues_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var values = session.Database.HashValues(key);
        Assert.Equal("v", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.HashValues));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashValuesAsync))]
    public async Task Given_redis_When_HashValuesAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var values = await session.Database.HashValuesAsync(key);
        Assert.Equal("v", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.HashValuesAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashLength))]
    public void Given_redis_When_HashLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var length = session.Database.HashLength(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.HashLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashLengthAsync))]
    public async Task Given_redis_When_HashLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var length = await session.Database.HashLengthAsync(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.HashLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashStringLength))]
    public void Given_redis_When_HashStringLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "ab");
        var length = session.Database.HashStringLength(key, "f");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.HashStringLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashStringLengthAsync))]
    public async Task Given_redis_When_HashStringLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "ab");
        var length = await session.Database.HashStringLengthAsync(key, "f");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.HashStringLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomField))]
    public void Given_redis_When_HashRandomField_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var field = session.Database.HashRandomField(key);
        Assert.Equal("f", (string?)field);
        session.AssertObserved(nameof(IDatabase.HashRandomField));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomFieldAsync))]
    public async Task Given_redis_When_HashRandomFieldAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var field = await session.Database.HashRandomFieldAsync(key);
        Assert.Equal("f", (string?)field);
        session.AssertObserved(nameof(IDatabase.HashRandomFieldAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomFields))]
    public void Given_redis_When_HashRandomFields_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var fields = session.Database.HashRandomFields(key, 1);
        Assert.Single(fields);
        session.AssertObserved(nameof(IDatabase.HashRandomFields));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomFieldsAsync))]
    public async Task Given_redis_When_HashRandomFieldsAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var fields = await session.Database.HashRandomFieldsAsync(key, 1);
        Assert.Single(fields);
        session.AssertObserved(nameof(IDatabase.HashRandomFieldsAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomFieldsWithValues))]
    public void Given_redis_When_HashRandomFieldsWithValues_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = session.Database.HashRandomFieldsWithValues(key, 1);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashRandomFieldsWithValues));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashRandomFieldsWithValuesAsync))]
    public async Task Given_redis_When_HashRandomFieldsWithValuesAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = await session.Database.HashRandomFieldsWithValuesAsync(key, 1);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashRandomFieldsWithValuesAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashScan))]
    public void Given_redis_When_HashScan_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = session.Database.HashScan(key).ToArray();
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashScan));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashScanAsync))]
    public async Task Given_redis_When_HashScanAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var entries = new List<HashEntry>();
        await foreach (var entry in session.Database.HashScanAsync(key))
        {
            entries.Add(entry);
        }
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.HashScanAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashScanNoValues))]
    public void Given_redis_When_HashScanNoValues_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var fields = session.Database.HashScanNoValues(key).ToArray();
        Assert.Equal("f", (string?)fields[0]);
        session.AssertObserved(nameof(IDatabase.HashScanNoValues));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashScanNoValuesAsync))]
    public async Task Given_redis_When_HashScanNoValuesAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var fields = new List<RedisValue>();
        await foreach (var field in session.Database.HashScanNoValuesAsync(key))
        {
            fields.Add(field);
        }
        Assert.Equal("f", (string?)fields[0]);
        session.AssertObserved(nameof(IDatabase.HashScanNoValuesAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGetLease))]
    public void Given_redis_When_HashGetLease_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        using var lease = session.Database.HashGetLease(key, "f");
        Assert.NotNull(lease);
        session.AssertObserved(nameof(IDatabase.HashGetLease));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashGetLeaseAsync))]
    public async Task Given_redis_When_HashGetLeaseAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        using var lease = await session.Database.HashGetLeaseAsync(key, "f");
        Assert.NotNull(lease);
        session.AssertObserved(nameof(IDatabase.HashGetLeaseAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldExpire))]
    public void Given_redis_When_HashFieldExpire_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var results = session.Database.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        Assert.Equal(ExpireResult.Success, results[0]);
        session.AssertObserved(nameof(IDatabase.HashFieldExpire));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldExpireAsync))]
    public async Task Given_redis_When_HashFieldExpireAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        var results = await session.Database.HashFieldExpireAsync(key, ["f"], TimeSpan.FromMinutes(1));
        Assert.Equal(ExpireResult.Success, results[0]);
        session.AssertObserved(nameof(IDatabase.HashFieldExpireAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldGetTimeToLive))]
    public void Given_redis_When_HashFieldGetTimeToLive_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var ttl = session.Database.HashFieldGetTimeToLive(key, ["f"]);
        Assert.True(ttl[0] > 0);
        session.AssertObserved(nameof(IDatabase.HashFieldGetTimeToLive));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldGetTimeToLiveAsync))]
    public async Task Given_redis_When_HashFieldGetTimeToLiveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var ttl = await session.Database.HashFieldGetTimeToLiveAsync(key, ["f"]);
        Assert.True(ttl[0] > 0);
        session.AssertObserved(nameof(IDatabase.HashFieldGetTimeToLiveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldGetExpireDateTime))]
    public void Given_redis_When_HashFieldGetExpireDateTime_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var when = session.Database.HashFieldGetExpireDateTime(key, ["f"]);
        Assert.True(when[0] > 0);
        session.AssertObserved(nameof(IDatabase.HashFieldGetExpireDateTime));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldGetExpireDateTimeAsync))]
    public async Task Given_redis_When_HashFieldGetExpireDateTimeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var when = await session.Database.HashFieldGetExpireDateTimeAsync(key, ["f"]);
        Assert.True(when[0] > 0);
        session.AssertObserved(nameof(IDatabase.HashFieldGetExpireDateTimeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldPersist))]
    public void Given_redis_When_HashFieldPersist_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var results = session.Database.HashFieldPersist(key, ["f"]);
        Assert.Equal(PersistResult.Success, results[0]);
        session.AssertObserved(nameof(IDatabase.HashFieldPersist));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HashFieldPersistAsync))]
    public async Task Given_redis_When_HashFieldPersistAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HashSet(key, "f", "v");
        session.Raw.HashFieldExpire(key, ["f"], TimeSpan.FromMinutes(1));
        var results = await session.Database.HashFieldPersistAsync(key, ["f"]);
        Assert.Equal(PersistResult.Success, results[0]);
        session.AssertObserved(nameof(IDatabase.HashFieldPersistAsync));
    }
}
