using Books.Domain.Books.DataAccess;
using Books.Domain.Books.Models;
using Books.Domain.Books.Services.Implementation;
using Books.Common.TryResult;
using Microsoft.Extensions.Logging;
using Moq;

namespace Books.UnitTests.Books;

public class BookServiceTests
{
    private Mock<IBookDao> _mockBookDao;
    private Mock<ILogger<BookService>> _mockLogger;
    private BookService _bookService;

    [SetUp]
    public void Setup()
    {
        _mockBookDao = new Mock<IBookDao>();
        _mockLogger = new Mock<ILogger<BookService>>();
        _bookService = new BookService(_mockLogger.Object, _mockBookDao.Object);
    }

    [Test]
    public async Task GetBookAsync_WhenBookExists_ReturnsBook()
    {
        var bookId = 1;
        var expectedBook = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            CreatedDate = DateTime.Now
        };

        _mockBookDao.Setup(x => x.GetBookAsync(bookId))
                   .ReturnsAsync(TryResult.Success(expectedBook));

        var result = await _bookService.GetBookAsync(bookId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.Title, Is.EqualTo(expectedBook.Title));
            Assert.That(result.Value.Author, Is.EqualTo(expectedBook.Author));
        });
    }

    [Test]
    public async Task GetBookAsync_WhenBookNotFound_ReturnsError()
    {
        var bookId = 99;
        var error = new Error(BookErrorCodes.BookNotFound, "Book not found");

        _mockBookDao.Setup(x => x.GetBookAsync(bookId))
                   .ReturnsAsync((TryResult<Book>)error);

        var result = await _bookService.GetBookAsync(bookId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error!.Code, Is.EqualTo(BookErrorCodes.BookNotFound));
        });

        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Test]
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

        _mockBookDao.Setup(x => x.GetBookAsync(bookId))
                   .ReturnsAsync(TryResult.Success(expectedBook));

        var result = await _bookService.GetBookAsync(bookId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.Title, Is.EqualTo(expectedBook.Title));
            Assert.That(result.Value.Author, Is.EqualTo(expectedBook.Author));
            Assert.That(result.Value.CreatedDate, Is.EqualTo(createdDate));
        });
    }
}