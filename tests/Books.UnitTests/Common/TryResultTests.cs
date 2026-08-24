using Books.Common.TryResult;

namespace Books.UnitTests.Common;

public class TryResultTests
{
    [Fact]
    public void TryResult_Success_HasIsSuccessTrue()
    {
        var result = TryResult.Success();

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void TryResult_WithError_HasIsSuccessFalse()
    {
        var error = new Error("TEST_CODE", "Test error message");

        TryResult result = error;

        Assert.False(result.IsSuccess);
        Assert.Equal("TEST_CODE", result.Error!.Code);
        Assert.Equal("Test error message", result.Error!.Message);
    }

    [Fact]
    public void TryResultT_Success_ValueIsAccessible()
    {
        var value = "hello";

        TryResult<string> result = TryResult.Success(value);

        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void TryResultT_WithError_ValueIsNull()
    {
        var error = new Error("TEST_CODE", "Test error message");

        TryResult<string> result = error;

        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
    }
}
