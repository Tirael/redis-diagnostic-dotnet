using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class ListTests : RedisTestBase
{
    public ListTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLeftPush))]
    public void Given_redis_When_ListLeftPush_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var length = session.Database.ListLeftPush(key, "a");
        Assert.Equal(1, length);
        Assert.Equal("a", (string?)session.Raw.ListGetByIndex(key, 0));
        session.AssertObserved(nameof(IDatabase.ListLeftPush));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLeftPushAsync))]
    public async Task Given_redis_When_ListLeftPushAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var length = await session.Database.ListLeftPushAsync(key, "a");
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.ListLeftPushAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPush))]
    public void Given_redis_When_ListRightPush_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var length = session.Database.ListRightPush(key, "a");
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.ListRightPush));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPushAsync))]
    public async Task Given_redis_When_ListRightPushAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var length = await session.Database.ListRightPushAsync(key, "a");
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.ListRightPushAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLeftPop))]
    public void Given_redis_When_ListLeftPop_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = session.Database.ListLeftPop(key);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListLeftPop));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLeftPopAsync))]
    public async Task Given_redis_When_ListLeftPopAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = await session.Database.ListLeftPopAsync(key);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListLeftPopAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPop))]
    public void Given_redis_When_ListRightPop_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = session.Database.ListRightPop(key);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListRightPop));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPopAsync))]
    public async Task Given_redis_When_ListRightPopAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = await session.Database.ListRightPopAsync(key);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListRightPopAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLength))]
    public void Given_redis_When_ListLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = session.Database.ListLength(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.ListLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListLengthAsync))]
    public async Task Given_redis_When_ListLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = await session.Database.ListLengthAsync(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.ListLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRange))]
    public void Given_redis_When_ListRange_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var values = session.Database.ListRange(key);
        Assert.Equal("a", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.ListRange));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRangeAsync))]
    public async Task Given_redis_When_ListRangeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var values = await session.Database.ListRangeAsync(key);
        Assert.Equal("a", (string?)values[0]);
        session.AssertObserved(nameof(IDatabase.ListRangeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListGetByIndex))]
    public void Given_redis_When_ListGetByIndex_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = session.Database.ListGetByIndex(key, 0);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListGetByIndex));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListGetByIndexAsync))]
    public async Task Given_redis_When_ListGetByIndexAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var value = await session.Database.ListGetByIndexAsync(key, 0);
        Assert.Equal("a", (string?)value);
        session.AssertObserved(nameof(IDatabase.ListGetByIndexAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListSetByIndex))]
    public void Given_redis_When_ListSetByIndex_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        session.Database.ListSetByIndex(key, 0, "b");
        Assert.Equal("b", (string?)session.Raw.ListGetByIndex(key, 0));
        session.AssertObserved(nameof(IDatabase.ListSetByIndex));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListSetByIndexAsync))]
    public async Task Given_redis_When_ListSetByIndexAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        await session.Database.ListSetByIndexAsync(key, 0, "b");
        Assert.Equal("b", (string?)session.Raw.ListGetByIndex(key, 0));
        session.AssertObserved(nameof(IDatabase.ListSetByIndexAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListInsertAfter))]
    public void Given_redis_When_ListInsertAfter_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = session.Database.ListInsertAfter(key, "a", "b");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.ListInsertAfter));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListInsertAfterAsync))]
    public async Task Given_redis_When_ListInsertAfterAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = await session.Database.ListInsertAfterAsync(key, "a", "b");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.ListInsertAfterAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListInsertBefore))]
    public void Given_redis_When_ListInsertBefore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = session.Database.ListInsertBefore(key, "a", "b");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.ListInsertBefore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListInsertBeforeAsync))]
    public async Task Given_redis_When_ListInsertBeforeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var length = await session.Database.ListInsertBeforeAsync(key, "a", "b");
        Assert.Equal(2, length);
        session.AssertObserved(nameof(IDatabase.ListInsertBeforeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRemove))]
    public void Given_redis_When_ListRemove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var removed = session.Database.ListRemove(key, "a");
        Assert.Equal(1, removed);
        session.AssertObserved(nameof(IDatabase.ListRemove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRemoveAsync))]
    public async Task Given_redis_When_ListRemoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var removed = await session.Database.ListRemoveAsync(key, "a");
        Assert.Equal(1, removed);
        session.AssertObserved(nameof(IDatabase.ListRemoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListTrim))]
    public void Given_redis_When_ListTrim_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["a", "b", "c"]);
        session.Database.ListTrim(key, 0, 1);
        Assert.Equal(2, session.Raw.ListLength(key));
        session.AssertObserved(nameof(IDatabase.ListTrim));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListTrimAsync))]
    public async Task Given_redis_When_ListTrimAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, ["a", "b", "c"]);
        await session.Database.ListTrimAsync(key, 0, 1);
        Assert.Equal(2, session.Raw.ListLength(key));
        session.AssertObserved(nameof(IDatabase.ListTrimAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListPosition))]
    public void Given_redis_When_ListPosition_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var position = session.Database.ListPosition(key, "a");
        Assert.Equal(0, position);
        session.AssertObserved(nameof(IDatabase.ListPosition));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListPositionAsync))]
    public async Task Given_redis_When_ListPositionAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var position = await session.Database.ListPositionAsync(key, "a");
        Assert.Equal(0, position);
        session.AssertObserved(nameof(IDatabase.ListPositionAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListPositions))]
    public void Given_redis_When_ListPositions_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var positions = session.Database.ListPositions(key, "a", 1);
        Assert.Equal(0, positions[0]);
        session.AssertObserved(nameof(IDatabase.ListPositions));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListPositionsAsync))]
    public async Task Given_redis_When_ListPositionsAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.ListRightPush(key, "a");
        var positions = await session.Database.ListPositionsAsync(key, "a", 1);
        Assert.Equal(0, positions[0]);
        session.AssertObserved(nameof(IDatabase.ListPositionsAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListMove))]
    public void Given_redis_When_ListMove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("ld");
        session.Raw.ListRightPush(key, "a");
        var moved = session.Database.ListMove(key, dest, ListSide.Left, ListSide.Right);
        Assert.Equal("a", (string?)moved);
        Assert.Equal("a", (string?)session.Raw.ListGetByIndex(dest, 0));
        session.AssertObserved(nameof(IDatabase.ListMove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListMoveAsync))]
    public async Task Given_redis_When_ListMoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("ld");
        session.Raw.ListRightPush(key, "a");
        var moved = await session.Database.ListMoveAsync(key, dest, ListSide.Left, ListSide.Right);
        Assert.Equal("a", (string?)moved);
        session.AssertObserved(nameof(IDatabase.ListMoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPopLeftPush))]
    public void Given_redis_When_ListRightPopLeftPush_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("ld");
        session.Raw.ListRightPush(key, "a");
        var moved = session.Database.ListRightPopLeftPush(key, dest);
        Assert.Equal("a", (string?)moved);
        session.AssertObserved(nameof(IDatabase.ListRightPopLeftPush));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ListRightPopLeftPushAsync))]
    public async Task Given_redis_When_ListRightPopLeftPushAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var dest = Fixture.NewKey("ld");
        session.Raw.ListRightPush(key, "a");
        var moved = await session.Database.ListRightPopLeftPushAsync(key, dest);
        Assert.Equal("a", (string?)moved);
        session.AssertObserved(nameof(IDatabase.ListRightPopLeftPushAsync));
    }
}
