using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class StringTests : RedisTestBase
{
    public StringTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSet))]
    public void Given_redis_When_StringSet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var written = session.Database.StringSet(key, "v");
        Assert.True(written);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetAsync))]
    public async Task Given_redis_When_StringSetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var written = await session.Database.StringSetAsync(key, "v");
        Assert.True(written);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGet))]
    public void Given_redis_When_StringGet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var value = session.Database.StringGet(key);
        Assert.Equal("v", (string?)value);
        session.AssertObserved(nameof(IDatabase.StringGet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetAsync))]
    public async Task Given_redis_When_StringGetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var value = await session.Database.StringGetAsync(key);
        Assert.Equal("v", (string?)value);
        session.AssertObserved(nameof(IDatabase.StringGetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetDelete))]
    public void Given_redis_When_StringGetDelete_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var value = session.Database.StringGetDelete(key);
        Assert.Equal("v", (string?)value);
        Assert.False(session.Raw.KeyExists(key));
        session.AssertObserved(nameof(IDatabase.StringGetDelete));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetDeleteAsync))]
    public async Task Given_redis_When_StringGetDeleteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var value = await session.Database.StringGetDeleteAsync(key);
        Assert.Equal("v", (string?)value);
        Assert.False(session.Raw.KeyExists(key));
        session.AssertObserved(nameof(IDatabase.StringGetDeleteAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetSet))]
    public void Given_redis_When_StringGetSet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "old");
        var previous = session.Database.StringGetSet(key, "new");
        Assert.Equal("old", (string?)previous);
        Assert.Equal("new", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringGetSet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetSetAsync))]
    public async Task Given_redis_When_StringGetSetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "old");
        var previous = await session.Database.StringGetSetAsync(key, "new");
        Assert.Equal("old", (string?)previous);
        Assert.Equal("new", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringGetSetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetSetExpiry))]
    public void Given_redis_When_StringGetSetExpiry_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var previous = session.Database.StringGetSetExpiry(key, TimeSpan.FromMinutes(1));
        Assert.Equal("v", (string?)previous);
        Assert.NotNull(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.StringGetSetExpiry));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetSetExpiryAsync))]
    public async Task Given_redis_When_StringGetSetExpiryAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        var previous = await session.Database.StringGetSetExpiryAsync(key, TimeSpan.FromMinutes(1));
        Assert.Equal("v", (string?)previous);
        Assert.NotNull(session.Raw.KeyTimeToLive(key));
        session.AssertObserved(nameof(IDatabase.StringGetSetExpiryAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetWithExpiry))]
    public void Given_redis_When_StringGetWithExpiry_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var value = session.Database.StringGetWithExpiry(key);
        Assert.Equal("v", (string?)value.Value);
        Assert.NotNull(value.Expiry);
        session.AssertObserved(nameof(IDatabase.StringGetWithExpiry));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetWithExpiryAsync))]
    public async Task Given_redis_When_StringGetWithExpiryAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v", TimeSpan.FromMinutes(1));
        var value = await session.Database.StringGetWithExpiryAsync(key);
        Assert.Equal("v", (string?)value.Value);
        Assert.NotNull(value.Expiry);
        session.AssertObserved(nameof(IDatabase.StringGetWithExpiryAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringAppend))]
    public void Given_redis_When_StringAppend_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "a");
        var length = session.Database.StringAppend(key, "b");
        Assert.Equal(2, length);
        Assert.Equal("ab", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringAppend));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringAppendAsync))]
    public async Task Given_redis_When_StringAppendAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "a");
        var length = await session.Database.StringAppendAsync(key, "b");
        Assert.Equal(2, length);
        Assert.Equal("ab", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringAppendAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLength))]
    public void Given_redis_When_StringLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abc");
        var length = session.Database.StringLength(key);
        Assert.Equal(3, length);
        session.AssertObserved(nameof(IDatabase.StringLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLengthAsync))]
    public async Task Given_redis_When_StringLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abc");
        var length = await session.Database.StringLengthAsync(key);
        Assert.Equal(3, length);
        session.AssertObserved(nameof(IDatabase.StringLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringIncrement))]
    public void Given_redis_When_StringIncrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var value = session.Database.StringIncrement(key);
        Assert.Equal(1, value);
        session.AssertObserved(nameof(IDatabase.StringIncrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringIncrementAsync))]
    public async Task Given_redis_When_StringIncrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var value = await session.Database.StringIncrementAsync(key);
        Assert.Equal(1, value);
        session.AssertObserved(nameof(IDatabase.StringIncrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringDecrement))]
    public void Given_redis_When_StringDecrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "5");
        var value = session.Database.StringDecrement(key);
        Assert.Equal(4, value);
        session.AssertObserved(nameof(IDatabase.StringDecrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringDecrementAsync))]
    public async Task Given_redis_When_StringDecrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "5");
        var value = await session.Database.StringDecrementAsync(key);
        Assert.Equal(4, value);
        session.AssertObserved(nameof(IDatabase.StringDecrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetRange))]
    public void Given_redis_When_StringGetRange_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abcd");
        var value = session.Database.StringGetRange(key, 1, 2);
        Assert.Equal("bc", (string?)value);
        session.AssertObserved(nameof(IDatabase.StringGetRange));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetRangeAsync))]
    public async Task Given_redis_When_StringGetRangeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abcd");
        var value = await session.Database.StringGetRangeAsync(key, 1, 2);
        Assert.Equal("bc", (string?)value);
        session.AssertObserved(nameof(IDatabase.StringGetRangeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetRange))]
    public void Given_redis_When_StringSetRange_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abcd");
        var value = session.Database.StringSetRange(key, 1, "XY");
        Assert.Equal("aXYd", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSetRange));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetRangeAsync))]
    public async Task Given_redis_When_StringSetRangeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "abcd");
        await session.Database.StringSetRangeAsync(key, 1, "XY");
        Assert.Equal("aXYd", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSetRangeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetBit))]
    public void Given_redis_When_StringSetBit_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var previous = session.Database.StringSetBit(key, 0, true);
        Assert.False(previous);
        Assert.True(session.Raw.StringGetBit(key, 0));
        session.AssertObserved(nameof(IDatabase.StringSetBit));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetBitAsync))]
    public async Task Given_redis_When_StringSetBitAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var previous = await session.Database.StringSetBitAsync(key, 0, true);
        Assert.False(previous);
        Assert.True(session.Raw.StringGetBit(key, 0));
        session.AssertObserved(nameof(IDatabase.StringSetBitAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetBit))]
    public void Given_redis_When_StringGetBit_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 0, true);
        var bit = session.Database.StringGetBit(key, 0);
        Assert.True(bit);
        session.AssertObserved(nameof(IDatabase.StringGetBit));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetBitAsync))]
    public async Task Given_redis_When_StringGetBitAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 0, true);
        var bit = await session.Database.StringGetBitAsync(key, 0);
        Assert.True(bit);
        session.AssertObserved(nameof(IDatabase.StringGetBitAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitCount))]
    public void Given_redis_When_StringBitCount_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 0, true);
        var count = session.Database.StringBitCount(key);
        Assert.Equal(1, count);
        session.AssertObserved(nameof(IDatabase.StringBitCount));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitCountAsync))]
    public async Task Given_redis_When_StringBitCountAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 0, true);
        var count = await session.Database.StringBitCountAsync(key);
        Assert.Equal(1, count);
        session.AssertObserved(nameof(IDatabase.StringBitCountAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitPosition))]
    public void Given_redis_When_StringBitPosition_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 2, true);
        var position = session.Database.StringBitPosition(key, true);
        Assert.Equal(2, position);
        session.AssertObserved(nameof(IDatabase.StringBitPosition));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitPositionAsync))]
    public async Task Given_redis_When_StringBitPositionAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSetBit(key, 2, true);
        var position = await session.Database.StringBitPositionAsync(key, true);
        Assert.Equal(2, position);
        session.AssertObserved(nameof(IDatabase.StringBitPositionAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitOperation))]
    public void Given_redis_When_StringBitOperation_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var left = Fixture.NewKey("bl");
        var right = Fixture.NewKey("br");
        session.Raw.StringSetBit(left, 0, true);
        session.Raw.StringSetBit(right, 0, true);
        var length = session.Database.StringBitOperation(Bitwise.And, key, left, right);
        Assert.True(length >= 1);
        Assert.True(session.Raw.StringGetBit(key, 0));
        session.AssertObserved(nameof(IDatabase.StringBitOperation));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringBitOperationAsync))]
    public async Task Given_redis_When_StringBitOperationAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var left = Fixture.NewKey("bl");
        var right = Fixture.NewKey("br");
        session.Raw.StringSetBit(left, 0, true);
        session.Raw.StringSetBit(right, 0, true);
        var length = await session.Database.StringBitOperationAsync(Bitwise.And, key, left, right);
        Assert.True(length >= 1);
        Assert.True(session.Raw.StringGetBit(key, 0));
        session.AssertObserved(nameof(IDatabase.StringBitOperationAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetLease))]
    public void Given_redis_When_StringGetLease_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        using var lease = session.Database.StringGetLease(key);
        Assert.NotNull(lease);
        session.AssertObserved(nameof(IDatabase.StringGetLease));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringGetLeaseAsync))]
    public async Task Given_redis_When_StringGetLeaseAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StringSet(key, "v");
        using var lease = await session.Database.StringGetLeaseAsync(key);
        Assert.NotNull(lease);
        session.AssertObserved(nameof(IDatabase.StringGetLeaseAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetAndGet))]
    public void Given_redis_When_StringSetAndGet_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var previous = session.Database.StringSetAndGet(key, "v");
        Assert.True(previous.IsNull);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSetAndGet));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringSetAndGetAsync))]
    public async Task Given_redis_When_StringSetAndGetAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var previous = await session.Database.StringSetAndGetAsync(key, "v");
        Assert.True(previous.IsNull);
        Assert.Equal("v", (string?)session.Raw.StringGet(key));
        session.AssertObserved(nameof(IDatabase.StringSetAndGetAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringDelete))]
    public void Given_redis_When_StringDelete_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        session.Raw.StringSet(key, "v");
        session.InvokeAndObserve(nameof(IDatabase.StringDelete), () => session.Database.StringDelete(key, ValueCondition.Exists));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringDigest))]
    public void Given_redis_When_StringDigest_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();
        session.Raw.StringSet(key, "v");
        session.InvokeAndObserve(nameof(IDatabase.StringDigest), () => session.Database.StringDigest(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequence))]
    public void Given_redis_When_StringLongestCommonSubsequence_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var lcs = session.Database.StringLongestCommonSubsequence(key, other);
        Assert.Equal("ace", lcs);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequence));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequenceAsync))]
    public async Task Given_redis_When_StringLongestCommonSubsequenceAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var lcs = await session.Database.StringLongestCommonSubsequenceAsync(key, other);
        Assert.Equal("ace", lcs);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequenceAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequenceLength))]
    public void Given_redis_When_StringLongestCommonSubsequenceLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var length = session.Database.StringLongestCommonSubsequenceLength(key, other);
        Assert.Equal(3, length);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequenceLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequenceLengthAsync))]
    public async Task Given_redis_When_StringLongestCommonSubsequenceLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var length = await session.Database.StringLongestCommonSubsequenceLengthAsync(key, other);
        Assert.Equal(3, length);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequenceLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequenceWithMatches))]
    public void Given_redis_When_StringLongestCommonSubsequenceWithMatches_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var matches = session.Database.StringLongestCommonSubsequenceWithMatches(key, other);
        Assert.True(matches.LongestMatchLength >= 1);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequenceWithMatches));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StringLongestCommonSubsequenceWithMatchesAsync))]
    public async Task Given_redis_When_StringLongestCommonSubsequenceWithMatchesAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var other = Fixture.NewKey("s2");
        session.Raw.StringSet(key, "abcde");
        session.Raw.StringSet(other, "ace");
        var matches = await session.Database.StringLongestCommonSubsequenceWithMatchesAsync(key, other);
        Assert.True(matches.LongestMatchLength >= 1);
        session.AssertObserved(nameof(IDatabase.StringLongestCommonSubsequenceWithMatchesAsync));
    }
}
