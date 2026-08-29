using Microsoft.Extensions.Time.Testing;
using Prometheus;

namespace RedisDiagnostic.Tests;

public sealed class PrometheusRedisMethodMetricsTests
{
    [Fact]
    public void Measure_WhenActionSucceeds_RecordsOkDuration()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);

        var result = sut.Measure("StringGet", () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(25));
            return 42;
        });

        Assert.Equal(42, result);
        AssertObservation(registry, "StringGet", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(25));
    }

    [Fact]
    public void Measure_WhenActionThrows_RecordsErrorAndRethrows()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);

        var thrown = Assert.Throws<InvalidOperationException>(() =>
            sut.Measure("StringSet", () =>
            {
                timeProvider.Advance(TimeSpan.FromMilliseconds(10));
                throw new InvalidOperationException("boom");
            }));

        Assert.Equal("boom", thrown.Message);
        AssertObservation(registry, "StringSet", PrometheusRedisMethodMetrics.ResultError, TimeSpan.FromMilliseconds(10));
    }

    [Fact]
    public void Measure_WhenActionIsVoidAndSucceeds_RecordsOk()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);
        var invoked = false;

        sut.Measure("KeyRestore", () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(5));
            invoked = true;
        });

        Assert.True(invoked);
        AssertObservation(registry, "KeyRestore", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(5));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskSucceeds_RecordsOkDuration()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);

        var result = await sut.MeasureAsync("StringGetAsync", async () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(40));
            await Task.CompletedTask;
            return "value";
        });

        Assert.Equal("value", result);
        AssertObservation(registry, "StringGetAsync", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(40));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskThrows_RecordsErrorAndRethrows()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);

        var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            sut.MeasureAsync("PingAsync", async () =>
            {
                timeProvider.Advance(TimeSpan.FromMilliseconds(15));
                await Task.CompletedTask;
                throw new InvalidOperationException("async-boom");
            }));

        Assert.Equal("async-boom", thrown.Message);
        AssertObservation(registry, "PingAsync", PrometheusRedisMethodMetrics.ResultError, TimeSpan.FromMilliseconds(15));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskIsNonGenericAndSucceeds_RecordsOk()
    {
        var timeProvider = new FakeTimeProvider();
        var registry = Metrics.NewCustomRegistry();
        var sut = new PrometheusRedisMethodMetrics(registry, timeProvider);

        await sut.MeasureAsync("WaitAsync", async () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(8));
            await Task.CompletedTask;
        });

        AssertObservation(registry, "WaitAsync", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(8));
    }

    private static void AssertObservation(
        CollectorRegistry registry,
        string methodName,
        string result,
        TimeSpan expectedDuration)
    {
        var histogram = Metrics
            .WithCustomRegistry(registry)
            .CreateHistogram(
                PrometheusRedisMethodMetrics.MetricName,
                PrometheusRedisMethodMetrics.MetricHelp,
                new HistogramConfiguration
                {
                    LabelNames = [PrometheusRedisMethodMetrics.LabelMethod, PrometheusRedisMethodMetrics.LabelResult],
                });

        var child = histogram.WithLabels(methodName, result);
        Assert.Equal(1, child.Count);
        Assert.Equal(expectedDuration.TotalSeconds, child.Sum, precision: 9);
    }
}
