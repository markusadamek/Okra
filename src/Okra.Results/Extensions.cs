namespace Okra.Results;

public static class Extensions
{
    extension<T>(T obj)
    {
        public Ok<T> AsOk()
        {
            return new Ok<T>(obj);
        }

        public Err<T> AsError()
        {
            return new Err<T>(obj);
        }
    }
}