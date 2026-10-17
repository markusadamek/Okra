namespace Okra.Results;

/// <summary>
/// Represents an error result in the Okra result pattern.
/// </summary>
/// <typeparam name="T">The type of the error value.</typeparam>
/// <param name="Error">The error value.</param>
public readonly record struct Err<T>(T Error)
{
    /// <summary>
    /// Converts an <see cref="Err{T}"/> to its underlying error value of type <typeparamref name="T"/>.
    /// </summary>
    public static implicit operator T(Err<T> err) => err.Error;

    /// <summary>
    /// Converts a value of type <typeparamref name="T"/> to an <see cref="Err{T}"/>.
    /// </summary>
    public static implicit operator Err<T>(T value) => new(value);
}