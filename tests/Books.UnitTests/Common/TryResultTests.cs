using Books.Common.TryResult;

namespace Books.UnitTests.Common;

public class TryResultTests
{
    [Test]
    public void TryResult_Success_HasIsSuccessTrue()
    {
        var result = TryResult.Success();

        Assert.That(result.IsSuccess, Is.True);
    }

    [Test]
    public void TryResult_WithError_HasIsSuccessFalse()
    {
        var error = new Error("TEST_CODE", "Test error message");

        TryResult result = error;

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error!.Code, Is.EqualTo("TEST_CODE"));
            Assert.That(result.Error!.Message, Is.EqualTo("Test error message"));
        });
    }

    [Test]
    public void TryResultT_Success_ValueIsAccessible()
    {
        var value = "hello";

        TryResult<string> result = TryResult.Success(value);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(value));
        });
    }

    [Test]
    public void TryResultT_WithError_ValueIsNull()
    {
        var error = new Error("TEST_CODE", "Test error message");

        TryResult<string> result = error;

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Value, Is.Null);
        });
    }
}
