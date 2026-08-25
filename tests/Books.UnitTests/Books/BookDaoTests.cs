using Books.Data.Books.DataAccess;
using Books.Data.Books.Entities;
using Books.Domain.Books.Models;

namespace Books.UnitTests.Books;

public class BookDaoTests : IDisposable
{
    private readonly BookDao _bookDao = new();
    private readonly List<BookEntity> _originalSeedBooks = [.. BookSeedData.Books];

    public void Dispose()
    {
        BookSeedData.Books.Clear();
        BookSeedData.Books.AddRange(_originalSeedBooks);
    }

    [Fact]
    public async Task DeleteBookAsync_WhenBookExists_RemovesBookAndReturnsSuccess()
    {
        var bookId = 1;

        var getBeforeDelete = await _bookDao.GetBookAsync(bookId);
        Assert.True(getBeforeDelete.IsSuccess);

        var result = await _bookDao.DeleteBookAsync(bookId);

        Assert.True(result.IsSuccess);

        var getAfterDelete = await _bookDao.GetBookAsync(bookId);
        Assert.False(getAfterDelete.IsSuccess);
        Assert.Equal(BookErrorCodes.BookNotFound, getAfterDelete.Error!.Code);
    }

    [Fact]
    public async Task DeleteBookAsync_WhenBookDoesNotExist_ReturnsErrorAndLeavesStoreUnchanged()
    {
        var bookId = 999;

        var result = await _bookDao.DeleteBookAsync(bookId);

        Assert.False(result.IsSuccess);
        Assert.Equal(BookErrorCodes.BookNotFound, result.Error!.Code);

        var otherBook = await _bookDao.GetBookAsync(2);
        Assert.True(otherBook.IsSuccess);
    }
}
