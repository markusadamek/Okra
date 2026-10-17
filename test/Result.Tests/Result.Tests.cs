using AwesomeAssertions;
using Xunit;

namespace Result.Tests;

using Okra.Results;

public class ResultTests
{
    [Fact]
    public void Result_Err_Evaluates_To_False()
    {
        Result<int, string> result = "boom";
        if (result)
        {
            Assert.Fail();
        }

        return;
    }

    [Fact]
    public void Result_Err_Not_Evaluates_To_True()
    {
        Result<int, string> result = "boom";
        if (!result)
        {
            return;
        }

        Assert.Fail();
    }

    [Fact]
    public void Result_Ok_Evaluates_To_True()
    {
        Result<int, string> resultOk1 = 42;
        if (resultOk1)
        {
            return;
        }

        Assert.Fail();
    }

    [Fact]
    public void Result_Ok_Not_Evaluates_To_False()
    {
        Result<int, string> resultOk1 = 42;
        if (!resultOk1)
        {
            Assert.Fail();
        }
    }

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
    public void Throws_Custom_Exception_When_Err_Unwrapped()
    {
        var ex = new Exception("test");
        Result<int, string> result = "boom";
        Action unwrapped = () => result.Unwrap(ex);
        unwrapped.Should().ThrowExactly<Exception>("test");
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

        switch (result.MapErr(e => 42))
        {
            case Err<int> err:
                err.Error.Should().Be(42);
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void Maps_Ok_Types()
    {
        Result<int, string> result = 42;

        switch (result.MapOk(ok => "ok"))
        {
            case Ok<string> err:
                err.Value.Should().Be("ok");
                break;
            default:
                Assert.Fail();
                break;
        }
    }

    [Fact]
    public void If_Can_Unwrap_Ok()
    {
        Result<int, string> result = 42;

        if (result.TryGetResult(out var resultOk))
        {
            resultOk.Should().Be(42);
        }
    }
}