namespace RedisDiagnostic.FunctionalTests.Infrastructure;

[CollectionDefinition(Name)]
public sealed class RedisCollection : ICollectionFixture<RedisFixture>
{
    public const string Name = "Redis";
}
