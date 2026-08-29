namespace RedisDiagnostic.Host;

/// <summary>
/// Request body for string and hash write endpoints.
/// </summary>
public sealed class ItemRequest
{
    /// <summary>
    /// Gets the value to store.
    /// </summary>
    public required string Value { get; init; }
}
