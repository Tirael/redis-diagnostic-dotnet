namespace RedisDiagnostic.Tests;

public sealed class PrometheusRedisMethodMetricsTests
{
    [Fact]
    public void Measure_WhenActionSucceeds_RecordsOkDuration()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);

        var result = sut.Measure("StringGet", () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(25));
            return 42;
        });

        result.ShouldBe(42);
        AssertObservation(registry, "StringGet", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(25));
    }

    [Fact]
    public void Measure_WhenActionThrows_RecordsErrorAndRethrows()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);

        var thrown = Should.Throw<InvalidOperationException>(() =>
            sut.Measure("StringSet", () =>
            {
                timeProvider.Advance(TimeSpan.FromMilliseconds(10));
                throw new InvalidOperationException("boom");
            }));

        thrown.Message.ShouldBe("boom");
        AssertObservation(registry, "StringSet", PrometheusRedisMethodMetrics.ResultError, TimeSpan.FromMilliseconds(10));
    }

    [Fact]
    public void Measure_WhenActionIsVoidAndSucceeds_RecordsOk()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);
        var invoked = false;

        sut.Measure("KeyRestore", () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(5));
            invoked = true;
        });

        invoked.ShouldBeTrue();
        AssertObservation(registry, "KeyRestore", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(5));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskSucceeds_RecordsOkDuration()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);

        var result = await sut.MeasureAsync("StringGetAsync", async () =>
        {
            timeProvider.Advance(TimeSpan.FromMilliseconds(40));
            await Task.CompletedTask;
            return "value";
        });

        result.ShouldBe("value");
        AssertObservation(registry, "StringGetAsync", PrometheusRedisMethodMetrics.ResultOk, TimeSpan.FromMilliseconds(40));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskThrows_RecordsErrorAndRethrows()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);

        var thrown = await Should.ThrowAsync<InvalidOperationException>(() =>
            sut.MeasureAsync("PingAsync", async () =>
            {
                timeProvider.Advance(TimeSpan.FromMilliseconds(15));
                await Task.CompletedTask;
                throw new InvalidOperationException("async-boom");
            }));

        thrown.Message.ShouldBe("async-boom");
        AssertObservation(registry, "PingAsync", PrometheusRedisMethodMetrics.ResultError, TimeSpan.FromMilliseconds(15));
    }

    [Fact]
    public async Task MeasureAsync_WhenTaskIsNonGenericAndSucceeds_RecordsOk()
    {
        FakeTimeProvider timeProvider = new();
        var registry = Metrics.NewCustomRegistry();
        PrometheusRedisMethodMetrics sut = new(registry, timeProvider);

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
        HistogramConfiguration configuration = new()
        {
            LabelNames = [PrometheusRedisMethodMetrics.LabelMethod, PrometheusRedisMethodMetrics.LabelResult],
        };
        var histogram = Metrics
            .WithCustomRegistry(registry)
            .CreateHistogram(
                PrometheusRedisMethodMetrics.MetricName,
                PrometheusRedisMethodMetrics.MetricHelp,
                configuration);

        var child = histogram.WithLabels(methodName, result);
        child.Count.ShouldBe(1);
        child.Sum.Should().BeApproximately(expectedDuration.TotalSeconds, 1e-9);
    }
}
