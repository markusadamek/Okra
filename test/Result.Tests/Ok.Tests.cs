using AwesomeAssertions;
using Okra.Results;
using Xunit;

namespace Result.Tests;

public class OkTests
{
    [Fact]
    public void Converts_To_Ok_Implicitly()
    {
        Result<int, string> result = 42;
        ShouldBeOk(result);
    }

    [Fact]
    public void Converts_To_Ok_Explicitly()
    {
        Result<int, int> result = 42.AsOk();
        ShouldBeOk(result);
    }

    private void ShouldBeOk<T, TE>(Result<T, TE> result)
    {
        result.IsErr.Should().BeFalse();
        result.IsOk.Should().BeTrue();
    }
}