namespace RedisDiagnostic.FunctionalTests.Methods;

[Collection(RedisCollection.Name)]
public sealed class ScriptTests : RedisTestBase
{
    public ScriptTests(RedisFixture fixture) : base(fixture)
    {
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluate))]
    public void Given_redis_When_ScriptEvaluate_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = session.Database.ScriptEvaluate("return 7");
        ((int)result).ShouldBe(7);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluate));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateAsync))]
    public async Task Given_redis_When_ScriptEvaluateAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = await session.Database.ScriptEvaluateAsync("return 7");
        ((int)result).ShouldBe(7);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateAsync));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateReadOnly))]
    public void Given_redis_When_ScriptEvaluateReadOnly_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = session.Database.ScriptEvaluateReadOnly("return 3");
        ((int)result).ShouldBe(3);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateReadOnly));
    }

    [Fact]
    [RedisMethod(nameof(IDatabase.ScriptEvaluateReadOnlyAsync))]
    public async Task Given_redis_When_ScriptEvaluateReadOnlyAsync_Then_state_and_metric_are_observed()
    {
        // Arrange
        var session = Fixture.CreateSession();

        // Act / Assert
        var result = await session.Database.ScriptEvaluateReadOnlyAsync("return 3");
        ((int)result).ShouldBe(3);
        session.AssertObserved(nameof(IDatabase.ScriptEvaluateReadOnlyAsync));
    }
}
