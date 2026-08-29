using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class LockScriptSortHyperLogLogTests : RedisTestBase
{
    public LockScriptSortHyperLogLogTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockTake))]
    public void Given_redis_When_LockTake_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var taken = session.Database.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        Assert.True(taken);
        session.AssertObserved(nameof(IDatabase.LockTake));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockTakeAsync))]
    public async Task Given_redis_When_LockTakeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var taken = await session.Database.LockTakeAsync(key, "tok", TimeSpan.FromSeconds(10));
        Assert.True(taken);
        session.AssertObserved(nameof(IDatabase.LockTakeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockQuery))]
    public void Given_redis_When_LockQuery_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var token = session.Database.LockQuery(key);
        Assert.Equal("tok", (string?)token);
        session.AssertObserved(nameof(IDatabase.LockQuery));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockQueryAsync))]
    public async Task Given_redis_When_LockQueryAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var token = await session.Database.LockQueryAsync(key);
        Assert.Equal("tok", (string?)token);
        session.AssertObserved(nameof(IDatabase.LockQueryAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockExtend))]
    public void Given_redis_When_LockExtend_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var extended = session.Database.LockExtend(key, "tok", TimeSpan.FromSeconds(20));
        Assert.True(extended);
        session.AssertObserved(nameof(IDatabase.LockExtend));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockExtendAsync))]
    public async Task Given_redis_When_LockExtendAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var extended = await session.Database.LockExtendAsync(key, "tok", TimeSpan.FromSeconds(20));
        Assert.True(extended);
        session.AssertObserved(nameof(IDatabase.LockExtendAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockRelease))]
    public void Given_redis_When_LockRelease_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var released = session.Database.LockRelease(key, "tok");
        Assert.True(released);
        session.AssertObserved(nameof(IDatabase.LockRelease));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.LockReleaseAsync))]
    public async Task Given_redis_When_LockReleaseAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.LockTake(key, "tok", TimeSpan.FromSeconds(10));
        var released = await session.Database.LockReleaseAsync(key, "tok");
        Assert.True(released);
        session.AssertObserved(nameof(IDatabase.LockReleaseAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluate))]
    public void Given_redis_When_ScriptEvaluate_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var result = session.Database.ScriptEvaluate("return 7");
        Assert.Equal(7, (int)result);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluate));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateAsync))]
    public async Task Given_redis_When_ScriptEvaluateAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var result = await session.Database.ScriptEvaluateAsync("return 7");
        Assert.Equal(7, (int)result);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateReadOnly))]
    public void Given_redis_When_ScriptEvaluateReadOnly_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var result = session.Database.ScriptEvaluateReadOnly("return 3");
        Assert.Equal(3, (int)result);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateReadOnly));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateReadOnlyAsync))]
    public async Task Given_redis_When_ScriptEvaluateReadOnlyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var result = await session.Database.ScriptEvaluateReadOnlyAsync("return 3");
        Assert.Equal(3, (int)result);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateReadOnlyAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.Sort))]
    public void Given_redis_When_Sort_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["2", "1"]);
        var values = session.Database.Sort(key);
        Assert.Equal("1", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.Sort));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAsync))]
    public async Task Given_redis_When_SortAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["2", "1"]);
        var values = await session.Database.SortAsync(key);
        Assert.Equal("1", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.SortAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAndStore))]
    public void Given_redis_When_SortAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("so");
        session.Raw.ListRightPush(key, ["2", "1"]);
        var length = session.Database.SortAndStore(dest, key);
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.SortAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortAndStoreAsync))]
    public async Task Given_redis_When_SortAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("so");
        session.Raw.ListRightPush(key, ["2", "1"]);
        var length = await session.Database.SortAndStoreAsync(dest, key);
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.SortAndStoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogAdd))]
    public void Given_redis_When_HyperLogLogAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var added = session.Database.HyperLogLogAdd(key, "a");
        Assert.True(added);
        session.AssertObserved(nameof(IDatabase.HyperLogLogAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogAddAsync))]
    public async Task Given_redis_When_HyperLogLogAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var added = await session.Database.HyperLogLogAddAsync(key, "a");
        Assert.True(added);
        session.AssertObserved(nameof(IDatabase.HyperLogLogAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogLength))]
    public void Given_redis_When_HyperLogLogLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HyperLogLogAdd(key, "a");
        var length = session.Database.HyperLogLogLength(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.HyperLogLogLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogLengthAsync))]
    public async Task Given_redis_When_HyperLogLogLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.HyperLogLogAdd(key, "a");
        var length = await session.Database.HyperLogLogLengthAsync(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.HyperLogLogLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogMerge))]
    public void Given_redis_When_HyperLogLogMerge_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var first = Fixture.NewKey("h1");
        var second = Fixture.NewKey("h2");
        session.Raw.HyperLogLogAdd(first, "a");
        session.Raw.HyperLogLogAdd(second, "b");
        session.Database.HyperLogLogMerge(key, first, second);
        Assert.Equal(2, session.Raw.HyperLogLogLength(key));
        session.AssertObserved(nameof(IDatabase.HyperLogLogMerge));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.HyperLogLogMergeAsync))]
    public async Task Given_redis_When_HyperLogLogMergeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var first = Fixture.NewKey("h1");
        var second = Fixture.NewKey("h2");
        session.Raw.HyperLogLogAdd(first, "a");
        session.Raw.HyperLogLogAdd(second, "b");
        await session.Database.HyperLogLogMergeAsync(key, first, second);
        Assert.Equal(2, session.Raw.HyperLogLogLength(key));
        session.AssertObserved(nameof(IDatabase.HyperLogLogMergeAsync));
    }
}
