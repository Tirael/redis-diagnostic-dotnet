namespace RedisDiagnostic;

/// <summary>
/// Measures duration of <c>IDatabase</c> method calls for diagnostics.
/// </summary>
public interface IRedisMethodMetrics
{
    /// <summary>
    /// Measures a synchronous method that returns a value.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    /// <param name="methodName">The <c>IDatabase</c> method name.</param>
    /// <param name="action">The inner call.</param>
    /// <returns>The inner result.</returns>
    T Measure<T>(string methodName, Func<T> action);

    /// <summary>
    /// Measures a synchronous method that returns no value.
    /// </summary>
    /// <param name="methodName">The <c>IDatabase</c> method name.</param>
    /// <param name="action">The inner call.</param>
    void Measure(string methodName, Action action);

    /// <summary>
    /// Measures an asynchronous method that returns a value.
    /// </summary>
    /// <typeparam name="T">The task result type.</typeparam>
    /// <param name="methodName">The <c>IDatabase</c> method name.</param>
    /// <param name="action">The inner call.</param>
    /// <returns>The inner task result.</returns>
    Task<T> MeasureAsync<T>(string methodName, Func<Task<T>> action);

    /// <summary>
    /// Measures an asynchronous method that returns no value.
    /// </summary>
    /// <param name="methodName">The <c>IDatabase</c> method name.</param>
    /// <param name="action">The inner call.</param>
    /// <returns>A task that completes when the inner call completes.</returns>
    Task MeasureAsync(string methodName, Func<Task> action);
}
