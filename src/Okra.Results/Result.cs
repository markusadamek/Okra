namespace Okra.Results;

public union Result<T,E>(Ok<T>, Err<T>);