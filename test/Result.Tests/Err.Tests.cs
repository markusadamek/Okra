using AwesomeAssertions;
using Okra.Results;
using Xunit;

namespace Result.Tests;

public class ErrTests
{
    [Fact]
    public void Converts_To_Err_Implicitly()
    {
        Result<int, string> result = "boom";
        ShouldBeError(result);
    }

    [Fact]
    public void Converts_To_Err_Explicitly()
    {
        Result<int, int> result = 42.AsError();
        ShouldBeError(result);
    }

    private void ShouldBeError<T, TE>(Result<T, TE> result)
    {
        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();
    }
}