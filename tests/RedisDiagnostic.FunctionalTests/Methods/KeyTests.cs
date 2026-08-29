using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class KeyTests : RedisTestBase
{
    public KeyTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExists))]
    public void Given_redis_When_KeyExists_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var exists = session.Database.KeyExists(key);
        Assert.True(exists);
        session.AssertObserved(nameof(IDatabase.KeyExists));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExistsAsync))]
    public async Task Given_redis_When_KeyExistsAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var exists = await session.Database.KeyExistsAsync(key);
        Assert.True(exists);
        session.AssertObserved(nameof(IDatabase.KeyExistsAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyDelete))]
    public void Given_redis_When_KeyDelete_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var deleted = session.Database.KeyDelete(key);
        Assert.True(deleted);
        Assert.False(session.Raw.KeyExists(key));
        session.AssertObserved(nameof(IDatabase.KeyDelete));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyDeleteAsync))]
    public async Task Given_redis_When_KeyDeleteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var deleted = await session.Database.KeyDeleteAsync(key);
        Assert.True(deleted);
        Assert.False(session.Raw.KeyExists(key));
        session.AssertObserved(nameof(IDatabase.KeyDeleteAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExpire))]
    public void Given_redis_When_KeyExpire_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var set = session.Database.KeyExpire(key, TimeSpan.FromMinutes(1));
        Assert.True(set);
        Assert.NotNull(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.KeyExpire));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExpireAsync))]
    public async Task Given_redis_When_KeyExpireAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var set = await session.Database.KeyExpireAsync(key, TimeSpan.FromMinutes(1));
        Assert.True(set);
        Assert.NotNull(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.KeyExpireAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExpireTime))]
    public void Given_redis_When_KeyExpireTime_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var when = session.Database.KeyExpireTime(key);
        Assert.NotNull(when);
        session.AssertObserved(nameof(IDatabase.KeyExpireTime));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyExpireTimeAsync))]
    public async Task Given_redis_When_KeyExpireTimeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var when = await session.Database.KeyExpireTimeAsync(key);
        Assert.NotNull(when);
        session.AssertObserved(nameof(IDatabase.KeyExpireTimeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyTimeToLive))]
    public void Given_redis_When_KeyTimeToLive_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var ttl = session.Database.KeyTimeToLive(key);
        Assert.NotNull(ttl);
        session.AssertObserved(nameof(IDatabase.KeyTimeToLive));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyTimeToLiveAsync))]
    public async Task Given_redis_When_KeyTimeToLiveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var ttl = await session.Database.KeyTimeToLiveAsync(key);
        Assert.NotNull(ttl);
        session.AssertObserved(nameof(IDatabase.KeyTimeToLiveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyPersist))]
    public void Given_redis_When_KeyPersist_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var persisted = session.Database.KeyPersist(key);
        Assert.True(persisted);
        Assert.Null(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.KeyPersist));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyPersistAsync))]
    public async Task Given_redis_When_KeyPersistAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var persisted = await session.Database.KeyPersistAsync(key);
        Assert.True(persisted);
        Assert.Null(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.KeyPersistAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRename))]
    public void Given_redis_When_KeyRename_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dest = Fixture.NewKey("ren");
        var renamed = session.Database.KeyRename(key, dest);
        Assert.True(renamed);
        Assert.Equal("v", (string?)session.Raw.StringGet(dest));
        session.AssertObserved(nameof(IDatabase.KeyRename));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRenameAsync))]
    public async Task Given_redis_When_KeyRenameAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dest = Fixture.NewKey("ren");
        var renamed = await session.Database.KeyRenameAsync(key, dest);
        Assert.True(renamed);
        Assert.Equal("v", (string?)session.Raw.StringGet(dest));
        session.AssertObserved(nameof(IDatabase.KeyRenameAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyCopy))]
    public void Given_redis_When_KeyCopy_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dest = Fixture.NewKey("cp");
        var copied = session.Database.KeyCopy(key, dest);
        Assert.True(copied);
        Assert.Equal("v", (string?)session.Raw.StringGet(dest));
        session.AssertObserved(nameof(IDatabase.KeyCopy));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyCopyAsync))]
    public async Task Given_redis_When_KeyCopyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dest = Fixture.NewKey("cp");
        var copied = await session.Database.KeyCopyAsync(key, dest);
        Assert.True(copied);
        Assert.Equal("v", (string?)session.Raw.StringGet(dest));
        session.AssertObserved(nameof(IDatabase.KeyCopyAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyType))]
    public void Given_redis_When_KeyType_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var type = session.Database.KeyType(key);
        Assert.Equal(RedisType.String, type);
        session.AssertObserved(nameof(IDatabase.KeyType));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyTypeAsync))]
    public async Task Given_redis_When_KeyTypeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var type = await session.Database.KeyTypeAsync(key);
        Assert.Equal(RedisType.String, type);
        session.AssertObserved(nameof(IDatabase.KeyTypeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyTouch))]
    public void Given_redis_When_KeyTouch_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var touched = session.Database.KeyTouch(key);
        Assert.True(touched);
        session.AssertObserved(nameof(IDatabase.KeyTouch));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyTouchAsync))]
    public async Task Given_redis_When_KeyTouchAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var touched = await session.Database.KeyTouchAsync(key);
        Assert.True(touched);
        session.AssertObserved(nameof(IDatabase.KeyTouchAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyDump))]
    public void Given_redis_When_KeyDump_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dump = session.Database.KeyDump(key);
        Assert.NotNull(dump);
        session.AssertObserved(nameof(IDatabase.KeyDump));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyDumpAsync))]
    public async Task Given_redis_When_KeyDumpAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dump = await session.Database.KeyDumpAsync(key);
        Assert.NotNull(dump);
        session.AssertObserved(nameof(IDatabase.KeyDumpAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRestore))]
    public void Given_redis_When_KeyRestore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dump = session.Raw.KeyDump(key)!;
        session.Raw.KeyDelete(key);
        session.Database.KeyRestore(key, dump);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.KeyRestore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRestoreAsync))]
    public async Task Given_redis_When_KeyRestoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var dump = session.Raw.KeyDump(key)!;
        session.Raw.KeyDelete(key);
        await session.Database.KeyRestoreAsync(key, dump);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.KeyRestoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyEncoding))]
    public void Given_redis_When_KeyEncoding_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var encoding = session.Database.KeyEncoding(key);
        Assert.False(string.IsNullOrWhiteSpace(encoding));
        session.AssertObserved(nameof(IDatabase.KeyEncoding));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyEncodingAsync))]
    public async Task Given_redis_When_KeyEncodingAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var encoding = await session.Database.KeyEncodingAsync(key);
        Assert.False(string.IsNullOrWhiteSpace(encoding));
        session.AssertObserved(nameof(IDatabase.KeyEncodingAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRefCount))]
    public void Given_redis_When_KeyRefCount_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var count = session.Database.KeyRefCount(key);
        Assert.True(count >= 1);
        session.AssertObserved(nameof(IDatabase.KeyRefCount));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRefCountAsync))]
    public async Task Given_redis_When_KeyRefCountAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var count = await session.Database.KeyRefCountAsync(key);
        Assert.True(count >= 1);
        session.AssertObserved(nameof(IDatabase.KeyRefCountAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyIdleTime))]
    public void Given_redis_When_KeyIdleTime_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var idle = session.Database.KeyIdleTime(key);
        Assert.NotNull(idle);
        session.AssertObserved(nameof(IDatabase.KeyIdleTime));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyIdleTimeAsync))]
    public async Task Given_redis_When_KeyIdleTimeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var idle = await session.Database.KeyIdleTimeAsync(key);
        Assert.NotNull(idle);
        session.AssertObserved(nameof(IDatabase.KeyIdleTimeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRandom))]
    public void Given_redis_When_KeyRandom_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var random = session.Database.KeyRandom();
        Assert.False(string.IsNullOrEmpty((string?)random));
        session.AssertObserved(nameof(IDatabase.KeyRandom));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyRandomAsync))]
    public async Task Given_redis_When_KeyRandomAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var random = await session.Database.KeyRandomAsync();
        Assert.False(string.IsNullOrEmpty((string?)random));
        session.AssertObserved(nameof(IDatabase.KeyRandomAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyMove))]
    public void Given_redis_When_KeyMove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var moved = session.Database.KeyMove(key, 1);
        Assert.True(moved);
        Assert.False(session.Raw.KeyExists(key));
        Assert.Equal("v", (string?)Fixture.Multiplexer.GetDatabase(1).StringGet(key));
        Fixture.Multiplexer.GetDatabase(1).KeyDelete(key);
        session.AssertObserved(nameof(IDatabase.KeyMove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.KeyMoveAsync))]
    public async Task Given_redis_When_KeyMoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var moved = await session.Database.KeyMoveAsync(key, 1);
        Assert.True(moved);
        Assert.False(session.Raw.KeyExists(key));
        Assert.Equal("v", (string?)Fixture.Multiplexer.GetDatabase(1).StringGet(key));
        Fixture.Multiplexer.GetDatabase(1).KeyDelete(key);
        session.AssertObserved(nameof(IDatabase.KeyMoveAsync));
    }

    [Fact(Skip = "Requires a second Redis instance")]
    [RedisMethod(nameof(IDatabase.KeyMigrate))]
    public void Given_redis_When_KeyMigrate_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        Assert.True(true);
    }

    [Fact(Skip = "Requires a second Redis instance")]
    [RedisMethod(nameof(IDatabase.KeyMigrateAsync))]
    public void Given_redis_When_KeyMigrateAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        Assert.True(true);
    }

    [Fact(Skip = "Requires maxmemory-policy LFU")]
    [RedisMethod(nameof(IDatabase.KeyFrequency))]
    public void Given_redis_When_KeyFrequency_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        Assert.True(true);
    }

    [Fact(Skip = "Requires maxmemory-policy LFU")]
    [RedisMethod(nameof(IDatabase.KeyFrequencyAsync))]
    public void Given_redis_When_KeyFrequencyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        Assert.True(true);
    }
}
