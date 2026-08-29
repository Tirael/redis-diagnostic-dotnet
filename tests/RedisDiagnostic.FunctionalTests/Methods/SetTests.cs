namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class SetTests : RedisTestBase
{
    public SetTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetAdd))]
    public void Given_redis_When_SetAdd_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = session.Database.SetAdd(key, "a");
        added.ShouldBeTrue();
        session.Raw.SetContains(key, "a").ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetAdd));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetAddAsync))]
    public async Task Given_redis_When_SetAddAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var added = await session.Database.SetAddAsync(key, "a");
        added.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetAddAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetContains))]
    public void Given_redis_When_SetContains_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var contains = session.Database.SetContains(key, "a");
        contains.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetContains));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetContainsAsync))]
    public async Task Given_redis_When_SetContainsAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var contains = await session.Database.SetContainsAsync(key, "a");
        contains.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetContainsAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRemove))]
    public void Given_redis_When_SetRemove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var removed = session.Database.SetRemove(key, "a");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetRemove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRemoveAsync))]
    public async Task Given_redis_When_SetRemoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var removed = await session.Database.SetRemoveAsync(key, "a");
        removed.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetRemoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetLength))]
    public void Given_redis_When_SetLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var length = session.Database.SetLength(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SetLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetLengthAsync))]
    public async Task Given_redis_When_SetLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var length = await session.Database.SetLengthAsync(key);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SetLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetMembers))]
    public void Given_redis_When_SetMembers_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var members = session.Database.SetMembers(key);
        ((string?)members[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetMembers));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetMembersAsync))]
    public async Task Given_redis_When_SetMembersAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var members = await session.Database.SetMembersAsync(key);
        ((string?)members[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetMembersAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetPop))]
    public void Given_redis_When_SetPop_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var value = session.Database.SetPop(key);
        ((string?)value).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetPop));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetPopAsync))]
    public async Task Given_redis_When_SetPopAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var value = await session.Database.SetPopAsync(key);
        ((string?)value).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetPopAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRandomMember))]
    public void Given_redis_When_SetRandomMember_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var value = session.Database.SetRandomMember(key);
        ((string?)value).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetRandomMember));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRandomMemberAsync))]
    public async Task Given_redis_When_SetRandomMemberAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var value = await session.Database.SetRandomMemberAsync(key);
        ((string?)value).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetRandomMemberAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRandomMembers))]
    public void Given_redis_When_SetRandomMembers_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var values = session.Database.SetRandomMembers(key, 1);
        values.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SetRandomMembers));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetRandomMembersAsync))]
    public async Task Given_redis_When_SetRandomMembersAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var values = await session.Database.SetRandomMembersAsync(key, 1);
        values.Should().ContainSingle();
        session.AssertObserved(nameof(IDatabase.SetRandomMembersAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetMove))]
    public void Given_redis_When_SetMove_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("sd");
        session.Raw.SetAdd(key, "a");
        var moved = session.Database.SetMove(key, dest, "a");
        moved.ShouldBeTrue();
        session.Raw.SetContains(dest, "a").ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetMove));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetMoveAsync))]
    public async Task Given_redis_When_SetMoveAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var dest = NewKey("sd");
        session.Raw.SetAdd(key, "a");
        var moved = await session.Database.SetMoveAsync(key, dest, "a");
        moved.ShouldBeTrue();
        session.AssertObserved(nameof(IDatabase.SetMoveAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetCombine))]
    public void Given_redis_When_SetCombine_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "a");
        var values = session.Database.SetCombine(SetOperation.Union, key, other);
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetCombine));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetCombineAsync))]
    public async Task Given_redis_When_SetCombineAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "b");
        var values = await session.Database.SetCombineAsync(SetOperation.Union, key, other);
        values.Length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SetCombineAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetCombineAndStore))]
    public void Given_redis_When_SetCombineAndStore_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        var dest = NewKey("sd");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "b");
        var length = session.Database.SetCombineAndStore(SetOperation.Union, dest, key, other);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SetCombineAndStore));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetCombineAndStoreAsync))]
    public async Task Given_redis_When_SetCombineAndStoreAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        var dest = NewKey("sd");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "b");
        var length = await session.Database.SetCombineAndStoreAsync(SetOperation.Union, dest, key, other);
        length.ShouldBe(2);
        session.AssertObserved(nameof(IDatabase.SetCombineAndStoreAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetIntersectionLength))]
    public void Given_redis_When_SetIntersectionLength_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "a");
        var length = session.Database.SetIntersectionLength([key, other]);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SetIntersectionLength));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetIntersectionLengthAsync))]
    public async Task Given_redis_When_SetIntersectionLengthAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        var other = NewKey("s2");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "a");
        var length = await session.Database.SetIntersectionLengthAsync([key, other]);
        length.ShouldBe(1);
        session.AssertObserved(nameof(IDatabase.SetIntersectionLengthAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetScan))]
    public void Given_redis_When_SetScan_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        var values = session.Database.SetScan(key).ToArray();
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetScan));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetScanAsync))]
    public async Task Given_redis_When_SetScanAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();
        var key = NewKey();

        // Act / Assert
        session.Raw.SetAdd(key, "a");
        List<RedisValue> values = new();
        await foreach (var value in session.Database.SetScanAsync(key))
        {
            values.Add(value);
        }
        ((string?)values[0]).ShouldBe("a");
        session.AssertObserved(nameof(IDatabase.SetScanAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.SetCombineLength))]
    public void Given_redis_When_SetCombineLength_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = NewKey();
        var other = NewKey("s2");
        session.Raw.SetAdd(key, "a");
        session.Raw.SetAdd(other, "a");
        session.InvokeAndObserve(
            nameof(IDatabase.SetCombineLength),
            () => session.Database.SetCombineLength(SetOperation.Union, [key, other]));
    }
}
