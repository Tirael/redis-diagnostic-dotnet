namespace RedisDiagnostic.FunctionalTests.Infrastructure;

public sealed class RedisDurationHistogram
{
    private readonly Histogram _histogram;

    public RedisDurationHistogram(CollectorRegistry registry)
    {
        HistogramConfiguration configuration = new()
        {
            LabelNames = [PrometheusRedisMethodMetrics.LabelMethod, PrometheusRedisMethodMetrics.LabelResult],
        };
        _histogram = Metrics
            .WithCustomRegistry(registry)
            .CreateHistogram(
                PrometheusRedisMethodMetrics.MetricName,
                PrometheusRedisMethodMetrics.MetricHelp,
                configuration);
    }

    public void AssertObserved(string methodName, string result = PrometheusRedisMethodMetrics.ResultOk) =>
        _histogram.WithLabels(methodName, result).Count.Should().BeGreaterThanOrEqualTo(
            1,
            because: "a histogram observation for method={0}, result={1} is expected",
            methodName,
            result);

    public void AssertNotObserved(string methodName)
    {
        _histogram.WithLabels(methodName, PrometheusRedisMethodMetrics.ResultOk).Count.ShouldBe(0);
        _histogram.WithLabels(methodName, PrometheusRedisMethodMetrics.ResultError).Count.ShouldBe(0);
    }
}
