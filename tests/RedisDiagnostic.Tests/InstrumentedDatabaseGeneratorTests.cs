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
        generated.Should().Contain("public sealed class InstrumentedDatabase : global::StackExchange.Redis.IDatabase")
            .And.Contain("StringSet")
            .And.Contain("StringGetAsync")
            .And.Contain("KeyRestore")
            .And.Contain("CreateBatch")
            .And.Contain("CreateTransaction")
            .And.Contain("Database")
            .And.Contain("Multiplexer")
            .And.Contain("_metrics.Measure(nameof(StringSet)")
            .And.Contain("_metrics.MeasureAsync(nameof(StringGetAsync)")
            .And.NotContain("_metrics.Measure(nameof(CreateBatch)");
    }

    private static string RunGenerator(string source)
    {
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => assembly is { IsDynamic: false, Location: { Length: > 0 } })
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));

        var compilation = CSharpCompilation.Create(
            "generator-test",
            [CSharpSyntaxTree.ParseText(source)],
            references,
            new(OutputKind.DynamicallyLinkedLibrary));

        InstrumentedDatabaseGenerator generator = new();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);

        diagnostics.Where(diagnostic => diagnostic is { Severity: DiagnosticSeverity.Error }).ShouldBeEmpty();

        var result = driver.GetRunResult();
        result.GeneratedTrees.Should().ContainSingle();
        return result.GeneratedTrees[0].GetText().ToString();
    }
}
