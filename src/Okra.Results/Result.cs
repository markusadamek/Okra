namespace Okra.Results;

public union Result<T, TE>(Ok<T>, Err<TE>)
{
    public bool IsOk => Value is Ok<T>;
    public bool IsErr => Value is Err<TE>;

    public T Unwrap(string? message = null)
    {
        return Value switch
               {
                   Ok<T> ok => ok.Value,
                   _ => throw new InvalidOperationException(message)
               };
    }

    public T Unwrap(Exception ex)
    {
        return Value switch
               {
                   Ok<T> ok => ok.Value,
                   _ => throw ex,
               };
    }

    public static implicit operator Result<T, TE>(T okValue) => okValue.AsOk();
    public static implicit operator Result<T, TE>(TE errorValue) => errorValue.AsError();

    public Result<T, TNewError> MapErr<TNewError>(Func<TE, TNewError> func)
    {
        return Value switch
               {
                   Ok<T> ok => ok,
                   Err<TE> err => func(err)
               };
    }

    public Result<TNew, TE> MapOk<TNew>(Func<T, TNew> func)
    {
        return Value switch
               {
                   Ok<T> ok => func(ok),
                   Err<TE> err => err
               };
    }
}