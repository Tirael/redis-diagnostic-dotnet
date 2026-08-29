namespace RedisDiagnostic.Tests;

internal class ConnectionMultiplexerStub : DispatchProxy
{
    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        ArgumentNullException.ThrowIfNull(targetMethod);
        if (targetMethod.Name is not nameof(IConnectionMultiplexer.GetDatabase))
        {
            return DispatchProxyDefaults.GetDefault(targetMethod.ReturnType);
        }

        return DispatchProxy.Create<IDatabase, DatabaseStub>();
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
    public static object? GetDefault(Type type)
    {
        if (type == typeof(void))
        {
            return null;
        }

        if (type == typeof(Task))
        {
            return Task.CompletedTask;
        }

        if (type is not { IsGenericType: true } || type.GetGenericTypeDefinition() != typeof(Task<>))
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        var resultType = type.GetGenericArguments()[0];
        var result = resultType.IsValueType ? Activator.CreateInstance(resultType) : null;
        return typeof(Task)
            .GetMethod(nameof(Task.FromResult))!
            .MakeGenericMethod(resultType)
            .Invoke(null, [result]);
    }
}
