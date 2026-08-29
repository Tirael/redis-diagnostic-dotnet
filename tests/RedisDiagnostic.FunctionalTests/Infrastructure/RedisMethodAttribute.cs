namespace RedisDiagnostic.FunctionalTests.Infrastructure;

[AttributeUsage(AttributeTargets.Method)]
public sealed class RedisMethodAttribute : Attribute
{
    public RedisMethodAttribute(string methodName)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        MethodName = methodName;
    }

    public string MethodName { get; }
}
