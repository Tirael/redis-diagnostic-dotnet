namespace RedisDiagnostic.Host;

internal static class RedisDiagnosticEndpoints
{
    public static WebApplication MapRedisDiagnosticEndpoints(this WebApplication app)
    {
        MapItemEndpoints(app);
        MapHashEndpoints(app);
        return app;
    }

    private static void MapItemEndpoints(WebApplication app)
    {
        app.MapPut("/items/{key}", async (string key, ItemRequest request, IDatabase database) =>
        {
            var written = await database.StringSetAsync(key, request.Value);
            return written ? Results.NoContent() : Results.Conflict();
        });

        app.MapGet("/items/{key}", async (string key, IDatabase database) =>
        {
            var value = await database.StringGetAsync(key);
            return value.IsNull
                ? Results.NotFound()
                : Results.Ok(new { key, value = value.ToString() });
        });

        app.MapDelete("/items/{key}", async (string key, IDatabase database) =>
        {
            var deleted = await database.KeyDeleteAsync(key);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/items/{key}/exists", async (string key, IDatabase database) =>
        {
            var exists = await database.KeyExistsAsync(key);
            return Results.Ok(new { key, exists });
        });
    }

    private static void MapHashEndpoints(WebApplication app)
    {
        app.MapPut("/hashes/{key}/{field}", async (string key, string field, ItemRequest request, IDatabase database) =>
        {
            await database.HashSetAsync(key, field, request.Value);
            return Results.NoContent();
        });

        app.MapGet("/hashes/{key}/{field}", async (string key, string field, IDatabase database) =>
        {
            var value = await database.HashGetAsync(key, field);
            return value.IsNull
                ? Results.NotFound()
                : Results.Ok(new { key, field, value = value.ToString() });
        });
    }
}
