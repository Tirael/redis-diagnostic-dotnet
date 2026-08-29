namespace RedisDiagnostic.SourceGenerator;

[Generator]
public sealed class InstrumentedDatabaseGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, static (productionContext, compilation) =>
        {
            if (compilation.GetTypeByMetadataName("StackExchange.Redis.IDatabase") is not { } databaseType)
                return;

            productionContext.AddSource("InstrumentedDatabase.g.cs", InstrumentedDatabaseSourceWriter.Write(databaseType));
        });
    }
}
