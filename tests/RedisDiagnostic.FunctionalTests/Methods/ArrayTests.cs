using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class ArrayTests : RedisTestBase
{
    public ArrayTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArraySet))]
    public void Given_redis_When_ArraySet_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        session.InvokeAndObserve(nameof(IDatabase.ArraySet), () => session.Database.ArraySet(key, 0, "v"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayGet))]
    public void Given_redis_When_ArrayGet_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayGet), () => session.Database.ArrayGet(key, 0));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayGetRange))]
    public void Given_redis_When_ArrayGetRange_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayGetRange), () => session.Database.ArrayGetRange(key, 0, 0));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayLength))]
    public void Given_redis_When_ArrayLength_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayLength), () => session.Database.ArrayLength(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayCount))]
    public void Given_redis_When_ArrayCount_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayCount), () => session.Database.ArrayCount(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayDelete))]
    public void Given_redis_When_ArrayDelete_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayDelete), () => session.Database.ArrayDelete(key, 0));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayDeleteRange))]
    public void Given_redis_When_ArrayDeleteRange_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayDeleteRange), () => session.Database.ArrayDeleteRange(key, 0, 0));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayScan))]
    public void Given_redis_When_ArrayScan_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayScan), () => session.Database.ArrayScan(key, 0, 0));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayGrep))]
    public void Given_redis_When_ArrayGrep_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        var request = new ArrayGrepRequest();
        request.AddPredicate(ArrayGrepRequest.Predicate.Exact("v"));
        session.InvokeAndObserve(nameof(IDatabase.ArrayGrep), () => session.Database.ArrayGrep(key, request));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayOperation))]
    public void Given_redis_When_ArrayOperation_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(
            nameof(IDatabase.ArrayOperation),
            () => session.Database.ArrayOperation(key, 0, 0, ArrayOperation.Sum));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayRing))]
    public void Given_redis_When_ArrayRing_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        session.InvokeAndObserve(nameof(IDatabase.ArrayRing), () => session.Database.ArrayRing(key, 8, "v"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayNext))]
    public void Given_redis_When_ArrayNext_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayNext), () => session.Database.ArrayNext(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayInsert))]
    public void Given_redis_When_ArrayInsert_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        session.InvokeAndObserve(nameof(IDatabase.ArrayInsert), () => session.Database.ArrayInsert(key, "v"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArraySeek))]
    public void Given_redis_When_ArraySeek_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArraySeek), () => session.Database.ArraySeek(key, 1));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayLastItems))]
    public void Given_redis_When_ArrayLastItems_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayLastItems), () => session.Database.ArrayLastItems(key, 1));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ArrayInfo))]
    public void Given_redis_When_ArrayInfo_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        TryArraySet(session.Raw, key);
        session.InvokeAndObserve(nameof(IDatabase.ArrayInfo), () => session.Database.ArrayInfo(key));
    }

    private static void TryArraySet(IDatabase database, RedisKey key)
    {
        try
        {
            database.ArraySet(key, 0, "v");
        }
        catch (RedisServerException)
        {
        }
    }
}
