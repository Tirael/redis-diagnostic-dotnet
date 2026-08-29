using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using RedisDiagnostic.SourceGenerator;

namespace RedisDiagnostic.Tests;

public sealed class InstrumentedDatabaseGeneratorTests
{
    [Fact]
    public async Task Generator_WhenIDatabaseStubIsPresent_EmitsAllMethodNamesAndCompiles()
    {
        const string source = """
            using System;
            using System.Threading.Tasks;

            namespace StackExchange.Redis
            {
                public interface IDatabase
                {
                    int Database { get; }
                    IConnectionMultiplexer Multiplexer { get; }
                    IBatch CreateBatch(object? asyncState = null);
                    ITransaction CreateTransaction(object? asyncState = null);
                    bool StringSet(RedisKey key, RedisValue value);
                    Task<RedisValue> StringGetAsync(RedisKey key, CommandFlags flags = CommandFlags.None);
                    void KeyRestore(RedisKey key, byte[] value);
                }

                public interface IConnectionMultiplexer { }
                public interface IBatch { }
                public interface ITransaction { }
                public struct RedisKey { }
                public struct RedisValue { }
                public enum CommandFlags { None = 0 }
            }

            namespace RedisDiagnostic
            {
                public interface IRedisMethodMetrics
                {
                    T Measure<T>(string methodName, Func<T> action);
                    void Measure(string methodName, Action action);
                    Task<T> MeasureAsync<T>(string methodName, Func<Task<T>> action);
                    Task MeasureAsync(string methodName, Func<Task> action);
                }
            }
            """;

        var test = new CSharpSourceGeneratorTest<InstrumentedDatabaseGenerator, DefaultVerifier>
        {
            TestBehaviors = TestBehaviors.SkipGeneratedSourcesCheck,
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
            TestState =
            {
                Sources = { source },
            },
        };

        await test.RunAsync();

        var generated = RunGenerator(source);
        Assert.Contains("public sealed class InstrumentedDatabase : global::StackExchange.Redis.IDatabase", generated, StringComparison.Ordinal);
        Assert.Contains("StringSet", generated, StringComparison.Ordinal);
        Assert.Contains("StringGetAsync", generated, StringComparison.Ordinal);
        Assert.Contains("KeyRestore", generated, StringComparison.Ordinal);
        Assert.Contains("CreateBatch", generated, StringComparison.Ordinal);
        Assert.Contains("CreateTransaction", generated, StringComparison.Ordinal);
        Assert.Contains("Database", generated, StringComparison.Ordinal);
        Assert.Contains("Multiplexer", generated, StringComparison.Ordinal);
        Assert.Contains("_metrics.Measure(nameof(StringSet)", generated, StringComparison.Ordinal);
        Assert.Contains("_metrics.MeasureAsync(nameof(StringGetAsync)", generated, StringComparison.Ordinal);
        Assert.DoesNotContain("_metrics.Measure(nameof(CreateBatch)", generated, StringComparison.Ordinal);
    }

    private static string RunGenerator(string source)
    {
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));

        var compilation = CSharpCompilation.Create(
            "generator-test",
            [CSharpSyntaxTree.ParseText(source)],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var generator = new InstrumentedDatabaseGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));

        var result = driver.GetRunResult();
        Assert.Single(result.GeneratedTrees);
        return result.GeneratedTrees[0].GetText().ToString();
    }
}
