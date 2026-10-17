namespace Okra.Results;

/// <summary>
/// Provides extension methods for converting values into <see cref="Ok{T}"/> or <see cref="Err{T}"/> results.
/// </summary>
public static class Extensions
{
    extension<T>(T obj)
    {
        /// <summary>
        /// Wraps the value in a successful <see cref="Ok{T}"/> result.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>An <see cref="Ok{T}"/> containing this value.</returns>
        public Ok<T> AsOk()
        {
            return new Ok<T>(obj);
        }

        /// <summary>
        /// Wraps the value in a failed <see cref="Err{T}"/> result.
        /// </summary>
        /// <typeparam name="T">The type of the error payload.</typeparam>
        /// <returns>An <see cref="Err{T}"/> containing this value.</returns>
        public Err<T> AsError()
        {
            return new Err<T>(obj);
        }
    }
}