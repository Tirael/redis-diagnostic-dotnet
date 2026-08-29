namespace RedisDiagnostic;

/// <summary>
/// Records <c>IDatabase</c> call duration as a Prometheus histogram.
/// </summary>
public sealed class PrometheusRedisMethodMetrics : IRedisMethodMetrics
{
    /// <summary>Prometheus metric name for method duration.</summary>
    public const string MetricName = "redis_idatabase_method_duration_seconds";

    /// <summary>Prometheus help text for the duration histogram.</summary>
    public const string MetricHelp = "Duration of IDatabase method calls in seconds.";

    /// <summary>Label for the IDatabase method name.</summary>
    public const string LabelMethod = "method";

    /// <summary>Label for call outcome.</summary>
    public const string LabelResult = "result";

    /// <summary>Successful call outcome.</summary>
    public const string ResultOk = "ok";

    /// <summary>Failed call outcome.</summary>
    public const string ResultError = "error";

    private readonly Histogram _duration;
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Creates metrics that write into the given registry.
    /// </summary>
    /// <param name="registry">The collector registry to register the histogram with.</param>
    /// <param name="timeProvider">Clock used to measure elapsed time.</param>
    public PrometheusRedisMethodMetrics(CollectorRegistry registry, TimeProvider timeProvider)
    {
        ArgumentNullException.ThrowIfNull(registry);
        ArgumentNullException.ThrowIfNull(timeProvider);

        _timeProvider = timeProvider;
        HistogramConfiguration configuration = new()
        {
            LabelNames = [LabelMethod, LabelResult],
        };
        _duration = Metrics
            .WithCustomRegistry(registry)
            .CreateHistogram(MetricName, MetricHelp, configuration);
    }

    /// <inheritdoc />
    public T Measure<T>(string methodName, Func<T> action)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(action);

        var startedAt = _timeProvider.GetTimestamp();
        try
        {
            var result = action();
            Observe(methodName, ResultOk, startedAt);
            return result;
        }
        catch
        {
            Observe(methodName, ResultError, startedAt);
            throw;
        }
    }

    /// <inheritdoc />
    public void Measure(string methodName, Action action)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(action);

        var startedAt = _timeProvider.GetTimestamp();
        try
        {
            action();
            Observe(methodName, ResultOk, startedAt);
        }
        catch
        {
            Observe(methodName, ResultError, startedAt);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<T> MeasureAsync<T>(string methodName, Func<Task<T>> action)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(action);

        var startedAt = _timeProvider.GetTimestamp();
        try
        {
            var result = await action().ConfigureAwait(false);
            Observe(methodName, ResultOk, startedAt);
            return result;
        }
        catch
        {
            Observe(methodName, ResultError, startedAt);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task MeasureAsync(string methodName, Func<Task> action)
    {
        ArgumentException.ThrowIfNullOrEmpty(methodName);
        ArgumentNullException.ThrowIfNull(action);

        var startedAt = _timeProvider.GetTimestamp();
        try
        {
            await action().ConfigureAwait(false);
            Observe(methodName, ResultOk, startedAt);
        }
        catch
        {
            Observe(methodName, ResultError, startedAt);
            throw;
        }
    }

    private void Observe(string methodName, string result, long startedAt)
    {
        var elapsed = _timeProvider.GetElapsedTime(startedAt);
        _duration.WithLabels(methodName, result).Observe(elapsed.TotalSeconds);
    }
}
