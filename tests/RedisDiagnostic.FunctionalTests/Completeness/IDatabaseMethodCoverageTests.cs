using System.Reflection;
using RedisDiagnostic.FunctionalTests.Infrastructure;
using StackExchange.Redis;

namespace RedisDiagnostic.FunctionalTests.Completeness;

public sealed class IDatabaseMethodCoverageTests
{
    [Fact]
    public void All_IDatabase_methods_have_a_functional_test()
    {
        var required = typeof(IDatabase)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(method => !method.IsSpecialName)
            .Select(method => method.Name)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        var covered = typeof(IDatabaseMethodCoverageTests).Assembly
            .GetTypes()
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            .SelectMany(method => method.GetCustomAttributes<RedisMethodAttribute>())
            .Select(attribute => attribute.MethodName)
            .ToHashSet(StringComparer.Ordinal);

        var missing = required.Where(name => !covered.Contains(name)).ToArray();

        Assert.True(
            missing.Length == 0,
            "Missing functional tests for IDatabase methods: " + string.Join(", ", missing));
    }
}
