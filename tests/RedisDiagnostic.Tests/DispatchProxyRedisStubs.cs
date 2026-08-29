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
        _ when type == typeof(void) => null,
        _ when type == typeof(Task) => Task.CompletedTask,
        _ when type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>) => FromResult(type),
        _ when type.IsValueType => Activator.CreateInstance(type),
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
