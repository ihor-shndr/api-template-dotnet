using Books.Common.TryResult;
using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;
using Books.Domain.Books.Services.Implementation;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Books.UnitTests.Books;

public class BookServiceTests
{
    private readonly IBookDao _bookDao;
    private readonly ILogger<BookService> _logger;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _bookDao = Substitute.For<IBookDao>();
        _logger = Substitute.For<ILogger<BookService>>();
        _bookService = new BookService(_logger, _bookDao);
    }

    [Fact]
    public async Task GetBookAsync_WhenBookExists_ReturnsBook()
    {
        var bookId = 1;
        var expectedBook = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            CreatedDate = DateTime.Now
        };

        _bookDao.GetBookAsync(bookId)
                .Returns(TryResult.Success(expectedBook));

        var result = await _bookService.GetBookAsync(bookId);

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedBook.Title, result.Value.Title);
        Assert.Equal(expectedBook.Author, result.Value.Author);
    }

    [Fact]
    public async Task GetBookAsync_WhenBookNotFound_ReturnsError()
    {
        var bookId = 99;
        var error = new Error(BookErrorCodes.BookNotFound, "Book not found");

        _bookDao.GetBookAsync(bookId)
                .Returns((TryResult<Book>)error);

        var result = await _bookService.GetBookAsync(bookId);

        Assert.False(result.IsSuccess);
        Assert.Equal(BookErrorCodes.BookNotFound, result.Error!.Code);

        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<Arg.AnyType>(),
            Arg.Any<Exception?>(),
            Arg.Any<Func<Arg.AnyType, Exception?, string>>());
    }

    [Fact]
    public async Task GetBookAsync_WhenBookExists_AllFieldsAreMapped()
    {
        var bookId = 2;
        var createdDate = new DateTime(2024, 6, 15, 10, 30, 0);
        var expectedBook = new Book
        {
            Title = "Clean Architecture",
            Author = "Robert C. Martin",
            CreatedDate = createdDate
        };

        _bookDao.GetBookAsync(bookId)
                .Returns(TryResult.Success(expectedBook));

        var result = await _bookService.GetBookAsync(bookId);

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedBook.Title, result.Value.Title);
        Assert.Equal(expectedBook.Author, result.Value.Author);
        Assert.Equal(createdDate, result.Value.CreatedDate);
    }

    [Fact]
    public async Task DeleteBookAsync_WhenBookExists_ReturnsSuccessAndForwardsId()
    {
        _bookDao.DeleteBookAsync(7).Returns(TryResult.Success());

        var result = await _bookService.DeleteBookAsync(7);

        Assert.True(result.IsSuccess);
        await _bookDao.Received(1).DeleteBookAsync(7);
    }

    [Fact]
    public async Task DeleteBookAsync_WhenBookNotFound_ReturnsSameError()
    {
        var error = new Error(BookErrorCodes.BookNotFound, "Book not found");
        _bookDao.DeleteBookAsync(99).Returns((TryResult)error);

        var result = await _bookService.DeleteBookAsync(99);

        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
        Assert.Equal(BookErrorCodes.BookNotFound, result.Error!.Code);
        await _bookDao.Received(1).DeleteBookAsync(99);
    }
}
