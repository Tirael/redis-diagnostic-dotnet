using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class StreamTests : RedisTestBase
{
    public StreamTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAdd))]
    public void Given_redis_When_StreamAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Database.StreamAdd(key, "f", "v");
        Assert.False(id.IsNull);
        Assert.Equal(1, session.Raw.StreamLength(key));
        session.AssertObserved(nameof(IDatabase.StreamAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAddAsync))]
    public async Task Given_redis_When_StreamAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = await session.Database.StreamAddAsync(key, "f", "v");
        Assert.False(id.IsNull);
        session.AssertObserved(nameof(IDatabase.StreamAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamLength))]
    public void Given_redis_When_StreamLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var length = session.Database.StreamLength(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.StreamLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamLengthAsync))]
    public async Task Given_redis_When_StreamLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var length = await session.Database.StreamLengthAsync(key);
        Assert.Equal(1, length);
        session.AssertObserved(nameof(IDatabase.StreamLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamRange))]
    public void Given_redis_When_StreamRange_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var entries = session.Database.StreamRange(key);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamRange));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamRangeAsync))]
    public async Task Given_redis_When_StreamRangeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var entries = await session.Database.StreamRangeAsync(key);
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamRangeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamRead))]
    public void Given_redis_When_StreamRead_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var entries = session.Database.StreamRead(key, "0-0");
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamRead));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamReadAsync))]
    public async Task Given_redis_When_StreamReadAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var entries = await session.Database.StreamReadAsync(key, "0-0");
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamReadAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamInfo))]
    public void Given_redis_When_StreamInfo_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var info = session.Database.StreamInfo(key);
        Assert.Equal(1, info.Length);
        session.AssertObserved(nameof(IDatabase.StreamInfo));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamInfoAsync))]
    public async Task Given_redis_When_StreamInfoAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var info = await session.Database.StreamInfoAsync(key);
        Assert.Equal(1, info.Length);
        session.AssertObserved(nameof(IDatabase.StreamInfoAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDelete))]
    public void Given_redis_When_StreamDelete_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        var deleted = session.Database.StreamDelete(key, [id]);
        Assert.Equal(1, deleted);
        session.AssertObserved(nameof(IDatabase.StreamDelete));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDeleteAsync))]
    public async Task Given_redis_When_StreamDeleteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        var deleted = await session.Database.StreamDeleteAsync(key, [id]);
        Assert.Equal(1, deleted);
        session.AssertObserved(nameof(IDatabase.StreamDeleteAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamTrim))]
    public void Given_redis_When_StreamTrim_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v1");
        session.Raw.StreamAdd(key, "f", "v2");
        var trimmed = session.Database.StreamTrim(key, 1);
        Assert.Equal(1, trimmed);
        session.AssertObserved(nameof(IDatabase.StreamTrim));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamTrimAsync))]
    public async Task Given_redis_When_StreamTrimAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v1");
        session.Raw.StreamAdd(key, "f", "v2");
        var trimmed = await session.Database.StreamTrimAsync(key, 1);
        Assert.Equal(1, trimmed);
        session.AssertObserved(nameof(IDatabase.StreamTrimAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamTrimByMinId))]
    public void Given_redis_When_StreamTrimByMinId_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var first = session.Raw.StreamAdd(key, "f", "v1");
        session.Raw.StreamAdd(key, "f", "v2");
        var trimmed = session.Database.StreamTrimByMinId(key, first);
        Assert.True(trimmed >= 0);
        session.AssertObserved(nameof(IDatabase.StreamTrimByMinId));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamTrimByMinIdAsync))]
    public async Task Given_redis_When_StreamTrimByMinIdAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var first = session.Raw.StreamAdd(key, "f", "v1");
        session.Raw.StreamAdd(key, "f", "v2");
        var trimmed = await session.Database.StreamTrimByMinIdAsync(key, first);
        Assert.True(trimmed >= 0);
        session.AssertObserved(nameof(IDatabase.StreamTrimByMinIdAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamCreateConsumerGroup))]
    public void Given_redis_When_StreamCreateConsumerGroup_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var created = session.Database.StreamCreateConsumerGroup(key, "g", "0-0");
        Assert.True(created);
        session.AssertObserved(nameof(IDatabase.StreamCreateConsumerGroup));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamCreateConsumerGroupAsync))]
    public async Task Given_redis_When_StreamCreateConsumerGroupAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        var created = await session.Database.StreamCreateConsumerGroupAsync(key, "g", "0-0");
        Assert.True(created);
        session.AssertObserved(nameof(IDatabase.StreamCreateConsumerGroupAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamGroupInfo))]
    public void Given_redis_When_StreamGroupInfo_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var groups = session.Database.StreamGroupInfo(key);
        Assert.Single(groups);
        session.AssertObserved(nameof(IDatabase.StreamGroupInfo));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamGroupInfoAsync))]
    public async Task Given_redis_When_StreamGroupInfoAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var groups = await session.Database.StreamGroupInfoAsync(key);
        Assert.Single(groups);
        session.AssertObserved(nameof(IDatabase.StreamGroupInfoAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamReadGroup))]
    public void Given_redis_When_StreamReadGroup_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var entries = session.Database.StreamReadGroup(key, "g", "c", ">");
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamReadGroup));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamReadGroupAsync))]
    public async Task Given_redis_When_StreamReadGroupAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var entries = await session.Database.StreamReadGroupAsync(key, "g", "c", ">");
        Assert.Single(entries);
        session.AssertObserved(nameof(IDatabase.StreamReadGroupAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAcknowledge))]
    public void Given_redis_When_StreamAcknowledge_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var acked = session.Database.StreamAcknowledge(key, "g", id);
        Assert.Equal(1, acked);
        session.AssertObserved(nameof(IDatabase.StreamAcknowledge));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAcknowledgeAsync))]
    public async Task Given_redis_When_StreamAcknowledgeAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var acked = await session.Database.StreamAcknowledgeAsync(key, "g", id);
        Assert.Equal(1, acked);
        session.AssertObserved(nameof(IDatabase.StreamAcknowledgeAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamPending))]
    public void Given_redis_When_StreamPending_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = session.Database.StreamPending(key, "g");
        Assert.Equal(1, pending.PendingMessageCount);
        session.AssertObserved(nameof(IDatabase.StreamPending));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamPendingAsync))]
    public async Task Given_redis_When_StreamPendingAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = await session.Database.StreamPendingAsync(key, "g");
        Assert.Equal(1, pending.PendingMessageCount);
        session.AssertObserved(nameof(IDatabase.StreamPendingAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamPendingMessages))]
    public void Given_redis_When_StreamPendingMessages_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = session.Database.StreamPendingMessages(key, "g", 10, "c");
        Assert.Single(pending);
        session.AssertObserved(nameof(IDatabase.StreamPendingMessages));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamPendingMessagesAsync))]
    public async Task Given_redis_When_StreamPendingMessagesAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = await session.Database.StreamPendingMessagesAsync(key, "g", 10, "c");
        Assert.Single(pending);
        session.AssertObserved(nameof(IDatabase.StreamPendingMessagesAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamConsumerInfo))]
    public void Given_redis_When_StreamConsumerInfo_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var consumers = session.Database.StreamConsumerInfo(key, "g");
        Assert.Single(consumers);
        session.AssertObserved(nameof(IDatabase.StreamConsumerInfo));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamConsumerInfoAsync))]
    public async Task Given_redis_When_StreamConsumerInfoAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var consumers = await session.Database.StreamConsumerInfoAsync(key, "g");
        Assert.Single(consumers);
        session.AssertObserved(nameof(IDatabase.StreamConsumerInfoAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamConsumerGroupSetPosition))]
    public void Given_redis_When_StreamConsumerGroupSetPosition_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var set = session.Database.StreamConsumerGroupSetPosition(key, "g", "0-0");
        Assert.True(set);
        session.AssertObserved(nameof(IDatabase.StreamConsumerGroupSetPosition));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamConsumerGroupSetPositionAsync))]
    public async Task Given_redis_When_StreamConsumerGroupSetPositionAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var set = await session.Database.StreamConsumerGroupSetPositionAsync(key, "g", "0-0");
        Assert.True(set);
        session.AssertObserved(nameof(IDatabase.StreamConsumerGroupSetPositionAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamClaim))]
    public void Given_redis_When_StreamClaim_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var pending = session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = session.Database.StreamClaim(key, "g", "c2", 0, [pending[0].Id]);
        Assert.Single(claimed);
        session.AssertObserved(nameof(IDatabase.StreamClaim));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamClaimAsync))]
    public async Task Given_redis_When_StreamClaimAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var pending = session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = await session.Database.StreamClaimAsync(key, "g", "c2", 0, [pending[0].Id]);
        Assert.Single(claimed);
        session.AssertObserved(nameof(IDatabase.StreamClaimAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamClaimIdsOnly))]
    public void Given_redis_When_StreamClaimIdsOnly_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var pending = session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = session.Database.StreamClaimIdsOnly(key, "g", "c2", 0, [pending[0].Id]);
        Assert.Single(claimed);
        session.AssertObserved(nameof(IDatabase.StreamClaimIdsOnly));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamClaimIdsOnlyAsync))]
    public async Task Given_redis_When_StreamClaimIdsOnlyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var pending = session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = await session.Database.StreamClaimIdsOnlyAsync(key, "g", "c2", 0, [pending[0].Id]);
        Assert.Single(claimed);
        session.AssertObserved(nameof(IDatabase.StreamClaimIdsOnlyAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAutoClaim))]
    public void Given_redis_When_StreamAutoClaim_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = session.Database.StreamAutoClaim(key, "g", "c2", 0, "0-0");
        Assert.NotEmpty(claimed.ClaimedEntries);
        session.AssertObserved(nameof(IDatabase.StreamAutoClaim));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAutoClaimAsync))]
    public async Task Given_redis_When_StreamAutoClaimAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = await session.Database.StreamAutoClaimAsync(key, "g", "c2", 0, "0-0");
        Assert.NotEmpty(claimed.ClaimedEntries);
        session.AssertObserved(nameof(IDatabase.StreamAutoClaimAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAutoClaimIdsOnly))]
    public void Given_redis_When_StreamAutoClaimIdsOnly_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = session.Database.StreamAutoClaimIdsOnly(key, "g", "c2", 0, "0-0");
        Assert.NotEmpty(claimed.ClaimedIds);
        session.AssertObserved(nameof(IDatabase.StreamAutoClaimIdsOnly));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAutoClaimIdsOnlyAsync))]
    public async Task Given_redis_When_StreamAutoClaimIdsOnlyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c1", ">");
        var claimed = await session.Database.StreamAutoClaimIdsOnlyAsync(key, "g", "c2", 0, "0-0");
        Assert.NotEmpty(claimed.ClaimedIds);
        session.AssertObserved(nameof(IDatabase.StreamAutoClaimIdsOnlyAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDeleteConsumer))]
    public void Given_redis_When_StreamDeleteConsumer_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = session.Database.StreamDeleteConsumer(key, "g", "c");
        Assert.True(pending >= 0);
        session.AssertObserved(nameof(IDatabase.StreamDeleteConsumer));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDeleteConsumerAsync))]
    public async Task Given_redis_When_StreamDeleteConsumerAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        var pending = await session.Database.StreamDeleteConsumerAsync(key, "g", "c");
        Assert.True(pending >= 0);
        session.AssertObserved(nameof(IDatabase.StreamDeleteConsumerAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDeleteConsumerGroup))]
    public void Given_redis_When_StreamDeleteConsumerGroup_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var deleted = session.Database.StreamDeleteConsumerGroup(key, "g");
        Assert.True(deleted);
        session.AssertObserved(nameof(IDatabase.StreamDeleteConsumerGroup));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamDeleteConsumerGroupAsync))]
    public async Task Given_redis_When_StreamDeleteConsumerGroupAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        var deleted = await session.Database.StreamDeleteConsumerGroupAsync(key, "g");
        Assert.True(deleted);
        session.AssertObserved(nameof(IDatabase.StreamDeleteConsumerGroupAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAcknowledgeAndDelete))]
    public void Given_redis_When_StreamAcknowledgeAndDelete_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        try
        {
            session.Database.StreamAcknowledgeAndDelete(key, "g", StreamTrimMode.KeepReferences, id);
            session.AssertObserved(nameof(IDatabase.StreamAcknowledgeAndDelete));
        }
        catch (RedisServerException)
        {
            session.AssertObserved(nameof(IDatabase.StreamAcknowledgeAndDelete), PrometheusRedisMethodMetrics.ResultError);
        }
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.StreamAcknowledgeAndDeleteAsync))]
    public async Task Given_redis_When_StreamAcknowledgeAndDeleteAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = Fixture.NewKey();

        // Act / Assert
        var id = session.Raw.StreamAdd(key, "f", "v");
        session.Raw.StreamCreateConsumerGroup(key, "g", "0-0");
        session.Raw.StreamReadGroup(key, "g", "c", ">");
        try
        {
            await session.Database.StreamAcknowledgeAndDeleteAsync(key, "g", StreamTrimMode.KeepReferences, id);
            session.AssertObserved(nameof(IDatabase.StreamAcknowledgeAndDeleteAsync));
        }
        catch (RedisServerException)
        {
            session.AssertObserved(nameof(IDatabase.StreamAcknowledgeAndDeleteAsync), PrometheusRedisMethodMetrics.ResultError);
        }
    }
}
