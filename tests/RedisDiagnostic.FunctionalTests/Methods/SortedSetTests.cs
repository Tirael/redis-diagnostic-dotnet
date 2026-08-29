namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class SortedSetTests : RedisTestBase
{
    public SortedSetTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetAdd))]
    public void Given_redis_When_SortedSetAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = session.Database.SortedSetAdd(key, "a", 1);
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SortedSetAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetAddAsync))]
    public async Task Given_redis_When_SortedSetAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = await session.Database.SortedSetAddAsync(key, "a", 1);
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SortedSetAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemove))]
    public void Given_redis_When_SortedSetRemove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = session.Database.SortedSetRemove(key, "a");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SortedSetRemove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveAsync))]
    public async Task Given_redis_When_SortedSetRemoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = await session.Database.SortedSetRemoveAsync(key, "a");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScore))]
    public void Given_redis_When_SortedSetScore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1.5);
        var score = session.Database.SortedSetScore(key, "a");
        score.ShouldBe(1.5);
        session.AssertObserved(nameof(IDatabase.SortedSetScore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScoreAsync))]
    public async Task Given_redis_When_SortedSetScoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1.5);
        var score = await session.Database.SortedSetScoreAsync(key, "a");
        score.ShouldBe(1.5);
        session.AssertObserved(nameof(IDatabase.SortedSetScoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScores))]
    public void Given_redis_When_SortedSetScores_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1.5);
        var scores = session.Database.SortedSetScores(key, ["a"]);
        scores[0].ShouldBe(1.5);
        session.AssertObserved(nameof(IDatabase.SortedSetScores));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScoresAsync))]
    public async Task Given_redis_When_SortedSetScoresAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1.5);
        var scores = await session.Database.SortedSetScoresAsync(key, ["a"]);
        scores[0].ShouldBe(1.5);
        session.AssertObserved(nameof(IDatabase.SortedSetScoresAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRank))]
    public void Given_redis_When_SortedSetRank_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var rank = session.Database.SortedSetRank(key, "a");
        rank.ShouldBe(0);
        session.AssertObserved(nameof(IDatabase.SortedSetRank));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRankAsync))]
    public async Task Given_redis_When_SortedSetRankAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var rank = await session.Database.SortedSetRankAsync(key, "a");
        rank.ShouldBe(0);
        session.AssertObserved(nameof(IDatabase.SortedSetRankAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetLength))]
    public void Given_redis_When_SortedSetLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = session.Database.SortedSetLength(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetLengthAsync))]
    public async Task Given_redis_When_SortedSetLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = await session.Database.SortedSetLengthAsync(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetLengthByValue))]
    public void Given_redis_When_SortedSetLengthByValue_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = session.Database.SortedSetLengthByValue(key, "a", "a");
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetLengthByValue));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetLengthByValueAsync))]
    public async Task Given_redis_When_SortedSetLengthByValueAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = await session.Database.SortedSetLengthByValueAsync(key, "a", "a");
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetLengthByValueAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetIncrement))]
    public void Given_redis_When_SortedSetIncrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var score = session.Database.SortedSetIncrement(key, "a", 2);
        score.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetIncrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetIncrementAsync))]
    public async Task Given_redis_When_SortedSetIncrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var score = await session.Database.SortedSetIncrementAsync(key, "a", 2);
        score.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetIncrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetDecrement))]
    public void Given_redis_When_SortedSetDecrement_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 5);
        var score = session.Database.SortedSetDecrement(key, "a", 2);
        score.ShouldBe(3);
        session.AssertObserved(nameof(IDatabase.SortedSetDecrement));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetDecrementAsync))]
    public async Task Given_redis_When_SortedSetDecrementAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 5);
        var score = await session.Database.SortedSetDecrementAsync(key, "a", 2);
        score.ShouldBe(3);
        session.AssertObserved(nameof(IDatabase.SortedSetDecrementAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetUpdate))]
    public void Given_redis_When_SortedSetUpdate_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var updated = session.Database.SortedSetUpdate(key, "a", 9);
        updated.ShouldBeTrue();
        session.Raw.SortedSetScore(key, "a").ShouldBe(9);
        session.AssertObserved(nameof(IDatabase.SortedSetUpdate));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetUpdateAsync))]
    public async Task Given_redis_When_SortedSetUpdateAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var updated = await session.Database.SortedSetUpdateAsync(key, "a", 9);
        updated.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SortedSetUpdateAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetPop))]
    public void Given_redis_When_SortedSetPop_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var entry = session.Database.SortedSetPop(key);
        ((string?)entry!.Value.Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetPop));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetPopAsync))]
    public async Task Given_redis_When_SortedSetPopAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var entry = await session.Database.SortedSetPopAsync(key);
        ((string?)entry!.Value.Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetPopAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMember))]
    public void Given_redis_When_SortedSetRandomMember_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var member = session.Database.SortedSetRandomMember(key);
        ((string?)member).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMember));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMemberAsync))]
    public async Task Given_redis_When_SortedSetRandomMemberAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var member = await session.Database.SortedSetRandomMemberAsync(key);
        ((string?)member).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMemberAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMembers))]
    public void Given_redis_When_SortedSetRandomMembers_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var members = session.Database.SortedSetRandomMembers(key, 1);
        members.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMembers));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMembersAsync))]
    public async Task Given_redis_When_SortedSetRandomMembersAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var members = await session.Database.SortedSetRandomMembersAsync(key, 1);
        members.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMembersAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMembersWithScores))]
    public void Given_redis_When_SortedSetRandomMembersWithScores_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var entries = session.Database.SortedSetRandomMembersWithScores(key, 1);
        entries.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMembersWithScores));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRandomMembersWithScoresAsync))]
    public async Task Given_redis_When_SortedSetRandomMembersWithScoresAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var entries = await session.Database.SortedSetRandomMembersWithScoresAsync(key, 1);
        entries.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SortedSetRandomMembersWithScoresAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByRank))]
    public void Given_redis_When_SortedSetRangeByRank_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetRangeByRank(key);
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByRank));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByRankAsync))]
    public async Task Given_redis_When_SortedSetRangeByRankAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = await session.Database.SortedSetRangeByRankAsync(key);
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByRankAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByRankWithScores))]
    public void Given_redis_When_SortedSetRangeByRankWithScores_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetRangeByRankWithScores(key);
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByRankWithScores));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByRankWithScoresAsync))]
    public async Task Given_redis_When_SortedSetRangeByRankWithScoresAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = await session.Database.SortedSetRangeByRankWithScoresAsync(key);
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByRankWithScoresAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByScore))]
    public void Given_redis_When_SortedSetRangeByScore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetRangeByScore(key, 0, 2);
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByScore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByScoreAsync))]
    public async Task Given_redis_When_SortedSetRangeByScoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = await session.Database.SortedSetRangeByScoreAsync(key, 0, 2);
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByScoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByScoreWithScores))]
    public void Given_redis_When_SortedSetRangeByScoreWithScores_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetRangeByScoreWithScores(key, 0, 2);
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByScoreWithScores));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByScoreWithScoresAsync))]
    public async Task Given_redis_When_SortedSetRangeByScoreWithScoresAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = await session.Database.SortedSetRangeByScoreWithScoresAsync(key, 0, 2);
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByScoreWithScoresAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByValue))]
    public void Given_redis_When_SortedSetRangeByValue_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetRangeByValue(key, "a", "a");
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByValue));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeByValueAsync))]
    public async Task Given_redis_When_SortedSetRangeByValueAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = await session.Database.SortedSetRangeByValueAsync(key, "a", "a");
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetRangeByValueAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeAndStore))]
    public void Given_redis_When_SortedSetRangeAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("zd");
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = session.Database.SortedSetRangeAndStore(key, dest, 0, 0);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRangeAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRangeAndStoreAsync))]
    public async Task Given_redis_When_SortedSetRangeAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("zd");
        session.Raw.SortedSetAdd(key, "a", 1);
        var length = await session.Database.SortedSetRangeAndStoreAsync(key, dest, 0, 0);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRangeAndStoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByRank))]
    public void Given_redis_When_SortedSetRemoveRangeByRank_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, [new SortedSetEntry("a", 1), new SortedSetEntry("b", 2)]);
        var removed = session.Database.SortedSetRemoveRangeByRank(key, 0, 0);
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByRank));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByRankAsync))]
    public async Task Given_redis_When_SortedSetRemoveRangeByRankAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, [new SortedSetEntry("a", 1), new SortedSetEntry("b", 2)]);
        var removed = await session.Database.SortedSetRemoveRangeByRankAsync(key, 0, 0);
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByRankAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByScore))]
    public void Given_redis_When_SortedSetRemoveRangeByScore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = session.Database.SortedSetRemoveRangeByScore(key, 0, 2);
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByScore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByScoreAsync))]
    public async Task Given_redis_When_SortedSetRemoveRangeByScoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = await session.Database.SortedSetRemoveRangeByScoreAsync(key, 0, 2);
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByScoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByValue))]
    public void Given_redis_When_SortedSetRemoveRangeByValue_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = session.Database.SortedSetRemoveRangeByValue(key, "a", "a");
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByValue));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetRemoveRangeByValueAsync))]
    public async Task Given_redis_When_SortedSetRemoveRangeByValueAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var removed = await session.Database.SortedSetRemoveRangeByValueAsync(key, "a", "a");
        removed.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetRemoveRangeByValueAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombine))]
    public void Given_redis_When_SortedSetCombine_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var values = session.Database.SortedSetCombine(SetOperation.Union, [key, other]);
        values.Length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombine));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombineAsync))]
    public async Task Given_redis_When_SortedSetCombineAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var values = await session.Database.SortedSetCombineAsync(SetOperation.Union, [key, other]);
        values.Length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombineAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombineWithScores))]
    public void Given_redis_When_SortedSetCombineWithScores_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var values = session.Database.SortedSetCombineWithScores(SetOperation.Union, [key, other]);
        values.Length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombineWithScores));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombineWithScoresAsync))]
    public async Task Given_redis_When_SortedSetCombineWithScoresAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var values = await session.Database.SortedSetCombineWithScoresAsync(SetOperation.Union, [key, other]);
        values.Length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombineWithScoresAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombineAndStore))]
    public void Given_redis_When_SortedSetCombineAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        var dest = NewKey("zd");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var length = session.Database.SortedSetCombineAndStore(SetOperation.Union, dest, key, other);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombineAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetCombineAndStoreAsync))]
    public async Task Given_redis_When_SortedSetCombineAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        var dest = NewKey("zd");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "b", 2);
        var length = await session.Database.SortedSetCombineAndStoreAsync(SetOperation.Union, dest, key, other);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SortedSetCombineAndStoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetIntersectionLength))]
    public void Given_redis_When_SortedSetIntersectionLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "a", 2);
        var length = session.Database.SortedSetIntersectionLength([key, other]);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetIntersectionLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetIntersectionLengthAsync))]
    public async Task Given_redis_When_SortedSetIntersectionLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("z2");
        session.Raw.SortedSetAdd(key, "a", 1);
        session.Raw.SortedSetAdd(other, "a", 2);
        var length = await session.Database.SortedSetIntersectionLengthAsync([key, other]);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SortedSetIntersectionLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScan))]
    public void Given_redis_When_SortedSetScan_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        var values = session.Database.SortedSetScan(key).ToArray();
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetScan));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SortedSetScanAsync))]
    public async Task Given_redis_When_SortedSetScanAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SortedSetAdd(key, "a", 1);
        List<SortedSetEntry> values = new();
        await foreach (var value in session.Database.SortedSetScanAsync(key))
        {
            values.Add(value);
        }
        ((string?)values[0].Element).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SortedSetScanAsync));
    }
}
