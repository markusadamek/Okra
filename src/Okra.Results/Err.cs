namespace Okra.Results;

public readonly record struct Err<T>(T Error);