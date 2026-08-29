var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["Redis:ConnectionString"];
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Configuration value 'Redis:ConnectionString' is required.");
}

builder.Services.AddInstrumentedRedis(connectionString);

var app = builder.Build();
app.UseMetricServer("/metrics");
app.MapRedisDiagnosticEndpoints();
await app.RunAsync();
