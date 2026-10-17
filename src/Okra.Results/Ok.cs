namespace Okra.Results;

/// <summary>
/// Represents a successful result that carries a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the value contained in this result.</typeparam>
public readonly record struct Ok<T>(T Value) {
    /// <summary>
    /// Gets the value contained in this result.
    /// </summary>
    public T Value { get; } = Value;

    /// <summary>
    /// Converts an instance of <typeparamref name="T"/> to this result.
    /// </summary>
    public static implicit operator Ok<T>(T value) => new(value);

    /// <summary>
    /// Converts this result to an instance of <typeparamref name="T"/>.
    /// </summary>
    public static implicit operator T(Ok<T> ok) => ok.Value;
}
