namespace Okra.Results;

public readonly record struct Ok<T>(T Value)
{
    public static implicit operator T(Ok<T> ok) => ok.Value;
    public static implicit operator Ok<T>(T value) => new(value);
}