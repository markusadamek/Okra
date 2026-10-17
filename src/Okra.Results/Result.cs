using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace Okra.Results;

public union Result<T, TE>(Ok<T>, Err<TE>)
{
    public bool IsOk => Value is Ok<T>;
    public bool IsErr => Value is Err<TE>;

    /// <summary>
    /// Unwraps the result, returning the contained value if it's a Ok, otherwise throws an exception.
    /// </summary>
    /// <param name="message">Optional custom exception message.</param>
    /// <returns>
    /// The contained value.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is an error.</exception>
    public T Unwrap(string? message = null)
    {
        return Value switch
        {
            Ok<T> ok => ok.Value,
            _ => throw new InvalidOperationException(message)
        };
    }

    /// <summary>
    /// Unwraps the result, returning the contained value if it's a Ok, otherwise throws the provided exception.
    /// </summary>
    /// <param name="ex">The exception to throw if the result is an error.</param>
    /// <returns>
    /// The contained value.
    /// </returns>
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

    /// <summary>
    /// Maps the error value using the provided function if the result is an error.
    /// </summary>
    /// <typeparam name="TNewError">The new error type.</typeparam>
    /// <param name="func">Function to transform the error value.</param>
    /// <returns>
    /// A new result with the mapped error if the original was an error,
    /// or the same result if it was ok.
    /// </returns>
    public Result<T, TNewError> MapErr<TNewError>(Func<TE, TNewError> func)
    {
        return Value switch
        {
            Ok<T> ok => ok,
            Err<TE> err => func(err)
        };
    }

    /// <summary>
    /// Maps the ok value using the provided function if the result is ok.
    /// </summary>
    /// <typeparam name="TNew">The new ok type.</typeparam>
    /// <param name="func">Function to transform the ok value.</param>
    /// <returns>
    /// A new result with the mapped ok if the original was ok,
    /// or the same result if it was an error.
    /// </returns>
    public Result<TNew, TE> MapOk<TNew>(Func<T, TNew> func)
    {
        return Value switch
        {
            Ok<T> ok => func(ok),
            Err<TE> err => err
        };
    }

    /// <summary>
    /// Performs a logical OR between two results, considering ok as true and err as false.
    /// </summary>
    /// <param name="x">First result.</param>
    /// <param name="y">Second result.</param>
    /// <returns>
    /// True if either x or y is ok, false otherwise.
    /// </returns>
    public static bool operator |(Result<T, TE> x, Result<T, TE> y)
    {
        return x.IsOk | y.IsOk;
    }

    /// <summary>
    /// Negates the result, treating ok as false and err as true.
    /// </summary>
    /// <param name="x">The result to negate.</param>
    /// <returns>
    /// False if x is ok, true if x is err.
    /// </returns>
    public static bool operator !(Result<T, TE> x)
    {
        return !x.IsOk;
    }

    /// <summary>
    /// Returns true if the result is ok, false if err.
    /// </summary>
    /// <param name="x">The result.</param>
    /// <returns>
    /// True if x is ok, false if x is err.
    /// </returns>
    public static bool operator true(Result<T, TE> x)
    {
        return x.IsOk;
    }

    /// <summary>
    /// Returns true if the result is err, false if ok.
    /// </summary>
    /// <param name="x">The result.</param>
    /// <returns>
    /// True if x is err, false if x is ok.
    /// </returns>
    public static bool operator false(Result<T, TE> x)
    {
        return x.IsErr;
    }

    /// <summary>
    /// Attempts to get the result value if it's ok; otherwise returns false.
    /// </summary>
    /// <param name="o">When this method returns, contains the ok value if the result is ok;
    /// otherwise, contains null.</param>
    /// <returns>
    /// True if the result is ok, false if it's err.
    /// </returns>
    public bool TryGetResult([MaybeNullWhen(false)] out T? o)
    {
        o = this switch
        {
            Ok<T> ok => ok.Value,
            _ => default
        };
        return o is Ok<T>;
    }
}