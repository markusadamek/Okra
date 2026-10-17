using AwesomeAssertions;
using Xunit;

namespace Result.Tests;

using Okra.Results;

public class ResultTests
{
    [Fact]
    public void Returns_Result_When_Unwrapped()
    {
        Result<int, string> result = 42;
        result.Unwrap().Should().Be(42);
    }

    [Fact]
    public void Throws_When_Err_Unwrapped()
    {
        Result<int, string> result = "boom";
        Action unwrapped = () => result.Unwrap();
        unwrapped.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Throws_When_Err_Unwrapped_With_Message()
    {
        string message = "boom exception";
        Result<int, string> result = "boom";
        Action unwrapped = () => result.Unwrap(message);
        unwrapped.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Maps_Error_Types()
    {
        Result<int, string> result = "boom";
        
        switch(result.MapErr(e => 42))
        {
            case Err<int> err:
                err.Error.Should().Be(42);
            break;
            default: Assert.Fail();
                break;
        }
    }
    
    [Fact]
    public void Maps_Ok_Types()
    {
        Result<int, string> result = 42;
        
        switch(result.MapOk(ok => "ok"))
        {
            case Ok<string> err:
                err.Value.Should().Be("ok");
                break;
            default: Assert.Fail();
                break;
        }
    }
}