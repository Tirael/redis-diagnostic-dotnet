namespace RedisDiagnostic.SourceGenerator;

[Generator]
public sealed class InstrumentedDatabaseGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, static (productionContext, compilation) =>
        {
            var databaseType = compilation.GetTypeByMetadataName("StackExchange.Redis.IDatabase");
            if (databaseType is null)
                return;

            productionContext.AddSource("InstrumentedDatabase.g.cs", InstrumentedDatabaseSourceWriter.Write(databaseType));
        });
    }
}
