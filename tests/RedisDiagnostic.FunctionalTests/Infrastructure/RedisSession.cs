using Prometheus;

namespace RedisDiagnostic.FunctionalTests.Infrastructure;

public sealed class RedisSession
{
    public RedisSession(IDatabase database, IDatabase raw, CollectorRegistry registry)
    {
        Database = database;
        Raw = raw;
        Registry = registry;
    }

    public IDatabase Database { get; }

    public IDatabase Raw { get; }

    public CollectorRegistry Registry { get; }

    public void AssertObserved(string methodName, string result = PrometheusRedisMethodMetrics.ResultOk)
    {
        var histogram = Metrics
            .WithCustomRegistry(Registry)
            .CreateHistogram(
                PrometheusRedisMethodMetrics.MetricName,
                PrometheusRedisMethodMetrics.MetricHelp,
                new HistogramConfiguration
                {
                    LabelNames = [PrometheusRedisMethodMetrics.LabelMethod, PrometheusRedisMethodMetrics.LabelResult],
                });

        Assert.True(
            histogram.WithLabels(methodName, result).Count >= 1,
            $"Expected a histogram observation for method={methodName}, result={result}.");
    }

    public void AssertNotObserved(string methodName)
    {
        var histogram = Metrics
            .WithCustomRegistry(Registry)
            .CreateHistogram(
                PrometheusRedisMethodMetrics.MetricName,
                PrometheusRedisMethodMetrics.MetricHelp,
                new HistogramConfiguration
                {
                    LabelNames = [PrometheusRedisMethodMetrics.LabelMethod, PrometheusRedisMethodMetrics.LabelResult],
                });

        Assert.Equal(0, histogram.WithLabels(methodName, PrometheusRedisMethodMetrics.ResultOk).Count);
        Assert.Equal(0, histogram.WithLabels(methodName, PrometheusRedisMethodMetrics.ResultError).Count);
    }
}
