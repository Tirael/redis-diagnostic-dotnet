namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class VectorSetTests : RedisTestBase
{
    public VectorSetTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetAdd))]
    public void Given_redis_When_VectorSetAdd_Then_state_and_metric_are_observed()
    {
        var session = Fixture.CreateSession();
        var key = NewKey();
        session.InvokeAndObserve(
            nameof(IDatabase.VectorSetAdd),
            () => session.Database.VectorSetAdd(key, VectorSetAddRequest.Member("m", new float[] { 1f, 0f })));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetLength))]
    public void Given_redis_When_VectorSetLength_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetLength), (database, key) => database.VectorSetLength(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetDimension))]
    public void Given_redis_When_VectorSetDimension_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetDimension), (database, key) => database.VectorSetDimension(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetGetApproximateVector))]
    public void Given_redis_When_VectorSetGetApproximateVector_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(
            nameof(IDatabase.VectorSetGetApproximateVector),
            (database, key) => database.VectorSetGetApproximateVector(key, "m")?.Dispose());
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetGetAttributesJson))]
    public void Given_redis_When_VectorSetGetAttributesJson_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetGetAttributesJson), (database, key) => database.VectorSetGetAttributesJson(key, "m"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetInfo))]
    public void Given_redis_When_VectorSetInfo_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetInfo), (database, key) => database.VectorSetInfo(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetContains))]
    public void Given_redis_When_VectorSetContains_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetContains), (database, key) => database.VectorSetContains(key, "m"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetGetLinks))]
    public void Given_redis_When_VectorSetGetLinks_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetGetLinks), (database, key) => database.VectorSetGetLinks(key, "m"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetGetLinksWithScores))]
    public void Given_redis_When_VectorSetGetLinksWithScores_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetGetLinksWithScores), (database, key) => database.VectorSetGetLinksWithScores(key, "m"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetRandomMember))]
    public void Given_redis_When_VectorSetRandomMember_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetRandomMember), (database, key) => database.VectorSetRandomMember(key));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetRandomMembers))]
    public void Given_redis_When_VectorSetRandomMembers_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetRandomMembers), (database, key) => database.VectorSetRandomMembers(key, 1));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetRemove))]
    public void Given_redis_When_VectorSetRemove_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(nameof(IDatabase.VectorSetRemove), (database, key) => database.VectorSetRemove(key, "m"));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetSetAttributesJson))]
    public void Given_redis_When_VectorSetSetAttributesJson_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(
            nameof(IDatabase.VectorSetSetAttributesJson),
            (database, key) => database.VectorSetSetAttributesJson(key, "m", """{"k":"v"}"""));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetSimilaritySearch))]
    public void Given_redis_When_VectorSetSimilaritySearch_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(
            nameof(IDatabase.VectorSetSimilaritySearch),
            (database, key) => database.VectorSetSimilaritySearch(key, VectorSetSimilaritySearchRequest.ByMember("m")));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetRange))]
    public void Given_redis_When_VectorSetRange_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(
            nameof(IDatabase.VectorSetRange),
            (database, key) => database.VectorSetRange(key, "-", "+", -1)?.Dispose());
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.VectorSetRangeEnumerate))]
    public void Given_redis_When_VectorSetRangeEnumerate_Then_state_and_metric_are_observed()
    {
        InvokeAfterAdd(
            nameof(IDatabase.VectorSetRangeEnumerate),
            (database, key) =>
            {
                foreach (var member in database.VectorSetRangeEnumerate(key, "-", "+", 10))
                    _ = member;
            });
    }

    private void InvokeAfterAdd(string methodName, Action<IDatabase, RedisKey> action)
    {
        var session = Fixture.CreateSession();
        var key = NewKey();
        try
        {
            session.Raw.VectorSetAdd(key, VectorSetAddRequest.Member("m", new float[] { 1f, 0f }));
        }
        catch (RedisServerException exception)
        {
            _ = exception;
        }

        session.InvokeAndObserve(methodName, () => action(session.Database, key));
    }
}
