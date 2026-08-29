namespace RedisDiagnostic.FunctionalTests.Infrastructure;

public sealed class RedisSession
{
    public RedisSession(IDatabase database, IDatabase raw, CollectorRegistry registry)
    {
        Database = database;
        Raw = raw;
        Histogram = new(registry);
    }

    public IDatabase Database { get; }

    public IDatabase Raw { get; }

    public RedisDurationHistogram Histogram { get; }

    public void InvokeAndObserve(string methodName, Action action)
    {
        try
        {
            action();
            Histogram.AssertObserved(methodName);
        }
        catch (RedisServerException)
        {
            Histogram.AssertObserved(methodName, PrometheusRedisMethodMetrics.ResultError);
        }
    }

    public void AssertObserved(string methodName, string result = PrometheusRedisMethodMetrics.ResultOk) =>
        Histogram.AssertObserved(methodName, result);

    public void AssertNotObserved(string methodName) => Histogram.AssertNotObserved(methodName);
}
