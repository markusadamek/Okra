using AwesomeAssertions;
using Okra.Results;
using Xunit;

namespace Result.Tests;

public class OkTests
{
    [Fact]
    public void Converts_To_Ok_Implicitly()
    {
        // Converts a numeric value to an Ok type (implicitly)
        Result<int, string> result = 42;
        ShouldBeOk(result);
    }

    [Fact]
    public void Converts_To_Ok_Explicitly()
    {
        // Converts a numeric value to an Ok type (explicitly)
        Result<int, int> result = 42.AsOk();
        ShouldBeOk(result);
    }

    private void ShouldBeOk<T, TE>(Result<T, TE> result)
    {
        // Verify the result is not an error
        result.IsErr.Should().BeFalse();
        // Verify the result is a successful Ok
        result.IsOk.Should().BeTrue();
    }
}
