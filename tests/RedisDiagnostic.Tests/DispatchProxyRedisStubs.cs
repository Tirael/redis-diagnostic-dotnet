namespace RedisDiagnostic.Tests;

internal class ConnectionMultiplexerStub : DispatchProxy
{
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        ArgumentNullException.ThrowIfNull(targetMethod);
        return targetMethod.Name switch
        {
            nameof(IConnectionMultiplexer.GetDatabase) => DispatchProxy.Create<IDatabase, DatabaseStub>(),
            _ => DispatchProxyDefaults.GetDefault(targetMethod.ReturnType),
        };
    }
}

internal class DatabaseStub : DispatchProxy
{
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        ArgumentNullException.ThrowIfNull(targetMethod);
        return DispatchProxyDefaults.GetDefault(targetMethod.ReturnType);
    }
}

internal static class DispatchProxyDefaults
{
    public static object? GetDefault(Type type) => type switch
    {
        var t when t == typeof(void) => null,
        var t when t == typeof(Task) => Task.CompletedTask,
        { IsGenericType: true } t when t.GetGenericTypeDefinition() == typeof(Task<>) => FromResult(t),
        { IsValueType: true } => Activator.CreateInstance(type),
        _ => null,
    };

    private static object? FromResult(Type taskType)
    {
        var resultType = taskType.GetGenericArguments()[0];
        var result = resultType.IsValueType ? Activator.CreateInstance(resultType) : null;
        return typeof(Task)
            .GetMethod(nameof(Task.FromResult))!
            .MakeGenericMethod(resultType)
            .Invoke(null, [result]);
    }
}
