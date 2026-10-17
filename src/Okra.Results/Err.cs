namespace Okra.Results;

public readonly record struct Err<T>(T Error)
{
    public static implicit operator T(Err<T> err) => err.Error;
    public static implicit operator Err<T>(T value) => new(value);
}