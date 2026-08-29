namespace RedisDiagnostic.FunctionalTests.Infrastructure;

[Collection(RedisCollection.Name)]
public abstract class RedisTestBase
{
    protected RedisTestBase(RedisFixture fixture)
    {
        Fixture = fixture;
    }

    protected RedisFixture Fixture { get; }

    protected static string NewKey(string suffix = "k") => RedisFixture.NewKey(suffix);
}
